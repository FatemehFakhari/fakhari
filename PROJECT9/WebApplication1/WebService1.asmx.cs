using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplication1.Models;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public List<GeoPoint> Points = new List<GeoPoint>()
    {
        new GeoPoint() { ID = 1, X = 51.1, Y = 35.5 },
        new GeoPoint() { ID = 2, X = 52.3, Y = 34.3 },
        new GeoPoint() { ID = 3, X = 53.2, Y = 32.3 },
        new GeoPoint() { ID = 4, X = 49.9, Y = 35.7 },
    };
        
        private double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371000; 

            double dLat = (lat2 - lat1) * Math.PI / 180.0;
            double dLon = (lon2 - lon1) * Math.PI / 180.0;

            lat1 = lat1 * Math.PI / 180.0;
            lat2 = lat2 * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            return R * c; 
        }
        private GeoPoint getbyID(int id)
        {
            GeoPoint point = null;
            foreach (GeoPoint pt in Points)
            {
                if (pt.ID == id)
                {
                    point = pt;
                }
            }
            return point;
        }
        [WebMethod]
        public GeoPoint GetPointByID(int id)
        {
            GeoPoint point = getbyID(id);
            return point;
        }
        [WebMethod]
        public double GetDistance(int id1, int id2)
        {
            GeoPoint p1 = getbyID(id1);
            GeoPoint p2 = getbyID(id2);

            if (p1 != null && p2 != null)
            {
                return HaversineDistance(p1.Y, p1.X, p2.Y, p2.X);
            }

            return 0;
        }
        [WebMethod]
        public double GetTrianglePerimeter(int id1, int id2, int id3)
        {
            GeoPoint p1 = getbyID(id1);
            GeoPoint p2 = getbyID(id2);
            GeoPoint p3 = getbyID(id3);

            if (p1 == null || p2 == null || p3 == null)
                return 0;

            double a = HaversineDistance(p1.Y, p1.X, p2.Y, p2.X);
            double b = HaversineDistance(p2.Y, p2.X, p3.Y, p3.X);
            double c = HaversineDistance(p3.Y, p3.X, p1.Y, p1.X);

            return a + b + c;
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }

