using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netcore.SyntaxParser.app.Utils
{
    public static class Parser
    {
        private static readonly List<(string Type, string Content)> Tokens = new();

        /// <summary>
        /// Tokenizes the input string into operators, numbers, identifiers, and assignment tokens.
        /// </summary>
        /// <param name="input">The input string to tokenize.</param>
        public static void Tokenize(string input)
        {
            Tokens.Clear();
            var operators = new[] { '+', '-', '*', '/' };

            try
            {
                for (int i = 0; i < input.Length; i++)
                {
                    char c = input[i];

                    if (char.IsWhiteSpace(c))
                    {
                        continue;
                    }
                    else if (c == '=')
                    {
                        AddToken("TOKEN_ASSIGN", c.ToString());
                    }
                    else if (operators.Contains(c))
                    {
                        AddToken("TOKEN_OP", c.ToString());
                    }
                    else if (char.IsDigit(c))
                    {
                        i = HandleNumber(input, i);
                    }
                    else if (char.IsLetter(c) || c == '_')
                    {
                        i = HandleIdentifier(input, i);
                    }
                    else
                    {
                        throw new Exception($"Unexpected character: {c}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Token: ->{input}<- not recognized!");
            }
        }

        /// <summary>
        /// Validates if the tokenized input matches the assignment line grammar.
        /// </summary>
        /// <returns>True if valid, otherwise false.</returns>
        public static bool ValidateAssignmentLine()
        {
            if (Tokens.Count == 0) return false;

            int index = 0;

            // <ID>
            if (!Match("TOKEN_ID", ref index)) return false;

            // "="
            if (!Match("TOKEN_ASSIGN", ref index)) return false;

            // <expressao>
            if (!ValidateExpression(ref index)) return false;

            // Ensure all tokens are consumed
            return index == Tokens.Count;
        }

        private static bool ValidateExpression(ref int index)
        {
            // <termo>
            if (!ValidateTerm(ref index)) return false;

            // { <op> <termo> }
            while (index < Tokens.Count && Tokens[index].Type == "TOKEN_OP")
            {
                index++; // Consume <op>
                if (!ValidateTerm(ref index)) return false;
            }

            return true;
        }

        private static bool ValidateTerm(ref int index)
        {
            if (index >= Tokens.Count) return false;

            if (Tokens[index].Type == "TOKEN_ID" || Tokens[index].Type == "TOKEN_NUM")
            {
                index++; // Consume <ID> or <NUM>
                return true;
            }

            return false;
        }

        private static bool Match(string expectedType, ref int index)
        {
            if (index < Tokens.Count && Tokens[index].Type == expectedType)
            {
                index++;
                return true;
            }
            return false;
        }

        private static int HandleNumber(string input, int startIndex)
        {
            var number = new StringBuilder();
            int i = startIndex;

            while (i < input.Length && char.IsDigit(input[i]))
            {
                number.Append(input[i]);
                i++;
            }
            i--; // Adjust the index after the loop
            AddToken("TOKEN_NUM", number.ToString());

            return i;
        }

        private static int HandleIdentifier(string input, int startIndex)
        {
            var identifier = new StringBuilder();
            int i = startIndex;

            while (i < input.Length && (char.IsLetterOrDigit(input[i]) || input[i] == '_'))
            {
                identifier.Append(input[i]);
                i++;
            }
            i--; // Adjust the index after the loop
            AddToken("TOKEN_ID", identifier.ToString());

            return i;
        }

        private static void AddToken(string type, string content)
        {
            Tokens.Add((type, content));
        }

        public static void PrintTokens()
        {
            foreach (var token in Tokens)
            {
                Console.WriteLine($"Type: {token.Type}, Content: {token.Content}");
            }
        }
    }
}
