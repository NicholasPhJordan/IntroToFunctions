using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Knight : Character
    {
        //knight constructor
        public Knight() : base()
        {
        }

        //knight constructor overload
        public Knight(string nameVal, string roleVal, float healthVal, float damageVal)
            : base(nameVal, roleVal, healthVal, damageVal)
        {
        }
    }
}
