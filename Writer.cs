public class Writer
{
    int TermMin;
    int TermHeight;
    int TermWidth;

    (int x, int y) DebugPos; // top left positon for debug messages
    (int x, int y) MessagePos; // position above bottom box for game messages

    public Writer(int termMin, int termHeight, int termWidth)
    {
        TermMin = termMin;
        TermHeight = termHeight;
        TermWidth = termWidth;

        DebugPos = (TermMin, TermMin);
        MessagePos = (TermMin + 1, TermHeight - 3);
    }
    public void MoveCursorWrite(int xPos, int yPos, string message) // seperate ints
    {
        Console.SetCursorPosition(xPos, yPos);
        Console.Write(message);
    }

    public void MoveCursorWrite((int x, int y) pos, string message) // tupled ints
    {
        Console.SetCursorPosition(pos.x, pos.y);
        Console.Write(message);
    }

    public void PromptWrite()
    {
        // TODO
    }
    
    public void MessageWrite(string message)
    {
        Console.SetCursorPosition(MessagePos.x, MessagePos.y);
        Console.Write(message);
    }

    public void DebugWrite(string message)
    {
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
    }
}
