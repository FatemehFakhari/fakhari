#import sqlite3
#con = sqlite3.connect(r"C:\Users\SSD_Squad\Desktop\sql1.db")
#cur = con.cursor()
#res = cur.execute("SELECT id, username, password FROM users")
#res.fetchone()
#res.fetchall()
#cur.close()
#con.close()
import uuid

from flask import Flask, request, jsonify
import sqlite3

app = Flask(__name__)
DB_PATH = "sql1.db"

@app.route('/login', methods=['POST'])
def login():
    data = request.json
    username = data.get('username')
    password = data.get('password')

    con = sqlite3.connect(DB_PATH)
    cur = con.cursor()

    user = cur.execute(
        "SELECT id FROM users WHERE username=? AND password=?",
        (username, password)
    ).fetchone()

    if not user:
        return jsonify({"error": "Invalid username or password"}), 401

    token = str(uuid.uuid4())

    cur.execute(
        "UPDATE users SET token=? WHERE id=?",
        (token, user[0])
    )
    con.commit()
    con.close()

    return jsonify({
        "message": "login successful",
        "token": token
    })


#info
@app.route('/info', methods=['GET'])
def info():
    token = request.headers.get('Authorization')

    if not token:
        return jsonify({"error": "Token required"}), 401

    con = sqlite3.connect(DB_PATH)
    cur = con.cursor()

    user = cur.execute(
        "SELECT id, username FROM users WHERE token=?",
        (token,)
    ).fetchone()

    con.close()

    if not user:
        return jsonify({"error": "Invalid token"}), 401

    return jsonify({
        "id": user[0],
        "username": user[1],
        "info": "این یک اطلاعات دلخواه است"
    })

app.run(debug=True)


