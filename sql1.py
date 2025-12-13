#import sqlite3
#con = sqlite3.connect(r"C:\Users\SSD_Squad\Desktop\sql1.db")
#cur = con.cursor()
#res = cur.execute("SELECT id, username, password FROM users")
#res.fetchone()
#res.fetchall()
#cur.close()
#con.close()
from flask import Flask, request, jsonify
import sqlite3

app = Flask(__name__)
DB_PATH = "sql1.db"

@app.route('/login', methods=['POST'])
def login():
    u, p = request.json.get('username'), request.json.get('password')
    user = sqlite3.connect(DB_PATH).execute(
        "SELECT id FROM users WHERE username=? AND password=?", (u, p)
    ).fetchone()
    return jsonify({"user_id": user[0]} if user else {"error": "Invalid"}, 200 if user else 401)

app.run(debug=True)

