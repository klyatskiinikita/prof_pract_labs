using System.Text;

Menu menu = new();
bool continueFlag = true;

while (continueFlag)
{
    menu.MakeRequest();
    continueFlag = menu.HandleRequest();
    menu.GetAnswer();
}

public class Menu
{

    public enum Entry : byte { Exit, PrintFishText, Calculate, WrongInput };

    private Entry _userChoice;
    private string _mainPrompt = "[0] - Exit\n[1] - Get Fish Text\n[2] - Get Values Multiplied";
    private string _output = "";

    private string? GetUserInput(string prompt)
    {
        System.Console.WriteLine(prompt);
        return System.Console.ReadLine();
    }

    public void MakeRequest()
    {
        if (!Menu.Entry.TryParse(GetUserInput(_mainPrompt), out _userChoice) || (int)_userChoice > Entry.GetNames(typeof(Entry)).Length)
            _userChoice = Menu.Entry.WrongInput;
    }

    public bool HandleRequest()
    {
        switch (_userChoice)
        {
            case Entry.PrintFishText:
                ushort wordsAmount;
                _output = ushort.TryParse(GetUserInput("Enter amount of words to read from fish text."), out wordsAmount) ?
                                    GetFishText(wordsAmount) :
                                    "Wrong input.";
                break;
            case Entry.Calculate:
                float x, y;
                _output = float.TryParse(GetUserInput("Enter first operand."), out x) &&
                          float.TryParse(GetUserInput("Enter second operand."), out y) ?
                                    GetCalculationResult(x, y).ToString() :
                                    "Wrong input.";
                break;
            case Entry.WrongInput:
                _output = "Wrong input.";
                break;
            case Entry.Exit:
                _output = "Bye!";
                return false;
            default:
                break;
        }
        return true;
    }

    public void GetAnswer() => GetUserInput("Answer: " + _output + "\nPress Return button to continue.");

    private string GetFishText(ushort wordsAmount)
    {
        StringBuilder resultBuilder = new StringBuilder();
        try
        {
            using (var streamReader = new StreamReader("./fish.txt"))
            {
                for (int? currentChar = null;
                    !streamReader.EndOfStream && wordsAmount > 0;
                    currentChar = streamReader.Read())
                {
                    resultBuilder.Append((char?)currentChar);
                    if (currentChar == ' ') wordsAmount--;
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Error reading fish.txt - " + e.Message);
        }
        return resultBuilder.ToString();

    }

    private float GetCalculationResult(float x, float y) => x * y;

}

