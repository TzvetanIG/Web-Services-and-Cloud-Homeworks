namespace DistanceCalculatorRESTService.Controllers
{
    using System;
    using System.Web.Http;
    using Models;

    public class CalculatorController : ApiController
    {
        [HttpPost]
        [Route("api/distance")]
        public double CalculateDistance([FromBody]TwoPoints data)
        {
            var deltaX = data.Point1.X - data.Point2.X;
            var deltaY = data.Point1.Y - data.Point2.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance;
        }
    }
}
