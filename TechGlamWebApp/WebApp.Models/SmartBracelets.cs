using WebApp.Interfaces;
using WebApp.enumeration;
namespace WebApp.Models
{
    public class SmartBracelet : Product, ICharacteristics
    {
        // Private fields for characteristics
        private bool healthMonitoring;
        private bool activityTracking;
        private bool notifications;
        private bool securityFeatures;
        private bool waterproof;
        private double batteryLife;
        private string compatibility;
        private string displayType;
        private bool gps;
        private bool heartRateMonitor;

        // Constructor initializing base properties and new characteristics
        public SmartBracelet(string name, string image, string description, decimal price, WebAppEnum.Category category,
                          string color, WebAppEnum.SizeRingsBracelets size, bool healthMonitoring, bool activityTracking,
                          bool notifications, bool securityFeatures, bool waterproof, double batteryLife, string compatibility,
                          string displayType, bool gps, bool heartRateMonitor) :
               base(name, image, description, price, category, color, size)
        {
            this.healthMonitoring = healthMonitoring;
            this.activityTracking = activityTracking;
            this.notifications = notifications;
            this.securityFeatures = securityFeatures;
            this.waterproof = waterproof;
            this.batteryLife = batteryLife;
            this.compatibility = compatibility;
            this.displayType = displayType;
            this.gps = gps;
            this.heartRateMonitor = heartRateMonitor;
        }

        // Properties for characteristics
        public bool HealthMonitoring
        {
            get { return healthMonitoring; }
            set { healthMonitoring = value; }
        }

        public bool ActivityTracking
        {
            get { return activityTracking; }
            set { activityTracking = value; }
        }

        public bool Notifications
        {
            get { return notifications; }
            set { notifications = value; }
        }

        public bool SecurityFeatures
        {
            get { return securityFeatures; }
            set { securityFeatures = value; }
        }

        public bool Waterproof
        {
            get { return waterproof; }
            set { waterproof = value; }
        }

        public double BatteryLife
        {
            get { return batteryLife; }
            set { batteryLife = value; }
        }

        public string Compatibility
        {
            get { return compatibility; }
            set { compatibility = value; }
        }

        public string DisplayType
        {
            get { return displayType; }
            set { displayType = value; }
        }

        public bool GPS
        {
            get { return gps; }
            set { gps = value; }
        }

        public bool HeartRateMonitor
        {
            get { return heartRateMonitor; }
            set { heartRateMonitor = value; }
        }
    }
}
