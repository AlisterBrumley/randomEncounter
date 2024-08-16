
// Actor[] enemies = CreateEnemies();
ActorFactory af = new ActorFactory();
Actor player = af.newPlayer();
Actor shrek = af.newOgre();

int dmgRoll = Roll();
shrek.Damaged(player.MeleeAtk, dmgRoll);
dmgRoll = Roll();
player.Damaged(shrek.MeleeAtk, dmgRoll);


int Roll()
{
    var rand = new Random();
    return rand.Next(1, 20);
}

// Actor[] CreateEnemies()
// {
//     Actor[] e_list = new Actor[4];

//     for (int i = 0; i < 4; i++)
//     {
//         int roll = Roll();
//         switch (roll)
//         {
//             case > 10:
//                 // create enemy
//                 break;
//             case > 0:
//                 // put enemy detail in class
//                 break;
//             default:
//                 // put enemy detail in class
//                 break;
//         }
//     }


//     return e_list;
// }



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



public class ActorFactory()
{
    public Actor newSlime()
    {
        return new Actor("Slime", 30, 10, 10, 10, 10);
    }

    public Actor newPlayer()
    {
        return new Actor("Player", 30, 10, 10, 10, 10);
    }

    public Actor newOgre()
    {
        return new Actor("Ogre", 100, 30, 30, 0, 20);
        // public string name = "Ogre";
        // public int hp = 100;
        // public int melee = 30;
        // public int range = 20;
    }

    // public Actor CyberDemon()
    // {
    //     string name = "CyberDemon";
    //     int hp = 5500;
    //     int melee = 120;
    //     int range = 300;


    // }

    // public Actor Player()
    // {
    //     public string name = "Player";
    //     public int hp = 150;
    //     public int mp = 120;
    //     public int melee = 10;

    // }
}
