using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Player
    {
        public string name;
        public float health;
        public float damage;
        public string role;

        //List that will act as the player's inventory
        public List<string> inventory = new List<string> { };

        public Player()
        {
            name = "None";
            health = 100.0f;
            damage = 0.0f;
            role = "Adventurer";
        }

        public Player(string nameVal, float healthVal, float damageVal, string roleVal)
        {
            name = nameVal;
            health = healthVal;
            damage = damageVal;
            role = roleVal;
        }

        //function that shows player stats
        public void ViewStats()
        {
            Console.WriteLine("\nName: " + name);
            Console.WriteLine("Role: " + role);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + damage);
            Console.Write("Inventory: ");
            inventory.ForEach(Console.Write);
            Console.WriteLine("\nPress any key to continue");
            Console.Write("> ");
            Console.ReadKey();
        }
        
        //playerm chooses name
        public void ChooseName()
        {
            Console.Clear();
            Typeout("Welcome! What is your name adventurer?");
            Console.Write("> ");
            name = Console.ReadLine();
            Typeout("Hello " + name);
        }

        //player chooses role
        public void ChooseRole()
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                Console.Clear();
                input = GetInput("Knight", "Rogue", "Wizard", name + " please select a Role.");
                switch (input)
                {
                    case '1':
                        //gives knight stats to player
                        
                        break;
                    case '2':
                        //gives rogue stats to player
                        role = "Rogue";
                        health = 80.0f;
                        damage = 15.0f;
                        inventory.Add("Daggers");
                        break;
                    case '3':
                        //gives the wizard stats to player
                        role = "Wizard";
                        health = 60.0f;
                        damage = 20.0f;
                        inventory.Add("Staff");
                        break;
                }
                Typeout("Hello " + name + " the mighty " + role + "!");
            }
        }


        //Adds typed out look to text
        //function that prints out message one leter at a time with a wait between each letter then goes to the next line
        //i did get this off the internet and i do understand what it's saying
        static void Typeout(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(30);
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
                }
                Console.WriteLine();
            }
            return input;
        }

    }
}
