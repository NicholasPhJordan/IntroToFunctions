using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Rogue : Character
    {
        //rogue constructor 
        public Rogue() : base()
        {
        }

        //rogue constructor overload
        public Rogue(string nameVal, string roleVal, float healthVal, float damageVal)
            : base(nameVal, roleVal, healthVal, damageVal)
        {
        }
    }
}
