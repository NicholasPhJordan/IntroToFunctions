using System;
using System.Collections.Generic;
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
        string _role = " ";

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
                GetInputTwo("Yes", "No", "Is this the name you wish to continue with?");
            }
        }

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

        Char GetInputThree(string option1, string option2, string option3, string query)
        {
            char input = ' ';
            while (input != '1' && input != 2)
            {
                Console.WriteLine(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. " + option3);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
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
            Console.BackgroundColor = ConsoleColor.Black;
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
        }

        //Performed once when the game ends
        public void End()
        {
            
        }
    }
}
