using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HelloWorld
{
    class Game
    {
        //Variables that are used through out the game
        bool _gameOver = false;
        float _playerHealth = 100.0f;
        float _playerDamage = 0.0f;
        string _playerName = " ";
        string _role = "Adventurer";

        //Adds typed out look to text
        //function that prints out message one leter at a time with a wait between each letter then goes to the next line
        //i did get this off the internet and for the most part i do understand what it's saying
        static void typewrite(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(35);
            }
            Console.WriteLine();
        }

        //function that shows player stats
        void viewStats()
        {
            Console.WriteLine("\nName: " + _playerName);
            Console.WriteLine("Role: " + _role);
            Console.WriteLine("Health: " + _playerHealth);
            Console.WriteLine("Damage: " + _playerDamage);
            Console.WriteLine("\nPress any key to continue");
            Console.Write("> ");
            Console.ReadKey();
        }

        //Function that gets player name
        void requestName()
        {
            char input = ' ';
            while (input != '1')
            {
                Console.Clear();
                typewrite("Welcome! What is your name adventurer?");
                Console.Write("> ");
                _playerName = Console.ReadLine();
                typewrite("Hello " + _playerName);
                input = GetInputTwo("Yes", "No", "Is this the name you wish to continue with?");
            }
        }

        void requestRole()
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                Console.Clear();
                input = GetInputThree("Knight", "Rogue", "Wizard", _playerName + " please select a Role.");
                if (input == '1')
                {
                    _role = "Knight";
                    _playerHealth = 100.0f;
                    _playerDamage = 10.0f;
                }
                else if (input == '2')
                {
                    _role = "Rogue";
                    _playerHealth = 80.0f;
                    _playerDamage = 15.0f;
                }
                if (input == '3')
                {
                    _role = "Wizard";
                    _playerHealth = 60.0f;
                    _playerDamage = 20.0f;
                }
                typewrite("Hello " + _playerName + " the mighty " + _role + "!");
            }
        }

        //Get player feed back when two options available
        Char GetInputTwo(string option1, string option2, string query)
        {
            char input = ' ';
            while (input != '1' && input != '2')
            {
                typewrite(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. View Stats");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar; 
                if (input == '3')
                {
                    viewStats();
                }
                Console.WriteLine();
            }
            return input;
        }

        //get player feed back when three options available 
        Char GetInputThree(string option1, string option2, string option3, string query)
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                typewrite(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. " + option3);
                Console.WriteLine("4. View Stats");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                if (input == '4')
                {
                    viewStats();
                }
                Console.WriteLine();
            }
            return input;
        }

        //Run the game
        public void Run()
        {
            Start();

            while (_gameOver == false)
            {
                Update();
            }

            End();
        }

        //Performed once when the game begins
        //used for initalizing variables 
        //also used for performing start up tasks that should only be done once
        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            typewrite("                                              ");
            typewrite("          T E X T  A D V E N T U R E          ");
            typewrite("                                              ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        //Repeated until the game ends
        //used for all game logic that will repeat
        public void Update()
        {
            requestName();
            requestRole();

            typewrite("After many years of living in the safety of your home village, " +
                "you head off on a mighty quest though unknown lands where monsters, thieves, and many other dangers live.");

            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                typewrite 
            }

                _gameOver = true;
        }

        //Performed once when the game ends
        public void End()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            typewrite("G A M E  O V E R");
        }
    }
}
