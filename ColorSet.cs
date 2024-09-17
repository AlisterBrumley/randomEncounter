public class ColorSet
{
    // taking original term colors
    (ConsoleColor BG, ConsoleColor FG) defaultColor = (Console.BackgroundColor, Console.ForegroundColor);

    public void Prompt()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void Highlight()
    {
        Console.BackgroundColor = ConsoleColor.DarkCyan;
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void Message()
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
    }
    
    public void Default()
    {
        Console.BackgroundColor = defaultColor.BG;
        Console.ForegroundColor = defaultColor.FG;
    }
}