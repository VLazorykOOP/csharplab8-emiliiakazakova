using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static string InsertTextAfterTargetWord(string originalText, string insertText, string targetWord)
    {
        string[] words = originalText.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        StringBuilder resultBuilder = new StringBuilder();
        foreach (string word in words)
        {
            resultBuilder.Append(word);
            resultBuilder.Append(' ');

            if (word.Equals(targetWord))
            {
                resultBuilder.Append(insertText);
                resultBuilder.Append(' ');
            }
        }

        return resultBuilder.ToString().Trim();
    }

    static void WriteNonDelimiterCharactersToFile(string filePath, string sentence)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (char character in sentence)
            {
                if (!IsDelimiter(character))
                {
                    writer.Write(character);
                }
            }
        }
    }

    static bool IsDelimiter(char character)
    {
        return Char.IsWhiteSpace(character) || Char.IsPunctuation(character);
    }


    static void ProcessText(string inputText)
    {
        Regex regex = new Regex(@"\[(.*?)\]");

        string replacedText = regex.Replace(inputText, match => {
            return new string('X', match.Length);
        });

        Console.WriteLine("Замінений текст:");
        Console.WriteLine(replacedText);

        string outputPath = "output5.txt";
        File.WriteAllText(outputPath, replacedText);

        Console.WriteLine($"Створено файл: {outputPath}");
    }
    static void Main()
    {
        Console.WriteLine("Task1");
        string inputFilePath = "input.txt";

        string outputFilePath = "output.txt";

        string formatPattern = @"\(\s*\d+\s*,\s*\d+\s*\)";

        try
        {
            string content = File.ReadAllText(inputFilePath);

            MatchCollection matches = Regex.Matches(content, formatPattern);

            int vectorCount = matches.Count;

            File.WriteAllLines(outputFilePath, matches.Cast<Match>().Select(match => match.Value));


            Console.WriteLine($"Кількість знайдених векторів: {vectorCount}");
            Console.WriteLine($"Вектори записано у файл: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\n\nTask2\n\n");

        string inputFilePath1 = "input2.txt";
        string outputFilePath1 = "output2.txt";

        string inputText = File.ReadAllText(inputFilePath1);

        string pattern = @"\b[a-zA-Z_][a-zA-Z0-9_]*\b";

        MatchCollection matches1 = Regex.Matches(inputText, pattern);

        string resultText = Regex.Replace(inputText, pattern, "");

        File.WriteAllText(outputFilePath1, resultText);

        Console.WriteLine($"Кількість вилучених ідентифікаторів: {matches1.Count}");

        Console.WriteLine("\n\nTask3\n\n");

        try
        {
            string firstText = File.ReadAllText("input3.txt");

            string secondText = File.ReadAllText("input3_1.txt");

            string targetWord = "target";

            string result = InsertTextAfterTargetWord(firstText, secondText, targetWord);

            File.WriteAllText("output3.txt", result);

            Console.WriteLine("Результат збережено у файлі 'output3.txt'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }


        Console.WriteLine("\n\nTask4\n\n");


        try
        {
            // Пропозиція
            string sentence = "I need to, Enter some words and articule? For cheak how it works.";

            string filePath = "output4.txt";

            WriteNonDelimiterCharactersToFile(filePath, sentence);

            Console.WriteLine("Вміст файлу:");
            Console.WriteLine(File.ReadAllText(filePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }



        Console.WriteLine("\n\nTask4\n\n");


        try
        {
            string inputText5 = "Це [текст] з кутовими дужками.";

            ProcessText(inputText5);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася помилка: {ex.Message}");
        }
    }
}