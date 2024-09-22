/*
    RANDOM ENCOUNTER GENERATOR
    by Alister Brumley 2024
*/
using static Globals;

/*
        MAIN VARIABLE SETTING
*/
// GAME SETINGS
bool winConditon = false;
// TERM SETTINGS
Console.CursorVisible = false;
Color.GetDefaults();

// PROMPT/INFO SETTINGS



/*
    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN    MAIN
*/
// CREATING ACTORS AND SORTING SPEED
Actor player = Game.CreatePlayer(args);
Actor[] enemies = Game.CreateEnemies();
Actor[] turnOrder = Game.TurnSort(enemies, player);

Game.Init();

// TURN LOOP
do
{
    Game.PromptSet();
    string selectedAction = Game.PlayerChoice();
    if (selectedAction == playerActions.escape)
    {
        Game.RunCheck();
    }
    else
    {
        Writer.MessageWrite(selectedAction);
    }
} while (!winConditon);


/*
        METHOD/FUNCTION DEFS
*/


/*
    OLD VARS/METHODS
*/
//          UN NEEDED
// int termHalfHeight = Console.WindowHeight / 2;

//          WORKS BETTER THE OTHER WAY ROUND
// string[] turnPrompt = { "Melee".PadRight(promptSpacing), "Ranged".PadRight(promptSpacing), "Run".PadRight(promptSpacing) };
// (string melee, string range, string escape) playerActions = (turnPrompt[0], turnPrompt[1], turnPrompt[2]);

//          MOVED INTO GLOBALS
// const int MaxEnemies = 4;
// const int MaxActors = MaxEnemies + 1;
// const int MaxDice = 20;
// const int MinDice = 1;
// const int TermMin = 0;
// int termHeight = Console.WindowHeight - 1;
// int termWidth = Console.WindowWidth;

// //          MOVED INTO WRITER

//  COMBINED
// void PromptHighlight(ref int idx)
// {

//     // checks if idx positon overrides and resets it back
//     idx = (idx > promptArrLength) ? 0 : (idx < 0 ? promptArrLength : idx);

//     Color.Highlight();
//     Writer.MoveCursorWrite(promptPos[idx], turnPrompt[idx]);
//     Color.Default();
// }

// void PromptUnHighlight(int idx)
// {
//     Color.Prompt();
//     Writer.MoveCursorWrite(promptPos[idx], turnPrompt[idx]);
//     Color.Default();
// }


/*
    MOVED INTO Game.cs
*/

// (int x, int y) prompBoxPos = (TermMin, termHeight - 2); 
// string clearLine = "".PadRight(termWidth);

// int promptHalfLength = string.Join("", turnPrompt).Length / 2;

// Basically a D20
// int Roll()
// {
//     var rand = new Random();
//     return rand.Next(MinDice, MaxDice);
// }

// string Game.PlayerChoice()
// {
//     bool selected = false;
//     int promptIdx = 0;
//     PromptHighlight(ref promptIdx);
//     do
//     {
//         switch (Console.ReadKey(true).Key)
//         {
//             case ConsoleKey.LeftArrow:
//                 PromptUnHighlight(promptIdx);
//                 promptIdx--;
//                 PromptHighlight(ref promptIdx);
//                 break;
//             case ConsoleKey.RightArrow:
//                 PromptUnHighlight(promptIdx);
//                 promptIdx++;
//                 PromptHighlight(ref promptIdx);
//                 break;
//             case ConsoleKey.Escape:
//                 Game.ExitCheck();
//                 break;
//             case ConsoleKey.Enter:
//                 selected = true;
//                 break;
//         }
//     } while (!selected);
//     return turnPrompt[promptIdx].Trim();
// }

// void InitGameScreen()
// {
//     string bottomBox = clearLine + "\n" + clearLine + "\n" + clearLine;
//     string gameStart = "A Random Encounter!";
//     int gameStartHalfLen = gameStart.Length / 2;
//     int introX = termHalfWidth - gameStartHalfLen;
//     (int x, int y) promptBoxPos = (TermMin, termHeight - 2);

//     // Drawing bottom Box
//     Console.Clear();
//     ColorSet.Prompt();
//     Writer.MoveCursorWrite(promptBoxPos, bottomBox);

//     // A random encounter!
//     Writer.PromptWrite(gameStart);
//     Thread.Sleep(1000);

//     ColorSet.Default();
// }

// void Game.PromptSet()
// {
//     Console.SetCursorPosition(promptPos[0].x, promptPos[0].y);
//     ColorSet.Prompt();
//     foreach (string prompts in turnPrompt)
//     {
//         Console.Write(prompts);
//     }
//     ColorSet.Default();
// }

// void Game.PromptExit()
// {
//     Writer.DebugWrite("Are you sure you want to quit? [y/N]: ");
//     string? conf = Console.ReadLine();
//     conf = conf?.ToLower().Trim();
//     if (conf == "y" || conf == "yes" || conf == "yeah" || conf == "yep") // we could go on forever here...
//     {
//         SafeExit();
//     }
//     else Writer.DebugWrite(clearLine);
// }

// void Game.RunCheck()
// {
//     int roll = Roll();
//     if (roll < 10)
//     {
//         Writer.DebugWrite("you ran away like a coward!");
//         Thread.Sleep(1500);
//         SafeExit();
//     }
//     else
//     {
//         Writer.DebugWrite("you tripped like a goof and couldnt escape!");
//         Thread.Sleep(1000);
//         Writer.DebugWrite(clearLine);
//     }
// }

// void SafeExit()
// {
//     Console.Clear();
//     Console.CursorVisible = true;
//     Environment.Exit(0);
// }

// // if we need it, this is a fast way to error and quit
// // void ErrorExit()
// // {
// //     Console.Clear();
// //     Console.CursorVisible = true;
// //     Environment.Exit(1);
// // }

// Actor Game.CreatePlayer()
// {
//     if (args.Length > 0)
//     {
//         return new Player(customName: args[0]);
//     }
//     else
//     {
//         return new Player();
//     }
// }

// Actor[] CreateEnemies()
// {
//     Actor[] enemies = new Actor[MaxEnemies];

//     for (int i = 0; i < MaxEnemies; i++)
//     {
//         int roll = Roll();
//         switch (roll)
//         {
//             case 20:
//                 enemies[i] = new CyberDemon();
//                 break;
//             case > 15:
//                 enemies[i] = new Ogre(customName: "Shrek", customHp: 200);
//                 break;
//             case > 10:
//                 enemies[i] = new Ogre();
//                 break;
//             case > 0:
//                 enemies[i] = new Slime();
//                 break;
//             default:
//                 enemies[i] = new Slime();
//                 break;
//         }
//     }

//     return enemies;
// }

// Actor[] TurnSort(Actor[] enemies, Actor player)
// {
//     Actor[] turnOrder = new Actor[MaxActors];
//     enemies.CopyTo(turnOrder, 0);
//     turnOrder[MaxActors - 1] = player; // player last to preference over same speed enemies
//     return turnOrder.OrderBy(enemy => enemy.Speed).Reverse().ToArray();
// }

