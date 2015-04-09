namespace WcfServiceCalculator
{
    using System;

    public class ServiceCalculator : IServiceCalculator
    {
        public double CalculateDistance(Point startPoint, Point endPoint)
        {
            var deltaX = startPoint.X - endPoint.X;
            var deltaY = startPoint.Y - endPoint.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance;
        }
    }
}
