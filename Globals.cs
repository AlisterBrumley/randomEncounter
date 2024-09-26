public static class Globals
{
    public const int MaxEnemies = 4;
    public const int MaxActors = MaxEnemies + 1;
    public const int MaxDice = 20;
    public const int MinDice = 1;
    public const int TermMin = 0;
    public static int termHeight = Console.WindowHeight - 1;
    public static int termWidth = Console.WindowWidth;
    public static int termHalfWidth = Console.WindowWidth / 2;
    public static int promptSpacing = 10;

    public static string clearLine = "".PadRight(termWidth);

    public static (string melee, string range, string escape) playerActions = ("Melee", "Ranged", "Run");
    public static string[] turnPrompt = { playerActions.melee.PadRight(promptSpacing), playerActions.range.PadRight(promptSpacing), playerActions.escape.PadRight(promptSpacing) };
    public static int promptStartX = termHalfWidth - (string.Join("", turnPrompt).Length / 2);
    public static (int x, int y)[] promptPos = { (promptStartX, termHeight - 1), (promptStartX + promptSpacing, termHeight - 1), (promptStartX + promptSpacing * 2, termHeight - 1) };
}