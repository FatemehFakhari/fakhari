from zeep import Client

client = Client('http://localhost:8000/')

result = client.service.trianglePerimeter(
    lat1=35.6892, lon1=51.3890,
    lat2=35.7000, lon2=51.4000,
    lat3=35.6800, lon3=51.3700
)

print("Perimeter:", result['perimeter'])
