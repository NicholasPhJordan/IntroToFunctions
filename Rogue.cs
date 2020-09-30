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
            base.ViewStats();
            Console.WriteLine("Stamina: " + _stamina); ;
        }

        public override float Attack(Monster monster)
        {
            float damageTaken = 0.0f;
            if (_stamina >= 20)
            {
                char input = ' ';
                while (input != '1' && input != '2')
                {
                    Console.WriteLine("1. Fast Attack [20 Damage]");
                    Console.WriteLine("2. Normal Attack [10 Damage]");
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    if (input == '1')
                    {
                        float totalDamage = 20.0f;
                        _stamina -= 20.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                    else
                    {
                        damageTaken = base.Attack(monster);
                    }
                }
            }
            return damageTaken;
        }
    }
}
