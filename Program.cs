
Actor[] enemies = CreateEnemies();
Actor player = new Player();
// Actor shrek = new Ogre("shrek");
// Actor ogre = new Ogre();

// int dmgRoll = Roll();
// shrek.Damaged(player.MeleeAtk, dmgRoll);
// dmgRoll = Roll();
// ogre.Damaged(shrek.MeleeAtk, dmgRoll);

foreach (Actor enemy in enemies)
{
    Console.WriteLine(enemy.Name);
}

/*
    TODO
    sort list by speed, including player in list
    run turn loop until winConditon or player HP <1
        action select
            actions on bottom line of screen
            messages one layer above
        run attack turns
*/

int Roll()
{
    var rand = new Random();
    return rand.Next(1, 20);
}

Actor[] CreateEnemies()
{
    Actor[] e_list = new Actor[4];

    for (int i = 0; i < 4; i++)
    {
        int roll = Roll();
        // Console.WriteLine(roll);
        switch (roll)
        {
            case 20:
                e_list[i] = new CyberDemon();
                break;
            case > 15:
                e_list[i] = new Ogre("Shrek");
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
}

public class Ogre : Actor
{
    public Ogre() : base(name: "Ogre", hp: 100, def: 30, melee: 30, ranged: 0, speed: 20) { }
    public Ogre(string customName) : base(name: customName, hp: 100, def: 30, melee: 30, ranged: 0, speed: 20) { }
}

public class Slime : Actor
{
    public Slime() : base(name: "Slime", hp: 30, def: 10, melee: 10, ranged: 10, speed: 10) { }
}

public class CyberDemon : Actor
{
    public CyberDemon() : base(name: "CyberDemon", hp: 500, def: 200, melee: 120, ranged: 200, speed: 20) { }
}

// base(name: , hp: , def: , melee: , ranged: , speed: )


// USE BASE CLASSES
// public class ActorFactory()
// {
//     public Actor newSlime()
//     {
//         return new Actor("Slime", 30, 10, 10, 10, 10);
//     }

//     public Actor newPlayer()
//     {
//         return new Actor("Player", 30, 10, 10, 10, 10);
//     }

//     public Actor newOgre()
//     {
//         return new Actor("Ogre", 100, 30, 30, 0, 20);
//         // public string name = "Ogre";
//         // public int hp = 100;
//         // public int melee = 30;
//         // public int range = 20;
//     }

//     public Actor CyberDemon()
//     {
//         return new Actor("CyberDemon", 5500, 120, 30, 0, 20);
//         // string name = "CyberDemon";
//         // int hp = 5500;
//         // int melee = 120;
//         // int range = 300;
//     }

//     // public Actor Player()
//     // {
//     //     public string name = "Player";
//     //     public int hp = 150;
//     //     public int mp = 120;
//     //     public int melee = 10;

//     // }
// }
