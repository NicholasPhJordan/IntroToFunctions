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
        private Player _player;
        string area = " ";

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

        //Battle sequince that also declares enemy stats
        void startBattle(string _enemyName = "none", float _enemyHealth = 0.0f, float _enemyDamage = 0.0f)
        {
            char input = ' ';
            while (_player.health > 0 && _enemyHealth > 0)
            {
                Console.Clear();
                Console.WriteLine("Health: " + _player.health + "               " + _enemyName + " Health: " + _enemyHealth);
                Console.WriteLine("----------------------------------------------");
                input = GetInput("Attack", "Defend", "What will you do?");
                if (input == '1')
                {
                    //attack with full damage and accept full damage
                    _enemyHealth -= _player.damage;
                    typewrite("The " + _enemyName + " took " + _player.damage + " damage!");
                    //enemy attack
                    _player.health -= _enemyDamage;
                    typewrite("You took " + _enemyDamage + " damage!");
                    Console.Write("> ");
                    Console.ReadKey();

                }
                else if (input == '2')
                {
                    //deal no damage but take less damage from enemy
                    typewrite("You blocked and took less damage.");
                    _player.health -= _enemyDamage * 0.25f;
                    Console.Write("> ");
                    Console.ReadKey();
                }
            }
            //conslusion of fight 
            if (_player.health <= 0.0f)
            {
                //if player looses
                typewrite("You Died");
                Console.WriteLine("Press any key to continue");
                ClearScreen();
                _gameOver = true;
            }
            else if (_enemyHealth <= 0.0f)
            {
                //if player wins
                typewrite("You survived the battle!");
                ClearScreen();
            }
        }

        //Function that gets player name and checks if they want to keep it
        public Player CreateCharacter()
        {
            Player player = new Player();
            char input = ' ';
            while (input != '1')
            {

                player.ChooseName();
                input = GetInput("Yes", "No", "Is this the name you wish to continue with?");
            }
            input = ' ';
            while (input != '1')
            {
                player.ChooseRole();
                input = GetInput("Yes", "No", "Is this the role you wish to continue with?");
            }
            return player;
        }

        //Get player feed back when two options available
        Char GetInput(string option1, string option2, string query)
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
                    _player.ViewStats();
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
                typewrite(query);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.WriteLine("3. " + option3);
                Console.WriteLine("4. View Stats");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                if (input == '4')
                {
                    _player.ViewStats();
                }
                Console.WriteLine();
            }
            return input;
        }

        public void ClearScreen()
        {
            Console.WriteLine("Press any key to continue");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
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
            //Charater set up
            _player = CreateCharacter();
            ClearScreen();

            //story intro
            typewrite("After many years of living in the safety of your home village, " +
                "you head off on a mighty quest through unknown lands where monsters, thieves, and many other dangers live. " +
                "There is also untold amounts of glory and riches for those who survive and return from this perilous quest.");
            typewrite("Good Luck!");
            ClearScreen();

            //first interaction
            typewrite("You start your adventure walking down an old road outside your village and soon run into a little girl, wearing a little red hood.");
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                input = GetInput("Ask why she is out here", "Take an apple", "Attack", "She holds up a basket full of bright red apples toward you.");
                if (input == '1')
                {
                    input = GetInput("Take an apple", "Walk away", "Attack", "She doesn't say anything but holds the basket up a little higher toward you.");
                    if (input == '1')
                    {
                        typewrite("You take an apple and bite into it. The little girl smiles with large fang like teeth.");
                        typewrite("Add apple to inventory");
                        _player.inventory.Insert(0, "Apple ,");
                        input = GetInput("Run Away", "Attack", "She throws off the little red hood and turns into a large wolf!");
                        if (input == '1')
                        {
                            typewrite("You manage to get away from the wolf, but not before it swipes at your back.");
                            typewrite("Loose 10 health");
                            _player.health -= 10.0f;
                            ClearScreen();
                        }
                        else if (input == '2')
                        {
                            switch (_player.role)
                            {
                                case "Knight":
                                    typewrite("You swipe skillfully with your sword at the wolf, scaring it and causing it to run away.");
                                    ClearScreen();
                                    break;
                                case "Rogue":
                                    typewrite("You quickly swipe at the wolf with your daggers, scaring it and causing it to run away.");
                                    ClearScreen();
                                    break;
                                case "Wizard":
                                    typewrite("You cast a fireball at the wolf, scaring it and causing it to run away.");
                                    ClearScreen();
                                    break;
                            }
                        }
                    }
                    else if (input == '2')
                    {
                        typewrite("You walk away from the little girl and continue your adventure.");
                        ClearScreen();
                    }
                    if (input == '3')
                    {
                        switch (_player.role)
                        {
                            case "Knight":
                                typewrite("You swipe skillfully with your sword at the girl, scaring her and causing her to run away.");
                                ClearScreen();
                                break;
                            case "Rogue":
                                typewrite("You quickly swipe at the girl with your daggers, scaring her and causing her to run away.");
                                ClearScreen();
                                break;
                            case "Wizard":
                                typewrite("You cast a fireball at the girl, scaring her and causing her to run away.");
                                ClearScreen();
                                break;
                        }
                    }
                }
                else if (input == '2')
                {
                    typewrite("You take an apple and bite into it. The little girl smiles with large fang like teeth.");
                    typewrite("Add apple to inventory");
                    _player.inventory.Insert(0, "Apple ,");
                    input = GetInput("Run Away", "Attack", "She throws off the little red hood and turns into a large wolf!");
                    if (input == '1')
                    {
                        typewrite("You manage to get away from the wolf, but not before it swipes at your back.");
                        typewrite("Loose 10 health");
                        _player.health -= 10.0f;
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        switch (_player.role)
                        {
                            case "Knight":
                                typewrite("You swipe skillfully with your sword at the wolf, scaring it and causing it to run away.");
                                ClearScreen();
                                break;
                            case "Rogue":
                                typewrite("You quickly swipe at the wolf with your daggers, scaring it and causing it to run away.");
                                ClearScreen();
                                break;
                            case "Wizard":
                                typewrite("You cast a fireball at the wolf, scaring it and causing it to run away.");
                                ClearScreen();
                                break;
                        }
                    }
                }
                if (input == '3')
                {
                    switch (_player.role)
                    {
                        case "Knight":
                            typewrite("You swipe skillfully with your sword at the girl, scaring her and causing her to run away.");
                            ClearScreen();
                            break;
                        case "Rogue":
                            typewrite("You quickly swipe at the girl with your daggers, scaring her and causing her to run away.");
                            ClearScreen();
                            break;
                        case "Wizard":
                            typewrite("You cast a fireball at the girl, scaring her and causing her to run away.");
                            ClearScreen();
                            break;
                    }
                }
            }

            //Path diverges
            typewrite("You continue walking down the old road and it soon turns into a dirt path.");
            input = ' ';
            while (input != '1' && input != '2')
            {
                input = GetInput("Left toward a Tall Grassy Meadow", "Right toward a Dark Woods", "Continuing your walk, you come to a fork in the road.");
                if (input == '1')
                {
                    typewrite("You decide to go left toward the Tall Grassy Meadow.");
                    area = "Tall Grassy Meadow";
                    ClearScreen();
                }
                else if (input == '2')
                {
                    typewrite("You decide to go right toward the Dark Woods,");
                    area = "Dark Woods";
                    ClearScreen();
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
                    input = GetInput("Talk to snake", "Run", "Attack", "A large snake slithers out the grass onto the path infront of you.");
                    if (input == '1')
                    {
                        if (_player.role == "Wizard")
                        {
                            typewrite("You talk to the snake and to your surprise it replies.");
                            input = GetInput("Offer apple", "Attack", "The sanke tells you that it doesn't wish to fight and just wants some food.");
                            if (input == '1')
                            {
                                if (_player.inventory.Contains("Apple ,"))
                                {
                                    typewrite("You take an apple, with a single bite in it, out of your bag and offer it to the snake.");
                                    input = GetInput("Run", "Attack", "The snake shakes its head and tells you that it doesn't want to eat the apple.");
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
                                    input = GetInput("Run", "Attack", "The snake is slooking at you with hunger in its eyes.");
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
                            input = GetInput("Run", "Attack", "The snake coils on itself locking gaze with you.");
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

                //path diverges
                typewrite("You carry on after your battle, walking through the tall grass. When you come to a bend in the path. " +
                    "You see a camp fire off the path to the left and a creek further down the trail to the right.");
                input = ' ';
                while (input != '1' && input != '2')
                {
                    input = GetInput("Left towrd Camp", "Right toward Creek", "Do you go toward the Camp or the Creek?");
                    if (input == '1')
                    {
                        typewrite("You decide to go toward the Camp.");
                        area = "Camp";
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        typewrite("You decide to go toward the Creek.");
                        area = "Creek";
                        ClearScreen();
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
                    input = GetInput("Help him up", "Walk away", "Attack", "When you get closer, the old man asks if you will help him up.");
                    if (input == '1')
                    {
                        typewrite("You help up the old amn and realize he is blind.");
                        typewrite("He looks at you with faded eyes and thanks you. He then waves his hand over your weapon " +
                            "before vanishing into a pillar of somke.");
                        typewrite("+5 bounus damage");
                        _player.damage += 5.0f;
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        typewrite("You ignore the old man and continue walking down the path.");
                        ClearScreen();
                    }
                    if (input == '3')
                    {
                        switch (_player.role)
                        {
                            case "Knight":
                                typewrite("You charge at the old man with your sword. But before you can get to him, he stands and waves his hand. " +
                                "You feel a burning sensation coming from your sword as it has turned red hot.");
                                typewrite("Loose 20 Health");
                                _player.health -= 20.0f;
                                ClearScreen();
                                break;
                            case "Rogue":
                                typewrite("You dash at the old man with your daggers. But before you can get to him, he stands and waves his hand. " +
                                "You feel a burning sensation coming from your daggers as they have turned red hot.");
                                typewrite("Loose 20 Health");
                                _player.health -= 20.0f;
                                ClearScreen();
                                break;
                            case "Wizard":
                                typewrite("You cast a fireball at the old man but nothing happens. He quickly stands and waves his hand. " +
                                "You feel a burning sensation coming from your staff as it has turned red hot.");
                                typewrite("Loose 20 Health");
                                _player.health -= 20.0f;
                                ClearScreen();
                                break;
                        }
                    }
                }

                //second interaction
                typewrite("You continue on the dark trail and soon enter an opening in the middle of the Dark Woods. " +
                    "You stop and start to hear noises. Something is moving all around you.");
                input = ' ';
                while (input != '1' && input != '2' && input != '3')
                {
                    input = GetInput("Call out", "Run", "Attack", "The noise suddenly stops infront of you behind some bushes.");
                    if (input == '1')
                    {
                        typewrite("You call out into the open.");
                        input = GetInput("Demand they come closer", "Run", "Attack", "A low voice replies saying hello, followed by some psychotic laughter.");
                        if (input == '1')
                        {
                            typewrite("You demand that whoever it is comes out of the bushes and into the opening. " +
                                "They follow your wishes and a shadowy figure comes out of the bushes. Before you stands a tall thin man with large daggers on his hips.");
                            if (_player.role == "Rogue")
                            {
                                input = GetInput("Use thieves cant", "Run", "Attack", "You notice a fellow thief when you see one.");
                                if (input == '1')
                                {
                                    typewrite("You try to use thieves cant to show that you are a friend, but he does not care. He says he has no frinds, " +
                                        "followd by some psychotic laughter.");
                                    input = GetInput("Run", "Attack", "He puts his hands on his daggers.");
                                    if (input == '1')
                                    {
                                        typewrite("You try to run for it realizing this guy is CRAZY, but he jumps infront of you cutting you across the chest!");
                                        typewrite("Loose 10 Health");
                                        _player.health -= 10.0f;
                                        typewrite("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        startBattle("Crazed Thief", 40.0f, 10.0f);
                                    }
                                    else if (input == '2')
                                    {
                                        typewrite("You don't wait for him to attack first and dash at him with your daggers!");
                                        typewrite("Prepare for a fight!");
                                        Console.Write("> ");
                                        Console.ReadKey();
                                        Console.Clear();
                                        startBattle("Crazed Thief", 40.0f, 10.0f);
                                    }
                                }
                                else if (input == '2')
                                {
                                    typewrite("You try to run for it, but he jumps infront of you cutting you across the chest!");
                                    typewrite("Loose 10 Health");
                                    _player.health -= 10.0f;
                                    typewrite("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    startBattle("Crazed Thief", 40.0f, 10.0f);
                                }
                                if (input == '3')
                                {
                                    typewrite("You don't wait for him to get a chance to attack first and dash at him with your daggers!");
                                    typewrite("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    startBattle("Crazed Thief", 40.0f, 10.0f);
                                }
                            }
                            else
                            {
                                input = GetInput("Reason with", "Run", "Attack", "He looks at you with a crazed look in his eyes.");
                                if (input == '1')
                                {
                                    typewrite("You starts to say something in a gentel voice, but the man dashes at you with his daggers cutting you across the chest!");
                                    typewrite("Loose 10 Health");
                                    _player.health -= 10.0f;
                                    typewrite("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    startBattle("Crazed Thief", 40.0f, 10.0f);
                                }
                                else if (input == '2')
                                {
                                    typewrite("You try to run for it, but he jumps infront of you cutting you across the chest!");
                                    typewrite("Loose 10 Health");
                                    _player.health -= 10.0f;
                                    typewrite("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    startBattle("Crazed Thief", 40.0f, 10.0f);
                                }
                                else if (input == '3')
                                {
                                    typewrite("You don't wait for him to go crazy and attack!");
                                    typewrite("Prepare for a fight!");
                                    Console.Write("> ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    startBattle("Crazed Thief", 40.0f, 10.0f);
                                }
                            }
                        }
                        else if (input == '2')
                        {
                            typewrite("You take a run for it when a tall, thin man jumps out infront of you with daggers in each hand.");
                            typewrite("He cuts you across the chest!");
                            typewrite("Loose 10 Health");
                            _player.health -= 10.0f;
                            typewrite("Prepare for a fight!");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                            startBattle("Crazed Thief", 40.0f, 10.0f);
                        }
                        if (input == '3')
                        {
                            typewrite("You attack blindly at the bush. A scream escapes from the bush and a tall, thin man jumps out with daggers ready!");
                            typewrite("Prepare for a fight!");
                            Console.Write("> ");
                            Console.ReadKey();
                            Console.Clear();
                            startBattle("Crazed Thief", 35.0f, 10.0f);
                        }
                    }
                    else if (input == '2')
                    {
                        typewrite("You make a run for it from these spooky noises when a tall, thin  man, with daggers in each hand, jumps out from behind the bushes.");
                        typewrite("He cuts you across the chest!");
                        typewrite("Loose 10 Health");
                        _player.health -= 10.0f;
                        typewrite("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        startBattle("Crazed Thief", 40.0f, 10.0f);
                    }
                    if (input == '3')
                    {
                        typewrite("You attack blindly at the bush. A scream escapes from the bush and a tall, thin man jumps out with daggers ready!");
                        typewrite("Prepare for a fight!");
                        Console.Write("> ");
                        Console.ReadKey();
                        Console.Clear();
                        startBattle("Crazed Thief", 35.0f, 10.0f);
                    }
                }

                //Path diverges
                typewrite("After the fight, you continue on the path. Some times goes by and you come to a bend in the path.");
                typewrite("The path goes toward a lighter and less dense part of the woods, " +
                    "but you also notice a covered path that leads even deeper into the Dark Woods.");
                input = ' ';
                while (input != '1' && input != '2')
                {
                    input = GetInput("Left toward the Dark Woods", "Right toward the Light Woods",
                        "Do you go toward the Light Woods or head deeper into the Dark Woods?");
                    if (input == '1')
                    {
                        typewrite("You decide to go further into the Dark Woods");
                        ClearScreen();
                    }
                    else if (input == '2')
                    {
                        typewrite("You decide to head towrd the Light Woods");
                        area = "Light Woods";
                        ClearScreen();
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
