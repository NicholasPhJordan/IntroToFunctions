using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Character
    {
        //character stats
        private string _name;
        private float _health;
        protected float _damage;

        //character constructor
        public Character()
        {
            _name = "Hero";
            _health = 100.0f;
            _damage = 10.0f;
        }

        //constructor overload
        public Character(float healthVal, string nameVal, float damageVal)
        {
            _name = nameVal;
            _health = healthVal;
            _damage = damageVal;
        }

        //FUNCTIONS USED FOR GETTING CHARACTER INFO

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
        public void ViewStats()
        {
            Console.WriteLine("Name: " + _name);
            Console.WriteLine("Health: " + _health);
            Console.WriteLine("Damage: " + _damage);
        }

        //return the character's/ player's name 
        public string GetName()
        {
            return _name;
        }

        //fUNCTIONS USED FOR BATTLE

        //functions that allows the player to attack the "enemy"
        public virtual float Attack(Character enemy)
        {
            return enemy.TakeDamage(_damage);
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

        //OTHER FUNCTIONS USED FOR DESIGN PURPOSES

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
    }
}
