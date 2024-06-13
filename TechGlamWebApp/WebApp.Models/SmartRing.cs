using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Interfaces;

namespace WebApp.Models
{
    public class SmartRing : Product, ICharacteristics
    {
        private bool healthMonitoring;
        private bool activityTracking;
        private bool notifications;
        private bool securityFeatures;
        private bool waterproof;

        public SmartRing(string name, string image, string description, decimal price, WebAppEnum.Category category,
                          string color, WebAppEnum.SizeRingsBracelets size, string metalType, bool healthMonitoring, bool activityTracking, bool notifications, bool securityFeatures, bool waterproof) :
               base(name, image, description, price, category, color, size, metalType)
        {
            this.healthMonitoring = healthMonitoring;
            this.activityTracking = activityTracking;
            this.notifications = notifications;
            this.securityFeatures = securityFeatures;
            this.waterproof = waterproof;
        }
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
    }
}