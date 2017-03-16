using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiTroll
{
    class Object
    {
        public byte BParam1;
        public byte BParam2;
        public byte Model;
        public int Behaviour;

        public Object(byte BParam1, byte BParam2, byte Model, int Behaviour)
        {
            this.BParam1 = BParam1;
            this.BParam2 = BParam2;
            this.Model = Model;
            this.Behaviour = Behaviour;
        }
    }
}
