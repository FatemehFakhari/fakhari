using System;
using System.Web.Http;
using PROJECT11.Models;

namespace PROJECT11.Controllers
{
    [RoutePrefix("api/triangle")]
    public class TriangleController : ApiController
    {
        [HttpPost]
        [Route("perimeter")]
        public IHttpActionResult Perimeter([FromBody] TriangleRequest req)
        {
            if (req == null || req.A == null || req.B == null || req.C == null)
                return BadRequest("Invalid input.");

            double ab = Distance(req.A, req.B);
            double bc = Distance(req.B, req.C);
            double ca = Distance(req.C, req.A);

            double perimeter = ab + bc + ca;

            var res = new TriangleResponse
            {
                SideAB_m = Math.Round(ab, 2),
                SideBC_m = Math.Round(bc, 2),
                SideCA_m = Math.Round(ca, 2),
                Perimeter_m = Math.Round(perimeter, 2),
                Perimeter_km = Math.Round(perimeter / 1000.0, 6)
            };

            return Ok(res);
        }

        private double Distance(Coordinate p1, Coordinate p2)
        {
            const double R = 6371000; // شعاع زمین به متر

            double lat1 = DegToRad(p1.Latitude);
            double lat2 = DegToRad(p2.Latitude);
            double dLat = lat2 - lat1;
            double dLon = DegToRad(p2.Longitude - p1.Longitude);

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double DegToRad(double deg)
        {
            return deg * Math.PI / 180.0;
        }
    }
}
