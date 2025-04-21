using Netcore.SyntaxParser.app.Utils;

class Program
{
    static void Main()
    {

        string filePath = "Files\\input.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Arquivo não encontrado.");
            return;
        }

        List<string> lines = File.ReadAllLines(filePath).ToList();

        foreach (string line in lines)
        {
            Console.WriteLine($"Testing line: {line}");
            Parser.Tokenize(line);
            Parser.PrintTokens();
            bool isValid = Parser.ValidateAssignmentLine();
            Console.WriteLine($"Is valid: {isValid}");
            Console.WriteLine("----------------------------");
        }
    }
}
