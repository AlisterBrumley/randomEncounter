/*
    TODO
    sort list by speed, including player in list
    run turn loop until winConditon or player HP <1
        action select
            turnPrompt on bottom line of screen
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
const int TermMin = 0;
bool winConditon = false;

// TERM SETTINGS
Console.CursorVisible = false;
int termHeight = Console.WindowHeight - 1;
int termWidth = Console.WindowWidth;
int termHalfHeight = Console.WindowHeight / 2;
int termHalfWidth = Console.WindowWidth / 2;
(int x, int y) prompColorSetPos = (TermMin, termHeight - 2);
(int x, int y) messageColorSetPos = (TermMin, termHeight - 3); // ColorSet above bottom ColorSet for game messages
string clearLine = "".PadRight(termWidth);
ColorSet ColorSet = new ColorSet();

// PROMPT/INFO SETTINGS
string gameStart = "A Random Encounter!";
int gameStartHalfLen = gameStart.Length / 2;
int messageY = termHeight - 1;
int promptSpacing = 10;
string[] turnPrompt = { "Melee".PadRight(promptSpacing), "Ranged".PadRight(promptSpacing), "Run".PadRight(promptSpacing) };
(string melee, string range, string escape) playerActions = (turnPrompt[0], turnPrompt[1], turnPrompt[2]);
int promptArrLength = turnPrompt.Length - 1;
int promptHalfLength = string.Join("", turnPrompt).Length / 2;
int promptStartX = termHalfWidth - promptHalfLength;
(int x, int y)[] promptPos = { (promptStartX, messageY), (promptStartX + promptSpacing, messageY), (promptStartX + promptSpacing * 2, messageY) };


Writer writer = new Writer(TermMin, termHeight, termWidth);

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
    PromptSet();
    string selectedAction = PlayerChoice();
    if (selectedAction == playerActions.escape)
    {
        RunCheck();
    }
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

string PlayerChoice()
{
    bool selected = false;
    int promptIdx = 0;
    PromptHighlight(ref promptIdx);
    do
    {
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.LeftArrow:
                PromptUnHighlight(promptIdx);
                promptIdx--;
                PromptHighlight(ref promptIdx);
                break;
            case ConsoleKey.RightArrow:
                PromptUnHighlight(promptIdx);
                promptIdx++;
                PromptHighlight(ref promptIdx);
                break;
            case ConsoleKey.Escape:
                PromptExit();
                break;
            case ConsoleKey.Enter:
                selected = true;
                break;
        }
    } while (!selected);
    return turnPrompt[promptIdx];
}

void InitGameScreen()
{
    string bottomColorSet = clearLine + "\n" + clearLine + "\n" + clearLine;
    int introX = termHalfWidth - gameStartHalfLen;
    Console.Clear();
    // Drawing bottom ColorSet
    ColorSet.Prompt();
    // Box.Draw(prompColorSetPos);
    writer.MoveCursorWrite(prompColorSetPos, bottomColorSet); // MAKE NEW FUNCTION TO WRITE PROMPTS HERE

    // A random encounter!
    writer.MoveCursorWrite(introX, messageY, gameStart);
    Thread.Sleep(1000);

    ColorSet.Default();
}

void PromptSet()
{
    Console.SetCursorPosition(promptPos[0].x, promptPos[0].y);
    ColorSet.Prompt();
    foreach (string prompts in turnPrompt)
    {
        Console.Write(prompts);
    }
    ColorSet.Default();
}

void PromptHighlight(ref int idx)
{
    // if (idx > promptArrLength)
    // {
    //     idx = 0;
    // }
    // else if (idx < 0)
    // {
    //     idx = promptArrLength;
    // }
    // ternary operator below does the above if, but in one line
    // checks if idx positon overrides and resets it back
    idx = (idx > promptArrLength) ? 0 : (idx < 0 ? promptArrLength : idx);

    ColorSet.Highlight();
    writer.MoveCursorWrite(promptPos[idx].x, messageY, turnPrompt[idx]);
    ColorSet.Default();
}

void PromptUnHighlight(int idx)
{
    ColorSet.Prompt();
    writer.MoveCursorWrite(promptPos[idx].x, messageY, turnPrompt[idx]);
    ColorSet.Default();
}

void PromptExit()
{
    writer.DebugWrite("Are you sure you want to quit? [y/N]: ");
    string? conf = Console.ReadLine();
    conf = conf?.ToLower().Trim();
    if (conf == "y" || conf == "yes" || conf == "yeah" || conf == "yep") // we could go on forever here...
    {
        SafeExit();
    }
    else writer.DebugWrite(clearLine);
}

void RunCheck()
{
    int roll = Roll();
    if (roll < 10)
    {
        writer.DebugWrite("you ran away like a coward!");
        Thread.Sleep(1500);
        SafeExit();
    }
    else
    {
        writer.DebugWrite("you tripped like a goof and couldnt escape!");
        Thread.Sleep(1000);
        writer.DebugWrite(clearLine);
    }
}

void SafeExit()
{
    Console.Clear();
    Console.CursorVisible = true;
    Environment.Exit(0);
}

// if we need it, this is a fast way to error and quit
// void ErrorExit()
// {
//     Console.Clear();
//     Console.CursorVisible = true;
//     Environment.Exit(1);
// }

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



public class Box
{
    // public void Draw((int x, int y)pos)
    // {
    //     // string clearLine = "".PadRight(Console.WindowWidth);
    //     string bottomColorSet = clearLine + "\n" + clearLine + "\n" + clearLine;
    //     Console.SetCursorPosition(pos.x, pos.y);
    //     Console.Write(bottomColorSet);
    // }
}