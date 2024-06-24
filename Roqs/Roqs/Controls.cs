using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roqs
{
    public class Controls
    {
        public bool left, right, up, down; // Move the player
        public enum key { JUMP = 'Z', LEFT='Q', DOWN='S', RIGHT='D' } // Keys to control the player
    }
}
