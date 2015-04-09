namespace WcfServiceCalculator
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Point
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }
}