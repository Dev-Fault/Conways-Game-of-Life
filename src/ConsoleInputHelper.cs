using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ConewaysGameOfLife
{
    public static class ConsoleInputHelper
    {
        private static readonly char[] yesNoChars = new char[2] { 'y', 'n' };
        private const string incorrectOption = "Please enter a valid option.";
        private const string fileNotFound = "File not found, try again.";

        public static void State_Message(string message)
        {
            Console.WriteLine(message);
        }

        public static char Ask_Char(string query, char[] options)
        {
            Console.WriteLine(query); 
            char selectedChar = ' ';
            bool validInput = false;
            while (!validInput)
            {
                string queryInput = Console.ReadLine(); 
                try
                {
                    selectedChar = Convert.ToChar(queryInput);
                    bool validChar = false;
                    for (int i = 0; i != options.Length; i++)
                    {
                        if (options[i] == selectedChar)
                        {
                            validChar = true;
                            break;
                        }
                    }
                    if (!validChar)
                    {
                        Console.WriteLine(incorrectOption);
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine(incorrectOption);
                    continue;
                }
                validInput = true;
            }

            return selectedChar;
        }

        public static char Ask_Char(string query)
        {
            Console.WriteLine(query);
            char selectedChar = ' ';
            bool validInput = false;
            while (!validInput)
            {
                string queryInput = Console.ReadLine();
                try
                {
                    selectedChar = Convert.ToChar(queryInput);
                }
                catch
                {
                    Console.WriteLine(incorrectOption);
                    continue;
                }
                validInput = true;
            }

            return selectedChar;
        }

        public static char Ask_YesNo(string query)
        {
            char answer = Ask_Char(query, yesNoChars);
            return answer;
        }

        public static int Ask_Integer(string query, int minRange, int maxRange)
        {
            Console.WriteLine(query);
            int loadOption = 0;
            bool validInput = false;
            while (!validInput)
            {
                string queryInput = Console.ReadLine();
                try
                {
                    loadOption = Convert.ToInt32(queryInput);
                    if (loadOption < minRange || loadOption > maxRange)
                    {
                        Console.WriteLine(incorrectOption);
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine(incorrectOption);
                    continue;
                }
                validInput = true;
            }
            return loadOption;
        }

        public static string Ask_FilePath(string query)
        {
            string path = "";
            bool validInput = false;
            Console.WriteLine(query);
            while (!validInput)
            {
                string inputPath = Console.ReadLine();
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine(fileNotFound);
                    continue;
                }
                validInput = true;
                path = inputPath;
            }
            return path;
        }
    }
}
