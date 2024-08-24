/*
    TODO
    sort list by speed, including player in list
    run turn loop until winConditon or player HP <1
        action select
            actions on bottom line of screen
            messages one layer above
        run attack turns
*/

/*
        MAIN VARIABLE SETTING
*/
// GAME SETINGS
const int MaxEnemies = 4;
const int MaxActors = MaxEnemies + 1;
const int MaxDice = 20;
const int MinDice = 1;
const int termMin = 0;
bool winConditon = false;

// TEXT/INFO SETTINGS
string gameStart = "A Random Encounter!";
int gameStartHalfLen = gameStart.Length / 2;
string[] actions = { "[ ]Melee".PadRight(10), "[ ]Ranged".PadRight(10), "[ ]Escape".PadRight(10) };
string turnPrompt = string.Join("", actions);
int promptHalfLength = turnPrompt.Length / 2;

// TERM SETTINGS
Console.CursorVisible = false;
ConsoleColor orgBG = Console.BackgroundColor;
ConsoleColor orgFG = Console.ForegroundColor;
int termHeight = Console.WindowHeight - 1;
int termWidth = Console.WindowWidth;
int termHalfHeight = Console.WindowHeight / 2;
int termHalfWidth = Console.WindowWidth / 2;
string fullWidth = "".PadRight(termWidth);


/*
    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN
*/
// CREATING ACTORS AND SORTING SPEED
Actor player = CreatePlayer();
Actor[] enemies = CreateEnemies();
Actor[] turnOrder = TurnSort(enemies, player);

InitGameScreen();

// TURN LOOP
do
{
    
} while (!winConditon);


/*
        METHOD/FUNCTION DEFS
*/
// Basically a D20
int Roll()
{
    var rand = new Random();
    return rand.Next(MinDice, MaxDice);
}

void InitGameScreen()
{
    Console.Clear();
    Console.BackgroundColor = ConsoleColor.DarkBlue;
    Console.ForegroundColor = ConsoleColor.White;
    // Drawing bottom box
    Console.SetCursorPosition(termMin, termHeight - 2);
    Console.Write(fullWidth + "\n" + fullWidth + "\n" + fullWidth);

    Console.SetCursorPosition(termHalfWidth - gameStartHalfLen, termHeight - 1);
    Console.Write(gameStart);
    Thread.Sleep(1000);
    Console.SetCursorPosition(termHalfWidth - promptHalfLength, termHeight - 1);
    Console.Write(turnPrompt);
    Console.BackgroundColor = orgBG;
    Console.ForegroundColor = orgFG;
}

Actor CreatePlayer()
{
    if (args.Length > 0)
    {
        return new Player(customName: args[0]);
    }
    else
    {
        return new Player();
    }
}

Actor[] CreateEnemies()
{
    Actor[] enemies = new Actor[MaxEnemies];

    for (int i = 0; i < MaxEnemies; i++)
    {
        int roll = Roll();
        switch (roll)
        {
            case 20:
                enemies[i] = new CyberDemon();
                break;
            case > 15:
                enemies[i] = new Ogre(customName: "Shrek", customHp: 200);
                break;
            case > 10:
                enemies[i] = new Ogre();
                break;
            case > 0:
                enemies[i] = new Slime();
                break;
            default:
                enemies[i] = new Slime();
                break;
        }
    }

    return enemies;
}

Actor[] TurnSort(Actor[] enemies, Actor player)
{
    Actor[] turnOrder = new Actor[MaxActors];
    enemies.CopyTo(turnOrder, 0);
    turnOrder[MaxActors - 1] = player; // player last to preference over same speed enemies
    return turnOrder.OrderBy(enemy => enemy.Speed).Reverse().ToArray();
}


/*
        CLASS DEFS
*/
public class Actor
{
    public string Name;
    public int Health;
    public int Def; // defense multiplier base stat
    public int MeleeAtk; // melee attack base damage
    public int RangedAtk; // range attack base damage
    public int Speed; // evasion base stat

    public Actor(string name, int hp, int def, int melee, int ranged, int speed)
    {

        Name = name;
        Health = hp;
        Def = def;
        MeleeAtk = melee;
        RangedAtk = ranged;
        Speed = speed;
    }

    public void Damaged(int baseDmg, int roll)
    {
        // Console.WriteLine("Name: " + Name);
        // Console.WriteLine("HP: " + Health);
        // Console.WriteLine("Roll: " + roll);
        int damage = baseDmg + (Def - roll);
        // Console.WriteLine("BD: " + baseDmg);
        // Console.WriteLine("Damage: " + damage);
        int crit = 2 * (Health / 3);

        if (damage >= crit)
        {
            Console.WriteLine("critical hit!");
            Console.WriteLine($"hit {Name} with {damage} damage!");
        }
        else
        {
            Console.WriteLine($"hit {Name} with {damage} damage!");
        }
        Console.WriteLine();
    }
}

public class Player : Actor
{
    public Player() : base(name: "Player", hp: 150, def: 120, melee: 50, ranged: 50, speed: 30) { }
    public Player(string customName) : base(name: customName, hp: 150, def: 120, melee: 50, ranged: 50, speed: 30) { }

    // add debug/cheat mode
    // public Player(string customName) : base(name: customName, hp: 999, def: 120, melee: 50, ranged: 50, speed: 30) { }
}

public class Ogre : Actor
{
    public Ogre() : base(name: "Ogre", hp: 100, def: 30, melee: 30, ranged: 0, speed: 20) { }
    // public Ogre(string customName) : base(name: customName, hp: 100, def: 30, melee: 30, ranged: 0, speed: 20) { }
    public Ogre(string customName, int customHp) : base(name: customName, hp: customHp, def: 30, melee: 30, ranged: 0, speed: 35) { }
}

public class Slime : Actor
{
    public Slime() : base(name: "Slime", hp: 30, def: 10, melee: 10, ranged: 10, speed: 10) { }
}

public class CyberDemon : Actor
{
    public CyberDemon() : base(name: "CyberDemon", hp: 500, def: 200, melee: 120, ranged: 200, speed: 20) { }
}

