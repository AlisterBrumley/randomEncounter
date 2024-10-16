using static Globals;
public class Game
{
    // Basically a D20
    public static int Roll()
    {
        var rand = new Random();
        return rand.Next(MinDice, MaxDice);
    }

    public static void Init()
    {
        string bottomBox = clearLine + "\n" + clearLine + "\n" + clearLine;
        string gameStart = "A Random Encounter!";
        int gameStartHalfLen = gameStart.Length / 2;
        int introX = termHalfWidth - gameStartHalfLen;
        (int x, int y) promptBoxPos = (TermMin, termHeight - 2);

        // Drawing bottom Box
        Console.Clear();
        Color.Prompt();
        Writer.MoveCursorWrite(promptBoxPos, bottomBox);

        // A random encounter!
        Writer.PromptWrite(gameStart);
        Thread.Sleep(1000);

        Color.Default();
    }

    public static void PromptReset()
    {
        Writer.PromptWrite(turnPrompt);
    }

    public static void ExitCheck()
    {
        Writer.DebugWrite("Are you sure you want to quit? [y/N]: ");
        string? conf = Console.ReadLine();
        conf = conf?.ToLower().Trim();
        if (conf == "y" || conf == "yes" || conf == "yeah" || conf == "yep") // we could go on forever here...
        {
            SafeExit();
        }
        else Writer.DebugWrite(clearLine);
    }

    public static void RunCheck()
    {
        int roll = Roll();
        if (roll < 10)
        {
            Writer.DebugWrite("you ran away like a coward!");
            Thread.Sleep(1500);
            SafeExit();
        }
        else
        {
            Writer.DebugWrite("you tripped like a goof and couldnt escape!");
            Thread.Sleep(1000);
            Writer.DebugWrite(clearLine);
        }
    }

    public static void SafeExit()
    {
        Console.Clear();
        Console.CursorVisible = true;
        Environment.Exit(0);
    }

    // if we need it, this error and quit
    // void ErrorExit()
    // {
    //     Console.Clear();
    //     Console.CursorVisible = true;
    //     Environment.Exit(1);
    // }


    // public static Actor PlayerTargetChoice(Actor[] enemies)
    // {
    //     int enemiesArrLength = enemies.Length - 1;
    //     bool selected = false;
    //     int enemyIdx = 0;
    //     do
    //     {
    //         Writer.MessageWrite("Select Target: " + enemies[enemyIdx].Name);
    //         switch (Console.ReadKey(true).Key)
    //         {
    //             case ConsoleKey.LeftArrow:
    //                 enemyIdx = --enemyIdx < 0 ? enemiesArrLength : enemyIdx;
    //                 break;
    //             case ConsoleKey.RightArrow:
    //                 enemyIdx = ++enemyIdx > enemiesArrLength ? 0 : enemyIdx;
    //                 break;
    //             case ConsoleKey.Escape:
    //                 ExitCheck();
    //                 break;
    //             case ConsoleKey.Enter:
    //                 selected = true;
    //                 break;
    //         }
    //     } while (!selected);

    //     return enemies[enemyIdx];
    // }

    public static Actor CreatePlayer(string[] args)
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

    public static Actor[] CreateEnemies(Actor player)
    {
        Actor[] enemies = new Actor[MaxEnemies];

        for (int i = 0; i < MaxEnemies; i++)
        {
            int roll = Roll();
            switch (roll)
            {
                case 20:
                    enemies[i] = new CyberDemon(player);
                    break;
                case > 17:
                    enemies[i] = new Ogre(customName: "Shrek", customHp: 200, player);
                    break;
                case > 12:
                    enemies[i] = new Ogre(player);
                    break;
                case > 7:
                    enemies[i] = new Slime(player);
                    break;
                case > 2:
                    enemies[i] = new Bug(player);
                    break;
                case > 0:
                    enemies[i] = new Bunny(player);
                    break;
                default:
                    enemies[i] = new Slime(player);
                    break;
            }
        }

        return enemies;
    }

    public static Actor[] TurnSort(Actor[] enemies, Actor player)
    {
        Actor[] turnOrder = new Actor[MaxActors];
        enemies.CopyTo(turnOrder, 0);
        turnOrder[MaxActors - 1] = player; // player last to preference over same speed enemies
        return turnOrder.OrderBy(enemy => enemy.Speed).Reverse().ToArray();
    }
}