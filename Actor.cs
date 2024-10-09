using static Globals;

public class Actor
{
    public string Name;
    public int Health;
    public int Def; // adjustDefense multiplier base stat
    public int MeleeAtk; // melee attack base damage
    public int RangedAtk; // range attack base damage
    public int Speed; // evasion base stat

    public string? actionState;
    public Actor? Target; // might need to make (idx, name)
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

    public bool Melee(Actor target)
    {
        // rolling for evasion
        int dodgeRoll = Game.Roll();
        int hitRoll = Game.Roll();
        if (hitRoll < 5)
        {
            Writer.DebugWrite("HIT " + hitRoll);
            Writer.MessageWrite(this.Name + " missed!");
            return false;
        }
        else if (dodgeRoll > 18)
        {
            Writer.DebugWrite("DOD " + dodgeRoll);
            Writer.MessageWrite(target.Name + " dodged the attack!");
            return true;
        }
        else if (target.GetType() == typeof(Player) && dodgeRoll > 10)
        {
            Writer.DebugWrite("DOD " + dodgeRoll);
            Writer.MessageWrite(target.Name + " dodged the attack!!");
            return true;
        }

        // random damage adjustment
        //      // TOFIX - tends to get low damage output on eneies
        int dmgRoll = Game.Roll();
        int adjustDamage = (int)(MeleeAtk / 4 * (dmgRoll * 5 / 100m));
        // random defense adjustment
        int defRoll = Game.Roll();
        int adjustDefense = (int)(target.Def * (defRoll * 5 / 100m) / 2);


        int damage = MeleeAtk + adjustDamage - adjustDefense;
        // taking care of overrun 
        damage = damage <= 0 ? 1 : damage;
        target.Health -= damage;

        Writer.DebugWrite(target.Name + " HP:" + target.Health + " DMG:" + damage + " DEF:" + adjustDefense + " ADJ:" + adjustDamage);

        return true;
    }

    public bool Ranged(Actor target)
    {
        // rolling for evasion
        //      // TOFIX - enemies miss too much
        int dodgeRoll = Game.Roll();
        int dodgeSpeed = (int)(target.Speed * (dodgeRoll * 5 / 100m));
        int hitRoll = Game.Roll();
        int hitSpeed = (int)(this.Speed * (hitRoll * 5 / 100m));
        if (dodgeSpeed > hitSpeed)
        {
            Writer.MessageWrite(this.Name + " missed!");
            return false;
        }

        return true;
    }

    // TODO
    // public class Actions;
    // {
    // }
    // ORD
    // enum Actions
    // {
    // }
}

public class Player : Actor
{
    public Player() : base(name: "Player", hp: 150, def: 80, melee: 50, ranged: 50, speed: 35) { }
    public Player(string customName) : base(name: customName, hp: 150, def: 120, melee: 50, ranged: 50, speed: 30) { }

    // add debug/cheat mode
    // public Player(string customName) : base(name: customName, hp: 999, def: 120, melee: 50, ranged: 50, speed: 30) { }

    public static string ActionChoice()
    {
        bool selected = false;
        int promptIdx = 0;
        Writer.PromptHighlight(promptIdx);
        do
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.LeftArrow:
                    Writer.PromptLeft(ref promptIdx);
                    break;
                case ConsoleKey.RightArrow:
                    Writer.PromptRight(ref promptIdx);
                    break;
                case ConsoleKey.Escape:
                    Game.ExitCheck();
                    break;
                case ConsoleKey.Enter:
                    selected = true;
                    break;
            }
        } while (!selected);
        Writer.PromptUnHighlight(promptIdx);
        return turnPrompt[promptIdx].Trim();
    }

    public static Actor TargetChoice(Actor[] enemies)
    {
        int enemiesArrLength = enemies.Length - 1;
        bool selected = false;
        int enemyIdx = 0;
        do
        {
            Writer.MessageWrite("Select Target: " + enemies[enemyIdx].Name);
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.LeftArrow:
                    enemyIdx = --enemyIdx < 0 ? enemiesArrLength : enemyIdx;
                    break;
                case ConsoleKey.RightArrow:
                    enemyIdx = ++enemyIdx > enemiesArrLength ? 0 : enemyIdx;
                    break;
                case ConsoleKey.Escape:
                    Game.ExitCheck();
                    break;
                case ConsoleKey.Enter:
                    selected = true;
                    break;
            }
        } while (!selected);

        return enemies[enemyIdx];
    }
}

// TODO
// public class Enemy : Actor
// {
//     public static Actor ActionChoice()
//     {
//         this.actionState == playerActions.melee;
//     }
// }


public class Ogre : Actor
{
    public Ogre() : base(name: "Ogre", hp: 100, def: 30, melee: 25, ranged: 0, speed: 20, graphic: ",|,,,|,\n(o _ o)") { }
    public Ogre(string customName, int customHp) : base(name: customName, hp: customHp, def: 30, melee: 30, ranged: 0, speed: 35, graphic: "O>   <O\n(T _ T)") { }
}

public class Slime : Actor
{
    public Slime() : base(name: "Slime", hp: 30, def: 10, melee: 15, ranged: 10, speed: 25, graphic: "       \n(o - O)") { }
}

public class CyberDemon : Actor
{
    public CyberDemon() : base(name: "CyberDemon", hp: 500, def: 200, melee: 120, ranged: 200, speed: 10, graphic: " }{ }{ \n(Ø>_<Ø)") { }
}

public class Bug : Actor
{
    public Bug() : base(name: "Bug", hp: 15, def: 5, melee: 10, ranged: 10, speed: 35, graphic: "°\\, ,/°\n(# m #)") { }
}

public class Bunny : Actor
{
    public Bunny() : base(name: "Bunny", hp: 5, def: 0, melee: 1, ranged: 0, speed: 100, graphic: " /\\ /\\ \n(o x o)") { }
}