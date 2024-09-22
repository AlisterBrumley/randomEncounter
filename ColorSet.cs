public class Color
{
    // taking original term colors
    static (ConsoleColor BG, ConsoleColor FG) defaultColor;

    public static void GetDefaults()
    {
        defaultColor = (Console.BackgroundColor, Console.ForegroundColor);
    }

    public static void Prompt()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Highlight()
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Message()
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void Default()
    {
        Console.BackgroundColor = defaultColor.BG;
        Console.ForegroundColor = defaultColor.FG;
    }
}