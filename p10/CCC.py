from pysimplesoap.server import SoapDispatcher, SOAPHandler
from http.server import HTTPServer
from geopy.distance import geodesic

# -------------------------
# تابع محاسبه محیط مثلث
# -------------------------
def triangle_perimeter(lat1, lon1, lat2, lon2, lat3, lon3):
    a = geodesic((lat1, lon1), (lat2, lon2)).kilometers
    b = geodesic((lat2, lon2), (lat3, lon3)).kilometers
    c = geodesic((lat3, lon3), (lat1, lon1)).kilometers
    return a + b + c

# -------------------------
# SOAP Dispatcher
# -------------------------
dispatcher = SoapDispatcher(
    name="TriangleService",
    location="http://localhost:8000/",
    action="http://localhost:8000/",
    namespace="http://example.com/triangle",
    prefix="tns",
    documentation="Web Service برای محاسبه محیط مثلث",
    trace=True,
    ns=True
)

dispatcher.wsdl = True

dispatcher.register_function(
    'trianglePerimeter',
    triangle_perimeter,
    returns={'perimeter': float},
    args={
        'lat1': float, 'lon1': float,
        'lat2': float, 'lon2': float,
        'lat3': float, 'lon3': float
    }
)

# -------------------------
# اجرای سرور (این بخش خیلی مهم است)
# -------------------------
if __name__ == '__main__':
    print("SOAP Server is running on http://localhost:8000/")
    httpd = HTTPServer(('', 8000), SOAPHandler)
    httpd.dispatcher = dispatcher
    httpd.serve_forever()
