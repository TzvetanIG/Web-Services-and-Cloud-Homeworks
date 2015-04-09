namespace WcfServiceCalculator
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IServiceCalculator
    {
        [OperationContract]
        double CalculateDistance(Point startPoint, Point endPoint);
    }
}
