using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roqs
{
    class Constants
    {

        public const float scale = 2f;
        public const float averageSpeedWalk = 5.5f / scale; /* (km/h) */
        public const float averageSpeedRun = 12.5f / scale; /* (km/h) */
        public const float averageHumanHeight = 1.7f / scale; /* (m) */
        public const float averageHumanWidth= averageHumanHeight* 0.45f; /* (m), not accurate but good */
        public const float ratioPixelMeter = 100f / averageHumanHeight / scale; /* Number of pixel per meter */
        public const int playerHeight = (int)(Constants.ratioPixelMeter * Constants.averageHumanHeight); // 1m75 in pixel
        public const int playerWidth = (int)(Constants.ratioPixelMeter * Constants.averageHumanWidth); // normal ratio for human male
        public const float playerJumpForce = 1f; 
    }
}
