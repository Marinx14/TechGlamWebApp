using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Interfaces
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