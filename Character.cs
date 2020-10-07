using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloWorld
{
    class Character
    {
        //character stats
        private string _name;
        private string _role;
        private float _health;
        protected float _damage;
        public int _gold;

        //character constructor
        public Character()
        {
            _name = "Hero";
            _role = "Adventurer";
            _health = 100.0f;
            _damage = 10.0f;
            _gold = 4;
        }

        //constructor overload
        public Character(string nameVal, string roleVal, float healthVal, float damageVal, int goldVal)
        {
            _name = nameVal;
            _role = roleVal;
            _health = healthVal;
            _damage = damageVal;
        }

        ///////////////////////////////////////////////////
        //Saving and loading
        //////////////////////////////////////////////////

        public virtual void Save(StreamWriter writer)
        {
            //Save the characters stats
            writer.WriteLine(_name);
            writer.WriteLine(_role);
            writer.WriteLine(_health);
            writer.WriteLine(_damage);
        }

        public virtual bool Load(StreamReader reader)
        {
            //Create variables to store loaded data.
            string name = reader.ReadLine();
            reader.ReadLine();
            string role = _role;
            float health = 10;
            float damage = 1;
            //Checks to see if loading was successful.
            if (float.TryParse(reader.ReadLine(), out health) == false)
            {
                return false;
            }
            if (float.TryParse(reader.ReadLine(), out damage) == false)
            {
                return false;
            }
            //If successful, set update the member variables and return true.
            _name = name;
            _role = reader.ReadLine();
            reader.ReadLine();
            _health = health;
            _damage = damage;
            return true;
        }

        ////////////////////////////////////////////
        //FUNCTIONS USED FOR GETTING CHARACTER INFO
        ////////////////////////////////////////////

        //prints out the character stats
        public virtual void ViewStats()
        {
            Console.WriteLine("\nName: " + _name);
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

        //gets the amount of gold the player has
        public int GetGold()
        {
            return _gold;
        }

        ////////////////////////////
        //fUNCTIONS USED FOR BATTLE
        ////////////////////////////

        //functions that allows the player to attack the "enemy"
        public virtual float Attack(Monster monster)
        {
            return monster.TakeDamage(_damage);
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

        public virtual float Heal(float healthVal)
        {
            return _health = healthVal;
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
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
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
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
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
