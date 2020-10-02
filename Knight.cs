using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Knight : Character
    {
        //adds stamina to character stats
        private float _rage;

        //rogue constructor 
        public Knight() : base()
        {
            _rage = 100.0f;
        }

        //rogue constructor overload
        public Knight(string nameVal, string roleVal, float healthVal, float damageVal, int goldVal, float rageVal)
            : base(nameVal, roleVal, healthVal, damageVal, goldVal)
        {
            _rage = rageVal;
        }

        //override of viewstats that addes stamina 
        public override void ViewStats()
        {
            base.ViewStats();
            Console.WriteLine("Rage: " + _rage); ;
        }

        //Heal, but with rage
        public override float Heal(float healthVal)
        {
            _rage = 100.0f;
            return base.Heal(healthVal);
        }

        public override float Attack(Monster monster)
        {
            float damageTaken = 0.0f;
            if (_rage >= 30.0f)
            {
                char input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    Console.WriteLine("1. Charge [30 Damage]");
                    Console.WriteLine("2. Lunge [20 Damage]");
                    Console.WriteLine("3. Normal Attack [10 Damage]");
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    if (input == '1')
                    {
                        float totalDamage = 30.0f;
                        _rage -= 30.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                    else if (input == '2')
                    {
                        float totalDamage = 20.0f;
                        _rage -= 20.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                    else
                    {
                        damageTaken = base.Attack(monster);
                    }
                }
            }
            else if (_rage >= 20.0f)
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
                        _rage -= 20.0f;
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
