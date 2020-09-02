using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Security.Cryptography;
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
        string area = " ";

        //List that will act as the player's inventory
        List<string> inventory = new List<string> { };

        //Adds typed out look to text
        //function that prints out message one leter at a time with a wait between each letter then goes to the next line
        //i did get this off the internet and for the most part i do understand what it's saying
        static void typewrite(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(30);
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
            Console.Write("Inventory: ");
            inventory.ForEach(Console.Write);
            Console.WriteLine("\nPress any key to continue");
            Console.Write("> ");
            Console.ReadKey();
        }

        //Battle sequince that also declares enemy stats
        void startBattle(string _enemyName = "none", float _enemyHealth = 0.0f, float _enemyDamage = 0.0f)
        {
            char input = ' ';
            while (_playerHealth > 0 && _enemyHealth > 0)
            {
                Console.Clear();
                Console.WriteLine("Health: " + _playerHealth + "               " + _enemyName + " Health: " + _enemyHealth);
                Console.WriteLine("----------------------------------------------");
                input = GetInputTwo("Attack", "Defend", "What will you do?");
                if (input == '1')
                {
                    _enemyHealth -= _playerDamage;
                    typewrite("The " + _enemyName + " took " + _playerDamage + " damage!");

                }
                else if (input == '2')
                {
                    typewrite("You blocked and took less damage.");
                    _playerHealth -= _enemyDamage * 0.75f;
                    Console.Write("> ");
                    Console.ReadKey();
                    continue;
                }
                _playerHealth -= _enemyDamage;
                typewrite("You took " + _enemyDamage + " damage!");
                Console.Write("> ");
                Console.ReadKey();
            }
            if (_playerHealth <= 0.0f)
            {
                typewrite("You Died");
                _gameOver = true;
                Console.WriteLine("Press any key to continue");
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();

            }
            else if (_enemyHealth <= 0.0f)
            {
                typewrite("You survived the battle!");
                Console.WriteLine("Press any key to continue");
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();
            }
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
                    inventory.Add("Sword");
                }
                else if (input == '2')
                {
                    _role = "Rogue";
                    _playerHealth = 80.0f;
                    _playerDamage = 15.0f;
                    inventory.Add("Daggers");
                }
                if (input == '3')
                {
                    _role = "Wizard";
                    _playerHealth = 60.0f;
                    _playerDamage = 20.0f;
                    inventory.Add("Staff");
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
            Console.WriteLine("          Press any key to start");
            Console.ReadKey();
            Console.Clear();
        }

        //Repeated until the game ends
        //used for all game logic that will repeat
        public void Update()
        {
            //death means game over
            if (_playerHealth <= 0.0f)
            {
                _gameOver = true;
            }

            //Charater set up
            requestName();
            requestRole();
            Console.WriteLine("Press any key to continue");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();

            //story intro
            typewrite("After many years of living in the safety of your home village, " +
                "you head off on a mighty quest through unknown lands where monsters, thieves, and many other dangers live. " +
                "There is also untold amounts of glory and riches for those who survive and return from this perilous quest.");
            typewrite("Good Luck!");
            Console.WriteLine("Press any key to continue");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();


            //first interaction
            typewrite("You start your adventure walking down an old road outside your village and soon run into a little girl, wearing a little red hood.");
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                input = GetInputThree("Ask why she is out here", "Take an apple", "Attack", "She holds up a basket full of bright red apples toward you.");
                if (input == '1')
                {
                    input = GetInputThree("Take an apple", "Walk away", "Attack", "She doesn't say anything but holds the basket up a little higher toward you.");
                    if (input == '1')
                    {
                        typewrite("You take an apple and bite into it. The little girl smiles with large fang like teeth.");
                        typewrite("Add apple to inventory");
                        inventory.Insert(0, "Apple ,");
                        input = GetInputTwo("Run Away", "Attack", "She throws off the little red hood and turns into a large wolf!");
                        if (input == '1')
                        {
                            typewrite("You manage to get away from the wolf, but not before it swipes at your back.");
                            typewrite("Loose 10 health");
                            _playerHealth -= 10.0f;
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (input == '2')
                        {
                            if (_role == "Knight")
                            {
                                typewrite("You swipe skillfully with your sword at the wolf, scaring it and causing it to run away.");
                                Console.WriteLine("Press any key to continue");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else if (_role == "Rogue")
                            {
                                typewrite("You quickly swipe at the wolf with your daggers, scaring it and causing it to run away.");
                                Console.WriteLine("Press any key to continue");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            if (_role == "Wizard")
                            {
                                typewrite("You cast a fireball at the wolf, scaring it and causing it to run away.");
                                Console.WriteLine("Press any key to continue");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    }
                    else if (input == '2')
                    {
                        typewrite("You walk away from the little girl and continue your adventure.");
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (input == '3')
                    {
                        if (_role == "Knight")
                        {
                            typewrite("You swipe skillfully with your sword at the girl, scaring her and causing her to run away.");
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (_role == "Rogue")
                        {
                            typewrite("You quickly swipe at the girl with your daggers, scaring her and causing her to run away.");
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        if (_role == "Wizard")
                        {
                            typewrite("You cast a fireball at the girl, scaring her and causing her to run away.");
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                else if (input == '2')
                {
                    typewrite("You take an apple and bite into it. The little girl smiles with large fang like teeth.");
                    typewrite("Add apple to inventory");
                    inventory.Insert(0, "Apple ,");
                    input = GetInputTwo("Run Away", "Attack", "She throws off the little red hood and turns into a large wolf!");
                    if (input == '1')
                    {
                        typewrite("You manage to get away from the wolf, but not before it swipes at your back.");
                        typewrite("Loose 10 health");
                        _playerHealth -= 10.0f;
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (input == '2')
                    {
                        if (_role == "Knight")
                        {
                            typewrite("You swipe skillfully with your sword at the wolf, scaring it and causing it to run away.");
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (_role == "Rogue")
                        {
                            typewrite("You quickly swipe at the wolf with your daggers, scaring it and causing it to run away.");
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        if (_role == "Wizard")
                        {
                            typewrite("You cast a fireball at the wolf, scaring it and causing it to run away.");
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                if (input == '3')
                {
                    if (_role == "Knight")
                    {
                        typewrite("You swipe skillfully with your sword at the girl, scaring her and causing her to run away.");
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (_role == "Rogue")
                    {
                        typewrite("You quickly swipe at the girl with your daggers, scaring her and causing her to run away.");
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (_role == "Wizard")
                    {
                        typewrite("You cast a fireball at the girl, scaring her and causing her to run away.");
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }

            //Path diverges
            typewrite("You continue walking down the old road and it soon turns into a dirt path.");
            input = ' ';
            while (input != '1' && input != '2')
            {
                input = GetInputTwo("Left toward a Tall Grassy Meadow", "Right toward a Dark Woods", "Continuing your walk, you come to a fork in the road.");
                if (input == '1')
                {
                    typewrite("You decide to go left toward the Tall Grassy Meadow.");
                    area = "Tall Grassy Meadow";
                    Console.WriteLine("Press any key to continue");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                }
                else if (input == '2')
                {
                    typewrite("You decide to go right toward the Dark Woods,");
                    area = "Dark Woods";
                    Console.WriteLine("Press any key to continue");
                    Console.Write("> ");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            //Tall Grassy Meadow area
            //First Interaction
            while (area == "Tall Grassy Meadow")
            {
                typewrite("You're walking on a small dirt road with tall grass that goes up to your waste on either side.");
                typewrite("While walking you hear a strange noise and something moving in the tall grass.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInputThree("Talk to snake", "Run", "Attack", "A large snake slithers out the grass onto the path infront of you.");
                    if (input == '1')
                    {
                        if (_role == "Wizard")
                        {
                            typewrite("You talk to the snake and to your surprise it replies.");
                            input = GetInputTwo("Offer apple", "Attack", "The sanke tells you that it doesn't wish to fight and just wants some food.");
                            if (input == '1')
                            {
                                if (inventory.Contains("Apple ,"))
                                {
                                    typewrite("You take an apple, with a single bite in it, out of your bag and offer it to the snake.");
                                    input = GetInputTwo("Run", "Attack", "The snake shakes its head and tells you that it doesn't want to eat the apple.");
                                    if (input == '1')
                                    {
                                        typewrite("You try to run from the snake but it blocks your path.");
                                        typewrite("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        startBattle("Snake", 30.0f, 5.0f);
                                    }
                                    else if (input == '2')
                                    {
                                        typewrite("You draw your staff and prepare to fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        startBattle("Snake", 30.0f, 5.0f);
                                    }
                                }
                                else
                                {
                                    typewrite("You do not have an apple.");
                                    input = GetInputTwo("Run", "Attack", "The snake is slooking at you with hunger in its eyes.");
                                    if (input == '1')
                                    {
                                        typewrite("You try to run from the snake but it blocks your path.");
                                        typewrite("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        startBattle("Snake", 30.0f, 5.0f);
                                    }
                                    else if (input == '2')
                                    {
                                        typewrite("You draw your staff and prepare to fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        startBattle("Snake", 30.0f, 5.0f);
                                    }
                                }
                            }
                        }
                        else
                        {
                            typewrite("You try to talk to the snake, but it is a snake and you're not a wizard. " +
                                "So you cannot talk to snakes.");
                            input = GetInputTwo("Run", "Attack", "The snake coils on itself locking gaze with you.");
                            if (input == '1')
                            {
                                typewrite("You try to run from the snake but it blocks your path.");
                                typewrite("Prepare for a fight!");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                                startBattle("Snake", 30.0f, 5.0f);
                            }
                            else if (input == '2')
                            {
                                typewrite("You draw your weapon and prepare to fight!");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                                startBattle("Snake", 30.0f, 5.0f);
                            }
                        }
                    }
                    else if (input == '2')
                    {
                        typewrite("You try to run from the snake but it blocks your path.");
                        typewrite("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        startBattle("Snake", 30.0f, 5.0f);
                    }
                    if (input == '3')
                    {
                        typewrite("You draw your weapon and prepare to fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        startBattle("Snake", 30.0f, 5.0f);
                    }
                }
            }

            //Dark woods area
            //First Interaction
            while (area == "Dark Woods")
            {
                typewrite("You're walking down the shadowy, leafy covered path of the Dark Woods.");
                typewrite("When you see an old man sitting on a stump off the side of the road.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInputThree("Help him up", "Walk away", "Attack", "When you get closer, the old man asks if you will help him up.");
                    if (input == '1')
                    {
                        typewrite("You help up the old amn and realize he is blind.");
                        typewrite("He looks at you with faded eyes and thanks you. He then waves his hand over your weapon " +
                            "before vanishing into a pillar of somke.");
                        typewrite("+5 bounus damage");
                        _playerDamage += 5.0f;
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (input == '2')
                    {
                        typewrite("You ignore the old man and continue walking down the path.");
                        Console.WriteLine("Press any key to continue");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    if (input == '3')
                    {
                        if (_role == "Knight")
                        {
                            typewrite("You charge at the old man with your sword. But before you can get to him, he stands and waves his hand. " +
                                "You feel a burning sensation coming from your sword as it has turned red hot.");
                            typewrite("Take 20 Damage");
                            _playerHealth -= 20.0f;
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (_role == "Rogue")
                        {
                            typewrite("You dash at the old man with your daggers. But before you can get to him, he stands and waves his hand. " +
                                "You feel a burning sensation coming from your daggers as they have turned red hot.");
                            typewrite("Take 20 Damage");
                            _playerHealth -= 20.0f;
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        if (_role == "Wizard")
                        {
                            typewrite("You cast a fireball at the old man but nothing happens. He quickly stands and waves his hand. " +
                                "You feel a burning sensation coming from your staff as it has turned red hot.");
                            typewrite("Take 20 Damage");
                            _playerHealth -= 20.0f;
                            Console.WriteLine("Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
            }

        }

        //Performed once when the game ends
        public void End()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            typewrite("G A M E  O V E R");
        }
    }
}
