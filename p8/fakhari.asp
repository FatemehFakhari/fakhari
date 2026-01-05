<%
x1 = CDbl(Request.QueryString("x1"))
y1 = CDbl(Request.QueryString("y1"))
x2 = CDbl(Request.QueryString("x2"))
y2 = CDbl(Request.QueryString("y2"))

distance = Sqr((x2 - x1)^2 + (y2 - y1)^2)

Response.Write "Distance between (" & x1 & "," & y1 & ") and (" & x2 & "," & y2 & ") is: " & distance
%>
