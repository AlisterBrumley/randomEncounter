/*
    TODO
    sort list by speed, including player in list
    run turn loop until winConditon or player HP <1
        action select
            actions on bottom line of screen
            messages one layer above
        run attack turns
*/

// MAIN VAR SET
const int MaxEnemies = 4;
const int MaxDice = 20;
const int MinDice = 1;

// NEW PLAYER
Actor player = CreatePlayer();
// setting custom playername


// NEW ENEMIES AND SPEED SORTING
Actor[] enemy_list = CreateEnemies();
enemy_list = SpeedSort(enemy_list);

foreach (Actor enemy in enemy_list)
{
    Console.WriteLine(enemy.Name);
}


int Roll()
{
    var rand = new Random();
    return rand.Next(MinDice, MaxDice);
}

Actor CreatePlayer()
{
    Actor player;
    if (args.Length > 0)
    {
        player = new Player(args[0]);
    }
    else
    {
        player = new Player();
    }
    return player;
}

Actor[] CreateEnemies()
{
    Actor[] e_list = new Actor[MaxEnemies];

    for (int i = 0; i < MaxEnemies; i++)
    {
        int roll = Roll();
        // Console.WriteLine(roll);
        switch (roll)
        {
            case 20:
                e_list[i] = new CyberDemon();
                break;
            case > 15:
                e_list[i] = new Ogre("Shrek", 200);
                break;
            case > 10:
                e_list[i] = new Ogre();
                break;
            case > 0:
                e_list[i] = new Slime();
                break;
            default:
                e_list[i] = new Slime();
                break;
        }
    }

    return e_list;
}

Actor[] SpeedSort(Actor[] unsortedList)
{
    return unsortedList.OrderBy(enemy => enemy.Speed).Reverse().ToArray();
}



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
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("HP: " + Health);
        Console.WriteLine("Roll: " + roll);
        int damage = baseDmg + (Def - roll);
        Console.WriteLine("BD: " + baseDmg);
        Console.WriteLine("Damage: " + damage);
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
    public Ogre(string customName, int customHp) : base(name: customName, hp: customHp, def: 30, melee: 30, ranged: 0, speed: 30) { }
}

public class Slime : Actor
{
    public Slime() : base(name: "Slime", hp: 30, def: 10, melee: 10, ranged: 10, speed: 10) { }
}

public class CyberDemon : Actor
{
    public CyberDemon() : base(name: "CyberDemon", hp: 500, def: 200, melee: 120, ranged: 200, speed: 20) { }
}

