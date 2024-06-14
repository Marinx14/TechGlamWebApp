using WebApp.Interfaces;
using WebApp.enumeration;
namespace WebApp.Models
{
    /// <summary>
    /// The SmartRing class represents a smart ring product and includes various characteristics 
    /// such as health monitoring, activity tracking, notifications, security features, and waterproofing.
    /// </summary>
    public class SmartRing : Product, ICharacteristics
    {
        // Private fields for smart ring characteristics
        private bool healthMonitoring;
        private bool activityTracking;
        private bool notifications;
        private bool securityFeatures;
        private bool waterproof;
        private string metalType;

        /// <summary>
        /// Initializes a new instance of the SmartRing class.
        /// </summary>
        /// <param name="name">The name of the smart ring.</param>
        /// <param name="image">The image URL of the smart ring.</param>
        /// <param name="description">The description of the smart ring.</param>
        /// <param name="price">The price of the smart ring.</param>
        /// <param name="category">The category of the product.</param>
        /// <param name="color">The color of the smart ring.</param>
        /// <param name="size">The size of the smart ring.</param>
        /// <param name="metalType">The type of metal used in the smart ring.</param>
        /// <param name="healthMonitoring">Indicates if health monitoring is available.</param>
        /// <param name="activityTracking">Indicates if activity tracking is available.</param>
        /// <param name="notifications">Indicates if notifications are available.</param>
        /// <param name="securityFeatures">Indicates if security features are available.</param>
        /// <param name="waterproof">Indicates if the smart ring is waterproof.</param>
        public SmartRing(string name, string image, string description, decimal price, WebAppEnum.Category category,
                          string color, WebAppEnum.SizeRingsBracelets size, string metalType, bool healthMonitoring, bool activityTracking,
                          bool notifications, bool securityFeatures, bool waterproof) :
               base(name, image, description, price, category, color, size)
        {
            this.healthMonitoring = healthMonitoring;
            this.activityTracking = activityTracking;
            this.notifications = notifications;
            this.securityFeatures = securityFeatures;
            this.waterproof = waterproof;
            this.metalType = metalType;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the smart ring has health monitoring.
        /// </summary>
        public bool HealthMonitoring
        {
            get { return healthMonitoring; }
            set { healthMonitoring = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the smart ring has activity tracking.
        /// </summary>
        public bool ActivityTracking
        {
            get { return activityTracking; }
            set { activityTracking = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the smart ring can receive notifications.
        /// </summary>
        public bool Notifications
        {
            get { return notifications; }
            set { notifications = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the smart ring has security features.
        /// </summary>
        public bool SecurityFeatures
        {
            get { return securityFeatures; }
            set { securityFeatures = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the smart ring is waterproof.
        /// </summary>
        public bool Waterproof
        {
            get { return waterproof; }
            set { waterproof = value; }
        }

        /// <summary>
        /// Gets or sets the type of metal used in the smart ring.
        /// </summary>
        public string MetalType
        {
            get { return metalType; }
            set { metalType = value; }
        }
    }
}
