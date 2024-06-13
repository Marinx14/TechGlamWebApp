namespace WebAppInterfaces
{
    public interface ICharacteristics
    {
            bool HealthMonitoring { get; set; }
            bool ActivityTracking { get; set; }
            bool Notifications { get; set; }
            bool SecurityFeatures { get; set; }
            bool Waterproof { get; set; }

        }
 }
