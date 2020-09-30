using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Rogue : Character
    {
        //adds stamina to character stats
        private float _stamina;

        //rogue constructor 
        public Rogue() : base()
        {
            _stamina = 100.0f;
        }

        //rogue constructor overload
        public Rogue(string nameVal, string roleVal, float healthVal, float damageVal, float staminaVal)
            : base(nameVal, roleVal, healthVal, damageVal)
        {
            _stamina = staminaVal;
        }

        //override of viewstats that addes stamina 
        public override void ViewStats()
        {
            Console.WriteLine("Mana: " + _stamina); ;
        }
    }
}
