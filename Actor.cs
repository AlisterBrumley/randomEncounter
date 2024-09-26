
public class Actor
{
    public string Name;
    public int Health;
    public int Def; // defense multiplier base stat
    public int MeleeAtk; // melee attack base damage
    public int RangedAtk; // range attack base damage
    public int Speed; // evasion base stat

    public string? actionState;
    public Actor? target;
    public string Graphic = "       \n       "; // empty space by default

    public Actor(string name, int hp, int def, int melee, int ranged, int speed)
    {

        Name = name;
        Health = hp;
        Def = def;
        MeleeAtk = melee;
        RangedAtk = ranged;
        Speed = speed;
    }
    public Actor(string name, int hp, int def, int melee, int ranged, int speed, string graphic)
    {

        Name = name;
        Health = hp;
        Def = def;
        MeleeAtk = melee;
        RangedAtk = ranged;
        Speed = speed;
        Graphic = graphic;
    }

    // Might be better to make this an attack rather then damage
    // public void Damaged(int baseDmg, int roll)
    // {
    //     int damage = baseDmg + (Def - roll);
    //     int crit = 2 * (Health / 3);

    //     if (damage >= crit)
    //     {
    //         Console.WriteLine("critical hit!");
    //         Console.WriteLine($"hit {Name} with {damage} damage!");
    //     }
    //     else
    //     {
    //         Console.WriteLine($"hit {Name} with {damage} damage!");
    //     }
    //     Console.WriteLine();
    // }

    public bool Melee(Actor target)
    {
        // rolling for damage
        int dmgRoll = Game.Roll();
        int damage = (int)(MeleeAtk * (dmgRoll * 5 / 100m));
        // Writer.DebugWrite("ATK:" + damage, 2000);

        // rolling for defense
        int defRoll = Game.Roll();
        int defense = (int)(target.Def * (defRoll * 5 / 100m));
        // Writer.DebugWrite("DEF:" + defense, 2000);

        // Writer.DebugWrite(target.Name + " HP:" + target.Health + " DMG:" + damage + " DEF:" + defense);

        // rolling for evasion
        //      // MISSES TOO MUCH?
        int dodgeRoll = Game.Roll();
        int dodgeSpeed = (int)(target.Speed * (dodgeRoll * 5 / 100m));
        int hitRoll = Game.Roll();
        int hitSpeed = (int)(this.Speed * (hitRoll * 5 / 100m));
        if (dodgeSpeed > hitSpeed)
        {
            Writer.MessageWrite(this.Name + " missed!");
            return false;
        }

        // taking care of overrun
        damage = defense >= damage ? 1 : damage;
        target.Health -= damage;
        Writer.DebugWrite(target.Health);

        // TODO - FIGURE THIS OUT?
        // int damage = MeleeAtk + (target.Def - roll);
        // int crit = 2 * (Health / 3);

        // int crit = target.Def % roll;
        // if (crit > 10)
        // {
        //     target.Health = target.Health - damage;
        //     // Console.WriteLine("critical hit!");
        //     // Console.WriteLine($"hit {Name} with {damage} damage!");
        // }
        // else
        // {
        //     // Console.WriteLine($"hit {Name} with {damage} damage!");
        // }

        return true;
    }

    public void Ranged(Actor target, int roll)
    {

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
    public Ogre() : base(name: "Ogre", hp: 100, def: 30, melee: 30, ranged: 0, speed: 20, graphic: ",|,,,|,\n(o _ o)") { }
    // public Ogre(string customName) : base(name: customName, hp: 100, def: 30, melee: 30, ranged: 0, speed: 20) { }
    public Ogre(string customName, int customHp) : base(name: customName, hp: customHp, def: 30, melee: 30, ranged: 0, speed: 35, graphic: "O>   <O\n(T _ T)") { }
}

public class Slime : Actor
{
    public Slime() : base(name: "Slime", hp: 30, def: 10, melee: 10, ranged: 10, speed: 10, graphic: "       \n(o - O)") { }
}

public class CyberDemon : Actor
{
    public CyberDemon() : base(name: "CyberDemon", hp: 500, def: 200, melee: 120, ranged: 200, speed: 20, graphic: " }{ }{ \n(Ø>_<Ø)") { }
}

public class Bug : Actor
{
    public Bug() : base(name: "Bug", hp: 15, def: 5, melee: 10, ranged: 10, speed: 10, graphic: "°\\, ,/°\n(# m #)") { }
}

public class Bunny : Actor
{
    public Bunny() : base(name: "Bunny", hp: 5, def: 0, melee: 1, ranged: 0, speed: 1, graphic: " /\\ /\\ \n(o x o)") { }
}