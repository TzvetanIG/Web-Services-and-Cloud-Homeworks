namespace DistanceCalculatorRESTService.Models
{
    public class TwoPoints
    {
        public int StartPointX { get; set; }
        public int StartPointY { get; set; }
        public int EndPointX { get; set; }
        public int EndPointY { get; set; }

        public Point Point1 { get; set; }

        public Point Point2 { get; set; }
    }
}