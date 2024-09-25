using static Globals;
public class Writer
{
    static (int x, int y) MessagePos = (TermMin, termHeight - 3);  // position above bottom box for game messages
    // static (int x, int y) PromptPos = (TermMin, termHeight - 1):
    public static void MoveCursorWrite(int xPos, int yPos, string message) // seperate ints - I should try get rid of these
    {
        Console.SetCursorPosition(xPos, yPos);
        Console.Write(message);
    }

    public static void MoveCursorWrite((int x, int y) pos, string message) // tupled ints
    {
        Console.SetCursorPosition(pos.x, pos.y);
        Console.Write(message);
    }

    public static void MessageWrite(string message)
    {
        Color.Message();
        Console.SetCursorPosition(MessagePos.x, MessagePos.y);
        Console.Write(message.PadRight(termWidth));
        Color.Default();
    }


    public static void PromptWrite(string message)
    {
        int messageStartPos = termHalfWidth - (message.Length / 2);
        (int x, int y) PromptPos = (messageStartPos, termHeight - 1);

        Color.Prompt();
        Console.SetCursorPosition(PromptPos.x, PromptPos.y);
        Console.WriteLine(message);
        Color.Default();

    }

    public static void PromptWrite(string[] messageArray)
    {
        string message = string.Join("", messageArray);
        int messageStartPos = termHalfWidth - (message.Length / 2);
        (int x, int y) PromptPos = (messageStartPos, termHeight - 1);

        Color.Prompt();
        Console.SetCursorPosition(PromptPos.x, PromptPos.y);
        Console.WriteLine(message);
        Color.Default();

    }

    public static void PromptHighlight(ref int idx, bool add)
    {
        int promptArrLength = turnPrompt.Length - 1;

        //resets
        Color.Prompt();
        Writer.MoveCursorWrite(promptPos[idx], turnPrompt[idx]);
        Color.Default();

        // moving index
        idx = add ? ++idx : --idx; // postfix++ / --didnt work
        // checks if idx positon overrides and resets it back
        idx = (idx > promptArrLength) ? 0 : (idx < 0 ? promptArrLength : idx);

        Color.Highlight();
        Writer.MoveCursorWrite(promptPos[idx], turnPrompt[idx]);
        Color.Default();
    }

    public static void PromptHighlight(int idx)
    {
        Color.Highlight();
        Writer.MoveCursorWrite(promptPos[idx], turnPrompt[idx]);
        Color.Default();
    }

    // DEBUGS - TODO - MOVE TO OWN FILE

    static (int x, int y) DebugPos = (TermMin, TermMin); // top left positon for debug messages

    public static void DebugWrite(string message)
    {
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
    }

    public static void DebugWrite(string message, int time)
    {
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
        Thread.Sleep(time);
        DebugClear();
    }

    public static void DebugWrite(int messageToConv)
    {
        string message = Convert.ToString(messageToConv);
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
    }


    public static void DebugWrite(int messageToConv, int time)
    {
        string message = Convert.ToString(messageToConv);
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
        Thread.Sleep(time);
        DebugClear();
    }

    public static void DebugWrite(bool messageToConv)
    {
        string message = Convert.ToString(messageToConv);
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
    }


    public static void DebugWrite(bool messageToConv, int time)
    {
        string message = Convert.ToString(messageToConv);
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(message);
        Thread.Sleep(time);
        DebugClear();
    }

    public static void DebugClear()
    {
        Console.SetCursorPosition(DebugPos.x, DebugPos.y);
        Console.Write(clearLine);
    }
}
