using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorClustering {
    class KNode : Pixel{
        public byte? bindedCluster = null;
        public KNode(byte _red, byte _green, byte _blue) : base( _red , _green , _blue) {
            
        }
    }
}
