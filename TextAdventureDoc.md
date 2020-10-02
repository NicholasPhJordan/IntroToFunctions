| Nicholas Jordan|
| :---          	|
| s208047     	|
| Introduction to C# |
| Text Adventure Documentation |

## I. Requirements

1. Description of Problem

	- **Name**: Text Adventure

	- **Problem Statement**: 
Creation of an application or game in C# that demonstrates foundational data structures and file handling.

	- **Problem Specifications**:  
     Creation of an application or game that meets one of the provided application briefs, or
application requirements as negotiated with your teacher. To gain competency in this subject, your assessment submission must demonstrate the following
components, implemented in the C# programming language:
      - A modular approach to implementing logic
      - Class inheritance
      - Two arrays of user defined data types
      - At least two classes containing four or more instance variables
      - At least one instance of an overloaded constructor
      - A class implementing user-defined object aggregation
      - Polymorphism
      - Reading from and writing to a text file

## II. Design

### Object Information

**File**: Game.cs

**Attributes**

        Name: Item 
            Description: Initializes variables
            Type: struct
        
        Name: name 
            Description: Holds the data for the name of the item
            Type: string 

        Name: cost
            Description: Holds the data for what the cost of the item will be
            Type: int

        Name: _gameOver
            Description: Holds data on wither the statement game over is true or false
            Type: bool

        Name: _shop
            Description: Allows information to be pulled from the Shop class
            Type: Shop Class

        Name: _player
            Description: Allows information to be accessed from the Character class 
            Type: Character Class

        Name: _snake
            Description: Allows initialization using Monster Class code
            Type: Monster Class


        Name: _crazedThief
            Description: Allows initialization using Monster Class code
            Type: Monster Class

        Name: _goblins 
            Description: Allows initialization using Monster Class code
            Type: Monster Class

        Name: _wolf
            Description: Allows initialization using Monster Class code
            Type: Monster Class

        Name: _dragon
            Description: Allows initialization using Monster Class code
            Type: Monster Class

        Name: area
            Description: Allows specific code to execute when put into a while loop and set to true 
            Type: string

        Name: _inventory
            Description: Sets up an array called _inventory
            Type: Item Array 

        Name: _staff
            Description: Allows initialization of an item
            Type: Item

        Name: _daggers
            Description: Allows initialization of an item
            Type: Item

        Name: _sword
            Description: Allows initialization of an item
            Type: Item

        Name: _apple
            Description: Allows initialization of an item
            Type: Item

        Name: _strangeCoin
            Description: Allows initialization of an item
            Type: Item

        Name: _shopInventory
            Description: Aets up an array called _shopInventory
            Type: Item Array

        Name: _arrow
            Description: allows initialization of an item
            Type: Item

        Name: _shield
            Description: Allows initialization of an item
            Type: Item

        Name: _bow
            Description: Allows initialization of an item
            Type: Item

        Name: Initialize()
            Description: Allows a space to initialize varibles and call upon those 
            Type: void

        Name: CreateCharacter()
            Description: Function  that takes player input to create new or load a character
            Type: Charcter

        Name: Typeout()
            Description: Function that takes a string and prints out one character at a time,
                             with a slight pause between each character then prints a new line when finished
            Type: void

        Name: StartBattle()
            Description: Takes in monster stats and plays out a fighting sequence between the two
            Type: void

        Name: PrintInventory()
            Description: Takes and inventory array and then prints out each item name
            Type: void

        Name: AddItemToInventory()
            Description: takes in item and index location to put that new item
            Type: void

        Name: GetInput()
            Description: takes in 2 strings and and a queery the prints it out and waits for player input to react  
            Type: Char

        Name: GetInput()
            Description: overload takes in 3 strings and and a queery the prints it out and waits for player input to react  
            Type: Char

        Name: GetInput()
            Description: overload takes in 4 strings and and a queery the prints it out and waits for player input to react  
            Type: Char

        Name: ClearScreen()
            Description: Gets player input of press any key then clears the console of all text
            Type: void

        Name: Save()
            Description: takes current player data and saves it into a text file for later reuse 
            Type: void

        Name: Load()
            Description: loads data from a text file that went under the Save function 
            Type: void

        Name: Run()
            Description: actually activates and run all the cude using start/ update/ and end functions 
            Type: void

        Name: Start()
            Description: preformend when games begins, is used for intializing varibles 
            Type: void

        Name: Update()
            Description: Repeats until the game ends 
            Type: void

        Name: End()
            Description: preformed when the game ends
            Type: void

**File**: Character.cs

**Attributes**

        Name: _name
            Description: declairs variable _name 
            Type: string

        Name: _role
            Description: Declares varible _role 
            Type: string

        Name: _health
            Description: Declares varible _health 
            Type: float

        Name: _damage
            Description: Declares varibles _damage 
            Type: float

        Name: _gold
            Description: Declares varible _gold 
            Type: int


        Name: Character()
            Description: initialzies the varables declared above
            Type: Character

        Name: Character()
            Description: overload that allows creation of character 
            Type: Character

        Name: Save()
            Description: Descides what stats to save from character  
            Type: void

        Name: Load()
            Description: creates variables to store loaded data, checks to see if load was successful and if so updat variables 
            Type: void

        Name: ViewStats()
            Description: prints out character stats to the console
            Type: void

        Name: GetName()
            Description: returns the name of the current player
            Type: string

        Name: GetRole()
            Description: returns the role of the current player 
            Type: string

        Name: GetHealth()
            Description: returns the health var of the current player
            Type: float

        Name: GetGold()
            Description: returns the amount of gold currently equal to the current players gold
            Type: int

        Name: Attack()
            Description: takes in a monster and descides the damage out to the monster 
            Type: float

        Name: TakeDamage()
            Description: takes in a damage value and uses that to subtract from the decided health 
            Type: float

        Name: Heal()
            Description: takes ina value and makes the players health equal to that value 
            Type: float

        Name: Typeout()
            Description: Function that takes a string and prints out one character at a time,
                             with a slight pause between each character then prints a new line when finished
            Type: void

        Name: GetInput()
            Description: takes in 2 strings and and a queery the prints it out and waits for player input to react  
            Type: Char

        Name: GetInput()
            Description: overload takes in 3 strings and and a queery the prints it out and waits for player input to react  
            Type: Char

        Name: ClearScreen()
            Description: Gets player input of press any key then clears the console of all text
            Type: void

**File**: Shop.cs

**Attributes**