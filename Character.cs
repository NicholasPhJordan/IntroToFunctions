using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Character
    {
        //character stats
        private string _name;
        private string _role;
        private float _health;
        protected float _damage;

        //character constructor
        public Character()
        {
            _name = "Hero";
            _role = "Adventurer";
            _health = 100.0f;
            _damage = 10.0f;
        }

        //constructor overload
        public Character(string nameVal, string roleVal, float healthVal, float damageVal)
        {
            _name = nameVal;
            _role = roleVal;
            _health = healthVal;
            _damage = damageVal;
        }

        ////////////////////////////////////////////
        //FUNCTIONS USED FOR GETTING CHARACTER INFO
        ////////////////////////////////////////////

        //function that allows the player to choose their name 
        public void ChooseName()
        {
            Console.Clear();
            Typeout("Welcome! What is your name adventurer?");
            Console.Write("> ");
            _name = Console.ReadLine();
            Typeout("Hello " + _name);
        }

        //prints out the character stats
        public virtual void ViewStats()
        {
            Console.WriteLine("Name: " + _name);
            Console.WriteLine("Role: " + _role);
            Console.WriteLine("Health: " + _health);
            Console.WriteLine("Damage: " + _damage);
        }

        //return the character's/ player's name 
        public string GetName()
        {
            return _name;
        }

        //return the character's/ player's role
        public string GetRole()
        {
            return _role;
        }

        public float GetHealth()
        {
            return _health;
        }

        ////////////////////////////
        //fUNCTIONS USED FOR BATTLE
        ////////////////////////////

        //functions that allows the player to attack the "enemy"
        public virtual float Attack(float enemyHealth)
        {
            return enemyHealth.TakeDamage(_damage);
        }

        //funtion used to apply damage to the players health
        public virtual float TakeDamage(float damageVal)
        {
            _health -= damageVal;
            if (_health < 0)
            {
                _health = 0;
            }
            return damageVal;
        }

        /////////////////////////////////////////////////////////////
        //OTHER FUNCTIONS USED FOR DESIGN PURPOSES AND FUNCTIONALITY
        /////////////////////////////////////////////////////////////

        //I did get this off the internet and I do understand what it's saying
        //Adds typed out look to text
        //function that takes a string and prints out one character at a time with a slight pause between each character then prints a new line
        //https://stackoverflow.com/questions/25337336/how-to-make-text-be-typed-out-in-console-application site i got info from
        static void Typeout(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(30); //the pause in this instance in 30ms 
            }
            Console.WriteLine();
        }

        //Get player feed back when two options available
        Char GetInput(string option1, string option2, string query)
        {
            char input = ' ';
            while (input != '1' && input != '2')
            {
                Typeout(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. View Stats");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                if (input == '3')
                {
                    ViewStats();
                    PrintInventory(_inventory);
                }
                Console.WriteLine();
            }
            return input;
        }

        //get player feed back when three options available 
        Char GetInput(string option1, string option2, string option3, string query)
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                Typeout(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. " + option3);
                Console.WriteLine("4. View Stats");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                if (input == '4')
                {
                    ViewStats();
                    PrintInventory(_inventory);
                }
                Console.WriteLine();
            }
            return input;
        }

        //clears the screen and continues with player input
        public void ClearScreen()
        {
            Console.WriteLine("Press any key to continue");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
