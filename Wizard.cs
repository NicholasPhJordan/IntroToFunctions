using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Wizard : Character
    {
        //adds mana to character stats
        private float _mana;

        //adds mana to character constructor
        public Wizard() : base()
        {
            _mana = 100;
        }

        //adds mana to character overload constructor
        public Wizard(string nameVal, string roleVal, float healthVal, float damageVal, float manaVal)
            : base(nameVal, roleVal, healthVal, damageVal)
        {
            _mana = manaVal;
        }

        //override of viewstats that addes mana 
        public override void ViewStats()
        {
            base.ViewStats();
            Console.WriteLine("Mana: " + _mana); ;
        }

        //override of the attack function for the wizard
        //allows the player as a wizard to choose a spell to cast based on the amount of mana they have
        public override float Attack(Monster monster)
        {
            float damageTaken = 0.0f;
            if (_mana >= 30)
            {
                char input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    Console.WriteLine("Choose a spell to cast");
                    Console.WriteLine("1. Frost [20 Damage]");
                    Console.WriteLine("2. Fireball [25 Damage]");
                    Console.WriteLine("3. Lightning [30 Damage]");
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    if (input == '1')
                    {
                        float totalDamage = 20.0f;
                        _mana -= 20.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                    else if (input == '2')
                    {
                        float totalDamage = 25.0f;
                        _mana -= 25.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                    if (input == '3')
                    {
                        float totalDamage = 30.0f;
                        _mana -= 30.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                }
            }
            else if (_mana >= 25)
            {
                char input = ' ';
                while (input != '1' && input != '2')
                {
                    Console.WriteLine("Choose a spell to cast");
                    Console.WriteLine("1. Frost [20 Damage]");
                    Console.WriteLine("2. Fireball [25 Damage]");
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    if (input == '1')
                    {
                        float totalDamage = 20.0f;
                        _mana -= 20.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                    else if (input == '2')
                    {
                        float totalDamage = 25.0f;
                        _mana -= 25.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                }
            }
            if (_mana >= 20)
            {
                char input = ' ';
                while (input != '1')
                {
                    Console.WriteLine("Choose a spell to cast");
                    Console.WriteLine("1. Frost [20 Damage]");
                    Console.Write("> ");
                    input = Console.ReadKey().KeyChar;
                    if (input == '1')
                    {
                        float totalDamage = 20.0f;
                        _mana -= 20.0f;
                        damageTaken = monster.TakeDamage(totalDamage);
                        return damageTaken;
                    }
                }
            }
            damageTaken = base.Attack(monster);
            return damageTaken;
        }
    }
}
