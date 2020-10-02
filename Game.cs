using System;
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
        public int cost;
    }

    class Game
    {
        //Variables that are used through out the game
        bool _gameOver = false;
        private Shop _shop;
        private Character _player;
        private Monster _snake;
        private Monster _crazedThief;
        private Monster _goblins;
        private Monster _wolf;
        private Monster _dragon;
        string area = " ";
        private Item[] _inventory;
        private Item _staff;
        private Item _daggers;
        private Item _sword;
        private Item _apple;
        private Item _strangeCoin;
        private Item[] _shopInventory;
        private Item _arrow;
        private Item _shield;
        private Item _bow;

        public void Initialize()
        {
            _apple.name = "Apple";
            _strangeCoin.name = "Strange Coin";
            _snake = new Monster("Snake", 30.0f, 5.0f, "simple");
            _crazedThief = new Monster("Crazed Thief", 40.0f, 10.0f, "simple");
            _goblins = new Monster("Goblins", 50.0f, 10.0f, "simple");
            _wolf = new Monster("Wolf", 60.0f, 15.0f, "simple");
            _dragon = new Monster("Dragon", 100.0f, 20.0f, "boss");
            _inventory = new Item[5];
            _arrow.name = "Arrow";
            _arrow.cost = 1;
            _shield.name = "Shield";
            _shield.cost = 1;
            _bow.name = "Bow";
            _bow.cost = 1;
        }

        //funtion that takes player input to create character
        public Character CreateCharacter()
        {
            //player chooses their name 
            char input = ' ';
            while (input != '1')
            {
                Console.Clear();
                Typeout("Welcome! What is your name adventurer?");
                Console.Write("> ");
                string name = Console.ReadLine();
                //player chooses role
                string role = "";
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
                        role = "Knight";
                        _player = new Knight(name, role, 100.0f, 10.0f, 20, 100.0f);
                        _sword.name = "Sword";
                        AddItemToInventory(_sword, 0);
                    }
                    else if (input == '2')
                    {
                        //gives rogue stats to player
                        role = "Rogue";
                        _player = new Rogue(name, role, 90.0f, 10.0f, 20, 100.0f);
                        _daggers.name = "Daggers";
                        AddItemToInventory(_daggers, 0);
                    }
                    if (input == '3')
                    {
                        //gives the wizard stats to player
                        role = "Wizard";
                        _player = new Wizard(name, role, 80.0f, 10.0f, 20, 100.0f);
                        _staff.name = "Staff";
                        AddItemToInventory(_staff, 0);
                    }
                }
                Console.WriteLine();
                input = ' ';
                Typeout("Is this the character you wish to play?");
                Typeout(name + " the mighty " + role);
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            return _player;
        }

        //I did get this off the internet and I do understand what it's saying
        //Adds typed out look to text
        //function that takes a string and prints out one character at a time with a slight pause between each character then prints a new line
        //https://stackoverflow.com/questions/25337336/how-to-make-text-be-typed-out-in-console-application site i got info from
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
                    //enemy attack
                    _player.TakeDamage(monster.GetDamage());
                    Console.WriteLine("\nYou attack and so does the " + monster.GetName() + "!");
                    Console.ReadKey();
                }
                else if (input == '2')
                {
                    //deal no damage but take less damage from enemy
                    _player.TakeDamage(monster.GetDamage() * 0.25f);
                    Console.WriteLine(monster.GetName() + " attacks but you took less damage!");
                    Console.Write("> ");
                    Console.ReadKey();
                }
            }
            //conslusion of fight 
            if (_player.GetHealth() <= 0.0f)
            {
                //if player looses
                if (monster.GetType() == "boss")
                {
                    //adds extra flare to dying by dragon
                    Typeout("You were slain by the mighty dragon! The fate of the world is lost.");
                    ClearScreen();
                    _gameOver = true;
                    area = "";
                }
                else
                {
                    Typeout("\nYou Died");
                    ClearScreen();
                    _gameOver = true;
                    area = "";

                }
            }
            else if (monster.GetHealth() <= 0.0f)
            {
                //if player wins
                if (monster.GetType() == "boss")
                {
                    //adds some extra flare for killing the dragon dragon
                    Typeout("You killed the mighty dragon! Your quest is complete!.");
                    ClearScreen();
                }
                else
                {
                    Typeout("\nYou survived the battle!");
                    ClearScreen();
                }
            }
        }

        //Prints the player's inventory 
        public void PrintInventory(Item[] inventory)
        {
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
                    Console.WriteLine();
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

        //get player feed back when four options available 
        Char GetInput(string option1, string option2, string option3, string option4, string query)
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3' && input != '4')
            {
                Typeout(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. " + option3);
                Console.WriteLine("4. " + option4);
                Console.WriteLine("5. View Stats");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                if (input == '5')
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

        //Code from the ShopRPG Not doing a shop for my final project. Just wanted a shop in my game
        //////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool Buy(Item item, int index)
        {
            if (_player.GetGold() > item.cost)
            {
                _player._gold -= item.cost;
                _inventory[index] = item;
                return true;
            }
            return false;
        }

        private void OpenShopMenu()
        {
            //Print a welcome message and all the choices to the screen
            Console.WriteLine("Welcome! What would you like dearie. The old woman behind the counter says.");
            PrintInventory(_shopInventory);

            //Get player input
            char input = Console.ReadKey().KeyChar;

            //Set itemIndex to be the indec the player selected
            int itemIndex = -1;
            switch (input)
            {
                case '1':
                    {
                        itemIndex = 0;
                        break;
                    }
                case '2':
                    {
                        itemIndex = 1;
                        break;
                    }
                case '3':
                    {
                        itemIndex = 2;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            //If the player can't afford the item print a message to let them know
            if (_player.GetGold() < _shopInventory[itemIndex].cost)
            {
                Console.WriteLine("You cant afford this.");
                return;
            }

            //Ask the player to replace a slot in their own inventory
            Console.WriteLine("Choose a slot to replace.");
            PrintInventory(_inventory);
            //Get player input
            input = Console.ReadKey().KeyChar;

            //Set the value of the playerIndex based on the player's choice
            int playerIndex = -1;
            switch (input)
            {
                case '1':
                    {
                        playerIndex = 0;
                        break;
                    }
                case '2':
                    {
                        playerIndex = 1;
                        break;
                    }
                case '3':
                    {
                        playerIndex = 2;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            //Sell item to player and replace the weapon at the index with the newly purchased weapon
            _shop.Sell(_player, itemIndex, playerIndex);
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
            _shopInventory = new Item[] { _arrow, _shield, _bow };
            _shop = new Shop(_shopInventory);

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
                "A Dragon has moved into the local area and causing havoc. Thieves, monsters, and many other troubles have come out of hiding with this new threat looming over. " +
                "It is now up to you and you alone to save everyone! ");
            Typeout("Good Luck!");
            ClearScreen();

            //first interaction
            /////LITTLE RED HOOD/////
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
                        Typeout("[Add Apple to inventory]");
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
                    Typeout("[Add Apple to inventory]");
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
            /////LARGE SNAKE/////
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
                                if (_inventory.Equals(_apple))
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

            //camp area
            while (area == "Camp")
            {
                Typeout("It doesn't take long to come across a clearing in the field where three people have set up camp.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Approach", "Attack", "Walk Away", "As you approach the camp you see two tall burly men and one, even larger, women sitting by a fire.");
                    if (input == '1')
                    {
                        Typeout("You slowly approach the three and they all stand with their weapons drawn.");
                        input = GetInput("Sleep", "Food", "Walk Away", "The woman asks what you want in a low raspy voice.");
                        if (input == '1')
                        {
                            Typeout("You ask for a place to sleep. The woman nods at the men. They put away their swrods and tell you that you can sleep right by the fire. " +
                                "They enter their tents.");
                            ClearScreen();
                            Typeout("You wake up to the cold air and realize the three have left sometime in the night. " +
                                "You feel rejuvenated for the long day ahead of you.");
                            switch (_player.GetRole())
                            {
                                case "Knight":
                                    _player.Heal(100.0f);
                                    ClearScreen();
                                    area = "Creek";
                                    break;
                                case "Rogue":
                                    _player.Heal(90.0f);
                                    ClearScreen();
                                    area = "Creek";
                                    break;
                                case "Wizard":
                                    _player.Heal(80.0f);
                                    ClearScreen();
                                    area = "Creek";
                                    break;
                            }
                        }
                        else if (input == '2')
                        {
                            Typeout("You ask for food. The woman nods at the men. They put away their swords and tell you to join them. " +
                                "Yall talk hours and start to eat and drink. One by one yall passout.");
                            ClearScreen();
                            Typeout("You wake up to the cold air and realize the three have left sometime in the night. " +
                                "You feel like you made some friends and rejuvenated for the long day ahead of you.");
                            switch (_player.GetRole())
                            {
                                case "Knight":
                                    _player.Heal(100.0f);
                                    ClearScreen();
                                    area = "Creek";
                                    break;
                                case "Rogue":
                                    _player.Heal(90.0f);
                                    ClearScreen();
                                    area = "Creek";
                                    break;
                                case "Wizard":
                                    _player.Heal(80.0f);
                                    ClearScreen();
                                    area = "Creek";
                                    break;
                            }
                        }
                        if (input == '3')
                        {
                            Typeout("You descide it's better to walk away and go back toward the creek.");
                            ClearScreen();
                            area = "Creek";
                        }
                    }
                    else if (input == '2')
                    {
                        switch (_player.GetRole())
                        {
                            case "Knight":
                                Typeout("You charge at them with your sword! The woman stand quickly and blocks your sword as the other two stab and cut into you! " +
                                    "You lay on the ground hearing them laugh at you.");
                                ClearScreen();
                                _gameOver = true;
                                area = " ";
                                break;
                            case "Rogue":
                                Typeout("You dash at them quickly with your daggers! The two men stand fast and block your blows, before you can back up the woman stabs you in the chest!" +
                                    "You lay on the ground hearing them laugh at you.");
                                ClearScreen();
                                _gameOver = true;
                                area = " ";
                                break;
                            case "Wizard":
                                Typeout("You quickly cast a spell taking out one of the men! The other two come at you quickly! " +
                                    "While you manage to fight the other man off, the woman comes from behind and stabs you in the chest! " +
                                    "She congragulates you for being able to take them down as you lay on the ground.");
                                ClearScreen();
                                _gameOver = true;
                                area = " ";
                                break;
                        }
                    }
                    if (input == '3')
                    {
                        Typeout("You decide it's better to walk away and go back toward the creek.");
                        ClearScreen();
                        area = "Creek";
                    }
                }
            }

            //Creek area
            while (area == "Creek")
            {
                //first inter action with bridge
                Typeout("You're walking toward the creek and see a small bridge that passes over it. As you get to the bridge, you notice the top of it is missing " +
                    "and there is a big gap from one side of the bridge to the other.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Jump the gap", "Walk through creek", "Find anohter way", "What do you do?");
                    if (input == '1')
                    {
                        Typeout("You try to jump the gap to the other side of the bridge and miss. You fall into the creek and realize it is very shallow. " +
                            "You walk to the other side of the creek.");
                    }
                    else if (input == '2')
                    {
                        Typeout("You get off the bridge and notice the creek is actually rather shallow. You walk through the creek.");
                    }
                    if (input == '3')
                    {
                        Typeout("You decide to walk along the creek and look for another way over.");
                    }
                }
                ClearScreen();

                //second interaction with faries
                if (input == '3')
                {
                    Typeout("You walk along the creek when you hear little voices playing in the water. As you get closer, you see two little faries playing in the water. " +
                        "The faries see you and fly over to you.");
                    input = GetInput("Passage over creek", "Tell them to shut up", "In high pitched voices they ask, what do you want?");
                    if (input == '1')
                    {
                        Typeout("You ask if they can help you over the creek and with happy little cheers, they pick you up by the sholders and carry you to the other side of the creek. " +
                            "They wish you luck and hope you have a great journey");
                        Typeout("You continue down the path and soon come to a village. You walk up to its big doors and enter the village.");
                        ClearScreen();
                        area = "Village";
                    }
                    else if (input == '2')
                    {
                        Typeout("You tell the faries to shut their little mouths. They get very angry and push you into the creek. Then they fly away. " +
                            "\nYou notice the creek is very shallow and decide to walk to the other side.");
                        Typeout("You continue down the path and soon come to a village. You walk up to its big doors and enter the village.");
                        ClearScreen();
                        area = "Village";
                    }
                }
                else
                {
                    Typeout("You walk along the path which follows the creek. At one point you notice two faries playing in the water near the other side of the creek. " +
                        "Since you already crossed the creek, you ignore them and continue forward.");
                    Typeout("You continue down the path and soon come to a village. You walk up to its big doors and enter the village.");
                    ClearScreen();
                    area = "village";
                }
            }

            //Dark woods area
            while (area == "Dark Woods")
            {
                //First Interaction
                /////OLD MAN/////
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
                        Typeout("[Add Strange Coin to inventory]");
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
                /////CRAZED THIEF/////
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

                /////LARGE HOUSE/////
                Typeout("You're walking down the leafy path as it start to turn to night fall. Once dark you come to the end of the path. " +
                    "Before you is a large, two story, wooden house, dimly lit on the inside with a porch infront.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                FrontOfHouse:
                    input = GetInput("Leave", "Walk around the house ", "Knock on the door", "What do you do?");
                    if (input == '1')
                    {
                        Typeout("You decide to walk back and continue on the path towards the Light Woods");
                        area = "Light Woods";
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        Typeout("You walk around the house, hesitant to go in. You see many windows, each look dimly lit like the one before, but each too foggy to see into. " +
                            "You see nothing else of interest as you come back to the front of the house.");
                        ClearScreen();
                        goto FrontOfHouse;
                    }
                    if (input == '3')
                    {
                        Typeout("You walk up to the hosue and knock on the door.");
                        Typeout("There is no response.");
                        input = GetInput("Go Inside", "Leave", "What do you do?");
                        if (input == '1')
                        {
                            ClearScreen();
                            Typeout("You open the door. Inside is an empty house that is suddenly dark.");
                            input = ' ';
                            while (input != '1' && input != '2' && input != '3' && input != '4')
                            {
                            Entrance:
                                Typeout("Infront of you is a doorway to your left, a doorway to your right, and a long hallway with stairs on the right side."); ;
                                input = GetInput("Left Doorway", "Right Doorway", "Upstairs", "Leave", "Where do you go");
                                if (input == '1')
                                {
                                    Typeout("You go through the left doorway and enter into a small room with another doorway at the end. You assume this to be the dinning room.");
                                    input = GetInput("Other Doorway", "Entrance", "Where do you go?");
                                    if (input == '1')
                                    {
                                        Typeout("You walk through the empty room to the other doorway and enter a kitchen. There seems to be nothing of interest in here. " +
                                            "No food or anything out of the ordinary. There is another doorway that leads to the far end of the long hallway you entered in.");
                                        ClearScreen();
                                        Typeout("You are now back at the front of the hosue.");
                                        goto Entrance;
                                    }
                                    else if (input == '2')
                                    {
                                        Typeout("You are now back at the front of the hosue.");
                                        ClearScreen();
                                        goto Entrance;
                                    }
                                }
                                else if (input == '2')
                                {
                                    Typeout("You go through the right doorway and enter a long room with a fireplace and empty shelves. You assume this to be the living room. " +
                                        "There is nothing else in this room.");
                                    ClearScreen();
                                    Typeout("You are now back at the front of the house.");
                                    goto Entrance;
                                }
                                else if (input == '3')
                                {
                                TopOfStairs:
                                    Typeout("You go upstairs and see a long hallway with three doors.");
                                    input = ' ';
                                    while (input != '1' && input != '2' && input != '3' && input != '4')
                                    {
                                        input = GetInput("Door One", "Door Two", "Door Three", "Go back downstairs", "Where do you go?");
                                        if (input == '1')
                                        {
                                            input = GetInput("Yes, sleep in bed", "NO, go back", "You enter a small room with a small bed. Would you like to sleep in the bed?");
                                            if (input == '1')
                                            {
                                                Typeout("You lay down in the bed and soon start to doze off. Right before you fall asleep, you see a figure standing in the doorway.");
                                                ClearScreen();
                                                Typeout("You wake up with blurry vision in a tub filled with warm water. An old lady is standing next to you reading from a large " +
                                                    "book when she notices you're awake." +
                                                    "She bends down next to you and with a raspy voice tells you to be calm as she puts a rag over your face. " +
                                                    "This is the last thing you feel before your eyes close again.");
                                                _gameOver = true;
                                                area = "";
                                            }
                                            else if (input == '2')
                                            {
                                                goto TopOfStairs;
                                            }
                                        }
                                        else if (input == '2')
                                        {
                                            input = GetInput("Yes, sleep in bed", "NO, go back", "You enter a small room with a small bed. Would you like to sleep in the bed?");
                                            if (input == '1')
                                            {
                                                Typeout("You lay down in the bed and soon start to doze off. Right before you fall asleep, you see a figure standing in the doorway.");
                                                ClearScreen();
                                                Typeout("You wake up with blurry vision in a tub filled with warm water. An old lady is standing next to you reading from a large " +
                                                    "book when she notices you're awake." +
                                                    "She bends down next to you and with a raspy voice tells you to be calm as she puts a rag over your face. " +
                                                    "This is the last thing you feel before your eyes close again.");
                                                _gameOver = true;
                                                area = "";
                                            }
                                            else if (input == '2')
                                            {
                                                goto TopOfStairs;
                                            }
                                        }
                                        else if (input == '3')
                                        {
                                            input = GetInput("Yes, sleep in bed", "NO, go back", "You enter a large room with a large bed. Would you like to sleep in the bed?");
                                            if (input == '1')
                                            {
                                                Typeout("You lay down in the bed and soon start to doze off. Right before you fall asleep, you see a figure standing in the doorway.");
                                                ClearScreen();
                                                Typeout("You wake up with blurry vision in a tub filled with warm water. An old lady is standing next to you reading from a large " +
                                                    "book when she notices you're awake." +
                                                    "She bends down next to you and with a raspy voice tells you to be calm as she puts a rag over your face. " +
                                                    "This is the last thing you feel before your eyes close again.");
                                                _gameOver = true;
                                                area = "";
                                            }
                                            else if (input == '2')
                                            {
                                                goto TopOfStairs;
                                            }
                                        }
                                        if (input == '4')
                                        {
                                            Typeout("You go back downstairs and get a strange feeling. Before you can do anything, you feel something hit the back of your head");
                                            ClearScreen();
                                            Typeout("You wake up with blurry vision in a tub filled with warm water. An old lady is standing next to you reading from a large " +
                                                "book when she notices you're awake." +
                                                "She bends down next to you and with a raspy voice tells you to be calm as she puts a rag over your face. " +
                                                "This is the last thing you feel before your eyes close again.");
                                            _gameOver = true;
                                            area = "";
                                        }
                                    }
                                }
                                if (input == '4')
                                {
                                    Typeout("You Leave the hosue and decide to walk back and continue on the path towards the Light Woods");
                                    area = "Light Woods";
                                    ClearScreen();
                                }
                            }
                        }
                        else
                        {
                            Typeout("You decide to walk back and continue on the path towards the Light Woods");
                            area = "Light Woods";
                            ClearScreen();
                        }
                    }
                }
            }

            //Light Woods area
            while (area == "Light Woods")
            {
                Typeout("You're walking through the woods as the trees start to become less dense and the path bare of leaves. In the distance you can see a samll village. " +
                    "\nAs you get closer to the village something trips you. You look around and see nothing. A little further and something trips you again.");
                /////
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Ask what it wants", "Walk past it", "Attack", "Then right outsid the village gate a small creature appears mid air infront of you.It is a brownie.");
                    if (input == '1')
                    {
                        Typeout("You ask the brownie why he has been bothering you and what it want.");
                        Typeout("It bows and apologizes profusely saying that it could not help itself. That it always trips people as they enter the village.");
                        input = GetInput("Yes", "No", "Attack", "It ask if you will forgive it and give it permission to enter the village.");
                        if (input == '1')
                        {
                            Typeout("You forgive the brownie and enter the village with the brownie floating behind you.");
                            ClearScreen();
                            area = "Village";
                        }
                        else if (input == '2')
                        {
                            Typeout("You push the brownie aside and enter the village.");
                            ClearScreen();
                            area = "Village";
                        }
                        if (input == '3')
                        {
                            switch (_player.GetRole())
                            {
                                case "Knight":
                                    Typeout("You punch the brownie into the door and enter the village.");
                                    ClearScreen();
                                    area = "Village";
                                    break;
                                case "Rogue":
                                    Typeout("You knock the top of the brownie's head with the pommel of one of your daggers and enter the village as the brownie falls to the ground.");
                                    ClearScreen();
                                    area = "Village";
                                    break;
                                case "Wizard":
                                    Typeout("You freeze the brownie and let it fall to the ground as you enter the village.");
                                    ClearScreen();
                                    area = "Village";
                                    break;
                            }
                        }
                    }
                    else if (input == '2')
                    {
                        Typeout("You push the brownie aside and enter the village.");
                        ClearScreen();
                        area = "Village";
                    }
                    if (input == '3')
                    {
                        switch (_player.GetRole())
                        {
                            case "Knight":
                                Typeout("You punch the brownie into the door and enter the village.");
                                ClearScreen();
                                area = "Village";
                                break;
                            case "Rogue":
                                Typeout("You knock the top of the brownie's head with the pommel of one of your daggers and enter the village as the brownie falls to the ground.");
                                ClearScreen();
                                area = "Village";
                                break;
                            case "Wizard":
                                Typeout("You freeze the brownie and let it fall to the ground as you enter the village.");
                                ClearScreen();
                                area = "Village";
                                break;
                        }
                    }
                }
            }

            //Village area
            while (area == "Village")
            {
                Typeout("You enter the village and see people bustling around. Even a drogon doesn't stop people from living out their lives. Although the knights look tired. " +
                    "Necks hurt and eyes dried from watching the sky for dragons.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3' && input != '4')
                {
                TownCenter:
                    input = GetInput("Inn", "Shop", "Well", "Leave Village", "Where do you go?");
                    if (input == '1')
                    {
                        Typeout("You walk over to the inn. The Dragon's Peak, a sign says above the door as you enter the inn.");
                        input = GetInput("Yes", "No", "Do you enter?");
                        if (input == '1')
                        {
                            Typeout("You enter the inn. It's full of noise and poeple. You walk over to the bar. The bartender walks up to you and eyes you up and down..");
                        TheBar:
                            input = GetInput("Food", "Sleep", "Information", "He asks what you want.");
                            if (input == '1')
                            {
                                Typeout("You ask the bartender for food and he asks if have gold to pay. " +
                                    "You tell him that you're going to fight the dragon and he waves you off, telling you your food will be there soon. " +
                                    "You sit a lonly table when a server drop off your food. You feel refreashed and ready to continue.");
                                switch (_player.GetRole())
                                {
                                    case "Knight":
                                        _player.Heal(100.0f);
                                        break;
                                    case "Rogue":
                                        _player.Heal(90.0f);
                                        break;
                                    case "Wizard":
                                        _player.Heal(80.0f);
                                        break;
                                }
                                ClearScreen();
                                goto TheBar;
                            }
                            else if (input == '2')
                            {
                                Typeout("You ask for a room and the bartender asks if you have any gold to pay. " +
                                    "You tell him that you're going to fight the dragon and he waves you off, telling you your room number. " +
                                    "You go to the room and sleep till the next day. Then you head out.");
                                switch (_player.GetRole())
                                {
                                    case "Knight":
                                        _player.Heal(100.0f);
                                        break;
                                    case "Rogue":
                                        _player.Heal(90.0f);
                                        break;
                                    case "Wizard":
                                        _player.Heal(80.0f);
                                        break;
                                }
                                ClearScreen();
                                goto TownCenter;
                            }
                            else if (input == '3')
                            {
                                Typeout("You ask the bartender if he has any information about the dragon. He scoffs and says that the dragon lives in a field beyond the cliff on the other side of the village. " +
                                    "He says that a group of goblins has moved in at the bottom of the cliff. Then he tells you not to throw your life away and go home.");
                                ClearScreen();
                                goto TheBar;
                            }
                        }
                        else if (input == '2')
                        {
                            Typeout("You decide not to enter the inn.");
                            ClearScreen();
                            goto TownCenter;
                        }
                    }
                    else if (input == '2')
                    {
                        Typeout("You walk up to the shop.");
                        OpenShopMenu();
                        ClearScreen();
                        goto TownCenter;
                    }
                    else if (input == '3')
                    {
                        Typeout("You walk up to the well, but it seems perfectly fine at the moment.");
                        ClearScreen();
                        goto TownCenter;
                    }
                    if (input == '4')
                    {
                        input = GetInput("Uphill", "Downhill", "Do you leave going uphill or downhill?");
                        if (input == '1')
                        {
                            Typeout("You decide to go Uphill out the village.");
                            ClearScreen();
                            area = "Uphill";
                        }
                        else if (input == '2')
                        {
                            Typeout("You decide to go Downhill out the village.");
                            ClearScreen();
                            area = "Downhill";
                        }
                    }
                }
            }

            //Uphill area
            while (area == "Uphill")
            {
                Typeout("You begain walking up hill. After hours of walking upward, you finally racher the top of the cliff. " +
                    "In the distance you can see a large burning field with a mighty dragon sitting in the center. " +
                    "You continue walking when you hear a snarl and turn to see a large black wolf with a little red hood in its mouth.");
                input = ' ';
                while (input != '1' && input != '2')
                {
                    input = GetInput("Run", "Attack", "What do you do?");
                    if (input == '1')
                    {
                        Typeout("You try to run from the wolf, but it runs infront of you and goes to attack!");
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_wolf);

                    }
                    else if (input == '2')
                    {
                        Typeout("You ready yourself and prepare to finish this wolf!");
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_wolf);
                    }
                }
                Typeout("You continue walking and soon stand on the edge of a buring field. " +
                    "There is no going back. You walk toward the Dragon!");
                ClearScreen();
                area = "Boss";
            }

            //DownHill Area
            while (area == "Downhill")
            {
                Typeout("You begain walking down hill and come to the bottom of the cliff. " +
                    "You see a group of goblins coming in and out of a cave at the base of the cliff.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Approach", "Sneak Past", "Attack", "What do you do?");
                    if (input == '1')
                    {
                        Typeout("You approach the goblins and they all freeze and stare. Then drop whatever they were doing and charge at you!");
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_goblins);
                    }
                    else if (input == '2')
                    {
                        switch (_player.GetRole())
                        {
                            case "Knight":
                                Typeout("You try to sneak past, but your armor makes too much noise and the goblins see you. " +
                                    "The goblins all freeze and stare. Then drop whatever they were doing and charge at you!");
                                Typeout("Prepare for a fight!");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                                StartBattle(_goblins);
                                break;
                            case "Rogue":
                                Typeout("You manage to sneak past them and continue on your way.");
                                Typeout("You continue walking and soon stand on the edge of a buring field. " +
                                    "There is no going back. You walk toward the Dragon!");
                                ClearScreen();
                                area = "Boss";
                                break;
                            case "Wizard":
                                Typeout("You try to sneak past, but your staff pokes out and gives you away. " +
                                    "The goblins all freeze and stare. Then drop whatever they were doing and charge at you!");
                                Typeout("Prepare for a fight!");
                                Console.Write("> ");
                                Console.ReadKey();
                                Console.Clear();
                                StartBattle(_goblins);
                                break;
                        }
                    }
                    if (input == '3')
                    {
                        Typeout("You charge at the goblin weapon ready!");
                        Typeout("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        StartBattle(_goblins);
                    }
                }
                Typeout("You continue walking and soon stand on the edge of a buring field. " +
                    "There is no going back. You walk toward the Dragon!");
                ClearScreen();
                area = "Boss";
            }

            //Boss Area
            while (area == "Boss")
            {
                Typeout("You walk across the burning field and soon come upon the mighty dragon! It stands tall before you, ready to get this over with.");
                Typeout("Prepare for a fight and good luck!");
                Console.Write("> ");
                Console.ReadKey();
                Console.Clear();
                StartBattle(_dragon);
                Typeout("You were the mighty world the hero needed!");
                ClearScreen();
                _gameOver = true;
                area = " ";
            }

        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Performed once when the game ends
        public void End()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Typeout("G A M E  O V E R");
        }
    }
}
