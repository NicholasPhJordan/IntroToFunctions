﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace HelloWorld
{

    struct Item 
    {
        public string name;
    }

    class Game
    {
        //Variables that are used through out the game
        bool _gameOver = false;
        private Character _player;
        private Monster _snake;
        private Monster _crazedThief;
        string area = " ";
        private Item[] _inventory;
        private Item _staff;
        private Item _daggers;
        private Item _sword;
        private Item _apple;
        private Item _strangeCoin;

        public void Initialize()
        {
            _apple.name = "Apple";
            _strangeCoin.name = "Strange Coin";
            _snake = new Monster("Snake", 30.0f, 5.0f);
            _crazedThief = new Monster("Crazed Thief", 40.0f, 10.0f);
        }





        public Character ChooseName()
        {
            //player chooses their name 
            char input = ' ';
            while (input != '1')
            {
                Console.Clear();
                Typeout("Welcome! What is your name adventurer?");
                Console.Write("> ");
                string name = Console.ReadLine();
                Character _player = new Character(name, "Adventurer", 100.0f, 10.0f);
                Typeout("Do you wish to continue with this name?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
            return _player;
        }

        public Character ChooseRole()
        {
            //player chooses role
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                Console.Clear();
                Typeout("Please select a role.");
                Console.WriteLine("1. Knight");
                Console.WriteLine("2. Rogue");
                Console.WriteLine("3. Wizard");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                if (input == '1')
                {
                    //gives knight stats to player
                    Character _player = new Knight(_player.GetName(), "Knight", 100.0f, 10.0f);
                    _sword.name = "Sword";
                    AddItemToInventory(_sword, 0);
                }
                else if (input == '2')
                {
                    //gives rogue stats to player
                    Character _player = new Rogue(_player.GetName(), "Rougue", 90.0f, 10.0f, 100.0f);
                    _daggers.name = "Daggers";
                    AddItemToInventory(_daggers, 0);
                }
                if (input == '3')
                {
                    //gives the wizard stats to player
                    Character _player = new Wizard(_player.GetName(), "Wizard", 80.0f, 10.0f, 100.0f);
                    _staff.name = "Staff";
                    AddItemToInventory(_staff, 0);
                }
            }
            return _player;
        }

        //Function that gets player name and checks if they want to keep it
        public Character CreateCharacter()
        {
            ChooseName();
            ChooseRole();
            return _player;
        }




        //Adds typed out look to text
        //function that prints out message one leter at a time with a wait between each letter then goes to the next line
        //i did get this off the internet and for the most part i do understand what it's saying
        static void Typeout(string message)
        {
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                System.Threading.Thread.Sleep(30);
            }
            Console.WriteLine();
        }

        //Battle sequince that also declares enemy stats
        //takes enemy name health and damage
        void StartBattle(Monster monster)
        {
            char input = ' ';
            while (_player.GetHealth() > 0 && monster.GetHealth() > 0)
            {
                Console.Clear();
                Console.WriteLine("Health: " + _player.GetHealth() + "               " + monster.GetName() + " Health: " + monster.GetHealth());
                Console.WriteLine("----------------------------------------------");
                input = GetInput("Attack", "Defend", "What will you do?");
                if (input == '1')
                {
                    //attack with full damage and accept full damage
                    _player.Attack(monster);
                    Typeout("The " + monster.GetName() + " took " + monster.GetDamage() + " damage!");
                    //enemy attack
                    _player.TakeDamage(monster.GetDamage());
                    Typeout("You took " + monster + " damage!");
                    Console.Write("> ");
                    Console.ReadKey();

                }
                else if (input == '2')
                {
                    //deal no damage but take less damage from enemy
                    Typeout("You blocked and took less damage.");
                    _player.TakeDamage(monster.GetDamage() * 0.25f);
                    Console.Write("> ");
                    Console.ReadKey();
                }
            }
            //conslusion of fight 
            if (_player.GetHealth() <= 0.0f)
            {
                //if player looses
                Typeout("You Died");
                Console.WriteLine("Press any key to continue");
                ClearScreen();
                _gameOver = true;
            }
            else if (monster.GetHealth() <= 0.0f)
            {
                //if player wins
                Typeout("You survived the battle!");
                ClearScreen();
            }
        }

        //Prints the player's inventory 
        public void PrintInventory(Item[] inventory)
        {
            Console.Write("\n");
            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + inventory[i].name);
            }
        }

        //allows us to add items to inventory
        public void AddItemToInventory(Item item, int index)
        {
            _inventory[index] = item;
        }

        //checks player inventory for specific items
        public bool Contains(int itemIndex)
        {
            if (itemIndex > 0 && itemIndex < _inventory.Length)
            {
                return true;
            }
            return false;
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
                    _player.ViewStats();
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
                    _player.ViewStats();
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

        /////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        //Performed once when the game begins
        //used for initalizing variables 
        //also used for performing start up tasks that should only be done once
        public void Start()
        {
            //initializes items and monsters for the game to use
            Initialize();

            //opening "menu"
            Console.BackgroundColor = ConsoleColor.DarkGreen; //changes background to dark green
            Console.ForegroundColor = ConsoleColor.White; //changes text to white
            Typeout("                                              ");
            Typeout("          T E X T  A D V E N T U R E          ");
            Typeout("                                              ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green; //changes text to green
            Console.WriteLine("          Press any key to start");
            Console.ReadKey();
            Console.Clear();
        }

        //Repeated until the game ends
        //used for all game logic that will repeat
        public void Update()
        {
            //Charater set up
            _player = CreateCharacter();
            ClearScreen();

            //story intro
            Typeout("After many years of living in the saftey of your village, you've never had your skills tested as you will on the upcoming task. " +
                "A Drake has moved into the local area and causing havoc. Thieves, monsters, and many other troubles have come out of hiding with this new threat looming over. " +
                "It is now up to you and you alone to save everyone! ");
            Typeout("Good Luck!");
            ClearScreen();

            //first interaction
            Typeout("You start your adventure walking down an old road outside your village and soon run into a little girl, wearing a little red hood.");
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                input = GetInput("Ask why she is out here", "Take an apple", "Attack", "She holds up a basket full of bright red apples toward you.");
                if (input == '1')
                {
                    input = GetInput("Take an apple", "Walk away", "Attack", "She doesn't say anything but holds the basket up a little higher toward you.");
                    if (input == '1')
                    {
                        Typeout("You take an apple and bite into it. The little girl smiles with large fang like teeth.");
                        Typeout("Add Apple to inventory");
                        AddItemToInventory(_apple, 1);
                        input = GetInput("Run Away", "Attack", "She throws off the little red hood and turns into a large wolf!");
                        if (input == '1')
                        {
                            Typeout("You manage to get away from the wolf, but not before it swipes at your back.");
                            Typeout("Loose 10 health");
                            _player.TakeDamage(10.0f);
                            ClearScreen();
                        }
                        else if (input == '2')
                        {
                            switch (_player.GetRole())
                            {
                                case "Knight":
                                    Typeout("You swipe skillfully with your sword at the wolf, scaring it and causing it to run away.");
                                    ClearScreen();
                                    break;
                                case "Rogue":
                                    Typeout("You quickly swipe at the wolf with your daggers, scaring it and causing it to run away.");
                                    ClearScreen();
                                    break;
                                case "Wizard":
                                    Typeout("You cast a fireball at the wolf, scaring it and causing it to run away.");
                                    ClearScreen();
                                    break;
                            }
                        }
                    }
                    else if (input == '2')
                    {
                        Typeout("You walk away from the little girl and continue your adventure.");
                        ClearScreen();
                    }
                    if (input == '3')
                    {
                        switch (_player.GetRole())
                        {
                            case "Knight":
                                Typeout("You swipe skillfully with your sword at the girl, scaring her and causing her to run away.");
                                ClearScreen();
                                break;
                            case "Rogue":
                                Typeout("You quickly swipe at the girl with your daggers, scaring her and causing her to run away.");
                                ClearScreen();
                                break;
                            case "Wizard":
                                Typeout("You cast a fireball at the girl, scaring her and causing her to run away.");
                                ClearScreen();
                                break;
                        }
                    }
                }
                else if (input == '2')
                {
                    Typeout("You take an apple and bite into it. The little girl smiles with large fang like teeth.");
                    Typeout("Add Apple to inventory");
                    AddItemToInventory(_apple, 1);
                    input = GetInput("Run Away", "Attack", "She throws off the little red hood and turns into a large wolf!");
                    if (input == '1')
                    {
                        Typeout("You manage to get away from the wolf, but not before it swipes at your back.");
                        Typeout("Loose 10 health");
                        _player.TakeDamage(10.0f);
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        switch (_player.GetRole())
                        {
                            case "Knight":
                                Typeout("You swipe skillfully with your sword at the wolf, scaring it and causing it to run away.");
                                ClearScreen();
                                break;
                            case "Rogue":
                                Typeout("You quickly swipe at the wolf with your daggers, scaring it and causing it to run away.");
                                ClearScreen();
                                break;
                            case "Wizard":
                                Typeout("You cast a fireball at the wolf, scaring it and causing it to run away.");
                                ClearScreen();
                                break;
                        }
                    }
                }
                if (input == '3')
                {
                    switch (_player.GetRole())
                    {
                        case "Knight":
                            Typeout("You swipe skillfully with your sword at the girl, scaring her and causing her to run away.");
                            ClearScreen();
                            break;
                        case "Rogue":
                            Typeout("You quickly swipe at the girl with your daggers, scaring her and causing her to run away.");
                            ClearScreen();
                            break;
                        case "Wizard":
                            Typeout("You cast a fireball at the girl, scaring her and causing her to run away.");
                            ClearScreen();
                            break;
                    }
                }
            }

            //Path diverges
            Typeout("You continue walking down the old road and it soon turns into a dirt path.");
            input = ' ';
            while (input != '1' && input != '2')
            {
                input = GetInput("Left toward a Tall Grassy Meadow", "Right toward a Dark Woods", "Continuing your walk, you come to a fork in the road.");
                if (input == '1')
                {
                    Typeout("You decide to go left toward the Tall Grassy Meadow.");
                    area = "Tall Grassy Meadow";
                    ClearScreen();
                }
                else if (input == '2')
                {
                    Typeout("You decide to go right toward the Dark Woods,");
                    area = "Dark Woods";
                    ClearScreen();
                }
            }

            //Tall Grassy Meadow area
            //First Interaction
            while (area == "Tall Grassy Meadow")
            {
                Typeout("You're walking on a small dirt road with tall grass that goes up to your waste on either side.");
                Typeout("While walking you hear a strange noise and something moving in the tall grass.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Talk to snake", "Run", "Attack", "A large snake slithers out the grass onto the path infront of you.");
                    if (input == '1')
                    {
                        if (_player.GetRole() == "Wizard")
                        {
                            Typeout("You talk to the snake and to your surprise it replies.");
                            input = GetInput("Offer apple", "Attack", "The sanke tells you that it doesn't wish to fight and just wants some food.");
                            if (input == '1')
                            {
                                if (Contains(1))
                                {
                                    Typeout("You take an apple, with a single bite in it, out of your bag and offer it to the snake.");
                                    input = GetInput("Run", "Attack", "The snake shakes its head and tells you that it doesn't want to eat the apple.");
                                    if (input == '1')
                                    {
                                        Typeout("You try to run from the snake but it blocks your path.");
                                        Typeout("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        StartBattle(_snake);
                                    }
                                    else if (input == '2')
                                    {
                                        Typeout("You draw your staff and prepare to fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        StartBattle(_snake);
                                    }
                                }
                                else
                                {
                                    Typeout("You do not have an apple.");
                                    input = GetInput("Run", "Attack", "The snake is slooking at you with hunger in its eyes.");
                                    if (input == '1')
                                    {
                                        Typeout("You try to run from the snake but it blocks your path.");
                                        Typeout("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        StartBattle(_snake);
                                    }
                                    else if (input == '2')
                                    {
                                        Typeout("You draw your staff and prepare to fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        StartBattle(_snake);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Typeout("You try to talk to the snake, but it is a snake and you're not a wizard. " +
                                "So you cannot talk to snakes.");
                            input = GetInput("Run", "Attack", "The snake coils on itself locking gaze with you.");
                            if (input == '1')
                            {
                                Typeout("You try to run from the snake but it blocks your path.");
                                Typeout("Prepare for a fight!");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                                StartBattle(_snake);
                            }
                            else if (input == '2')
                            {
                                Typeout("You draw your weapon and prepare to fight!");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                                StartBattle(_snake);
                            }
                        }
                    }
                    else if (input == '2')
                    {
                        Typeout("You try to run from the snake but it blocks your path.");
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_snake);
                    }
                    if (input == '3')
                    {
                        Typeout("You draw your weapon and prepare to fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_snake);
                    }
                }

                //path diverges
                Typeout("You carry on after your battle, walking through the tall grass. When you come to a bend in the path. " +
                    "You see a camp fire off the path to the left and a creek further down the trail to the right.");
                input = ' ';
                while (input != '1' && input != '2')
                {
                    input = GetInput("Left towrd Camp", "Right toward Creek", "Do you go toward the Camp or the Creek?");
                    if (input == '1')
                    {
                        Typeout("You decide to go toward the Camp.");
                        area = "Camp";
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        Typeout("You decide to go toward the Creek.");
                        area = "Creek";
                        ClearScreen();
                    }
                }

            }

            //Dark woods area
            //First Interaction
            while (area == "Dark Woods")
            {
                Typeout("You're walking down the shadowy, leafy covered path of the Dark Woods.");
                Typeout("When you see an old man sitting on a stump off the side of the road.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Help him up", "Walk away", "Attack", "When you get closer, the old man asks if you will help him up.");
                    if (input == '1')
                    {
                        Typeout("You help up the old amn and realize he is blind.");
                        Typeout("He looks at you with faded eyes and thanks you. He then gives you a strange golden coin " +
                            "before vanishing into a pillar of somke.");
                        Typeout("Add Strange Coin to inventory");
                        AddItemToInventory(_strangeCoin, 2);
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        Typeout("You ignore the old man and continue walking down the path.");
                        ClearScreen();
                    }
                    if (input == '3')
                    {
                        switch (_player.GetRole())
                        {
                            case "Knight":
                                Typeout("You charge at the old man with your sword. But before you can get to him, he stands and waves his hand. " +
                                "You feel a burning sensation coming from your sword as it has turned red hot.");
                                Typeout("Loose 20 Health");
                                _player.TakeDamage(20.0f);
                                ClearScreen();
                                break;
                            case "Rogue":
                                Typeout("You dash at the old man with your daggers. But before you can get to him, he stands and waves his hand. " +
                                "You feel a burning sensation coming from your daggers as they have turned red hot.");
                                Typeout("Loose 20 Health");
                                _player.TakeDamage(20.0f);
                                ClearScreen();
                                break;
                            case "Wizard":
                                Typeout("You cast a fireball at the old man but nothing happens. He quickly stands and waves his hand. " +
                                "You feel a burning sensation coming from your staff as it has turned red hot.");
                                Typeout("Loose 20 Health");
                                _player.TakeDamage(20.0f);
                                ClearScreen();
                                break;
                        }
                    }
                }

                //second interaction
                Typeout("You continue on the dark trail and soon enter an opening in the middle of the Dark Woods. " +
                    "You stop and start to hear noises. Something is moving all around you.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Call out", "Run", "Attack", "The noise suddenly stops infront of you behind some bushes.");
                    if (input == '1')
                    {
                        Typeout("You call out into the open.");
                        input = GetInput("Demand they come closer", "Run", "Attack", "A low voice replies saying hello, followed by some psychotic laughter.");
                        if (input == '1')
                        {
                            Typeout("You demand that whoever it is comes out of the bushes and into the opening. " +
                                "They follow your wishes and a shadowy figure comes out of the bushes. Before you stands a tall thin man with large daggers on his hips.");
                            if (_player.GetRole() == "Rogue")
                            {
                                input = GetInput("Use thieves cant", "Run", "Attack", "You notice a fellow thief when you see one.");
                                if (input == '1')
                                {
                                    Typeout("You try to use thieves cant to show that you are a friend, but he does not care. He says he has no frinds, " +
                                        "followd by some psychotic laughter.");
                                    input = GetInput("Run", "Attack", "He puts his hands on his daggers.");
                                    if (input == '1')
                                    {
                                        Typeout("You try to run for it realizing this guy is CRAZY, but he jumps infront of you cutting you across the chest!");
                                        Typeout("Loose 10 Health");
                                        _player.TakeDamage(20.0f);
                                        Typeout("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        StartBattle(_crazedThief);
                                    }
                                    else if (input == '2')
                                    {
                                        Typeout("You don't wait for him to attack first and dash at him with your daggers!");
                                        Typeout("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        StartBattle(_crazedThief);
                                    }
                                }
                                else if (input == '2')
                                {
                                    Typeout("You try to run for it, but he jumps infront of you cutting you across the chest!");
                                    Typeout("Loose 10 Health");
                                    _player.TakeDamage(10.0f);
                                    Typeout("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    StartBattle(_crazedThief);
                                }
                                if (input == '3')
                                {
                                    Typeout("You don't wait for him to get a chance to attack first and dash at him with your daggers!");
                                    Typeout("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    StartBattle(_crazedThief);
                                }
                            }
                            else
                            {
                                input = GetInput("Reason with", "Run", "Attack", "He looks at you with a crazed look in his eyes.");
                                if (input == '1')
                                {
                                    Typeout("You starts to say something in a gentel voice, but the man dashes at you with his daggers cutting you across the chest!");
                                    Typeout("Loose 10 Health");
                                    _player.TakeDamage(10.0f);
                                    Typeout("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    StartBattle(_crazedThief);
                                }
                                else if (input == '2')
                                {
                                    Typeout("You try to run for it, but he jumps infront of you cutting you across the chest!");
                                    Typeout("Loose 10 Health");
                                    _player.TakeDamage(10.0f);
                                    Typeout("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    StartBattle(_crazedThief);
                                }
                                else if (input == '3')
                                {
                                    Typeout("You don't wait for him to go crazy and attack!");
                                    Typeout("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    StartBattle(_crazedThief);
                                }
                            }
                        }
                        else if (input == '2')
                        {
                            Typeout("You take a run for it when a tall, thin man jumps out infront of you with daggers in each hand.");
                            Typeout("He cuts you across the chest!");
                            Typeout("Loose 10 Health");
                            _player.TakeDamage(10.0f);
                            Typeout("Prepare for a fight!");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                            StartBattle(_crazedThief);
                        }
                        if (input == '3')
                        {
                            Typeout("You attack blindly at the bush. A scream escapes from the bush and a tall, thin man jumps out with daggers ready!");
                            Typeout("Prepare for a fight!");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                            StartBattle(_crazedThief);
                        }
                    }
                    else if (input == '2')
                    {
                        Typeout("You make a run for it from these spooky noises when a tall, thin  man, with daggers in each hand, jumps out from behind the bushes.");
                        Typeout("He cuts you across the chest!");
                        Typeout("Loose 10 Health");
                        _player.TakeDamage(10.0f);
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_crazedThief);
                    }
                    if (input == '3')
                    {
                        Typeout("You attack blindly at the bush. A scream escapes from the bush and a tall, thin man jumps out with daggers ready!");
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_crazedThief);
                    }
                }

                //Path diverges
                Typeout("After the fight, you continue on the path. Some times goes by and you come to a bend in the path.");
                Typeout("The path goes toward a lighter and less dense part of the woods, " +
                    "but you also notice a covered path that leads even deeper into the Dark Woods.");
                input = ' ';
                while (input != '1' && input != '2')
                {
                    input = GetInput("Left toward the Dark Woods", "Right toward the Light Woods",
                        "Do you go toward the Light Woods or head deeper into the Dark Woods?");
                    if (input == '1')
                    {
                        Typeout("You decide to go further into the Dark Woods");
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        Typeout("You decide to head towrd the Light Woods");
                        area = "Light Woods";
                        ClearScreen();
                    }
                }

            }

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Performed once when the game ends
        public void End()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Typeout("G A M E  O V E R");
        }
    }
}
