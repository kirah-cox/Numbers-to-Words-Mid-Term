﻿using MyTools;
using MyEnums;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<char, string> singleNumberDictionary = new Dictionary<char, string>()
        {
            { '0', "" },
            { '1', "one" },
            { '2',  "two" },
            { '3',  "three" },
            { '4',  "four" },
            { '5',  "five" },
            { '6',  "six" },
            { '7',  "seven" },
            { '8',  "eight" },
            { '9',  "nine" },

        };
        Dictionary<string, string> doubleNumberDictionary = new Dictionary<string, string>()
        {
            { "10",  "ten" },
            { "11",  "eleven" },
            { "12",  "twelve" },
            { "13",  "thirteen" },
            { "14",  "fourteen" },
            { "15",  "fifteen" },
            { "16",  "sixteen" },
            { "17",  "seventeen" },
            { "18",  "eighteen" },
            { "19",  "nineteen" },
            { "2",  "twenty" },
            { "3",  "thirty" },
            { "4",  "forty" },
            { "5",  "fifty" },
            { "6",  "sixty" },
            { "7",  "seventy" },
            { "8",  "eighty" },
            { "9",  "ninety" },
        };

        Console.WriteLine("Hello, welcome to the bank! Select an option:");

        while (!Tools.ExitSystem)
        {
            int optionResponse = 0;
            bool correctResponse = false;
            while (!correctResponse)
            {
                Console.WriteLine($"1: {MainMenu.View} Account");
                Console.WriteLine($"2: {MainMenu.Deposit} into Account");
                Console.WriteLine($"3: {MainMenu.Withdraw} from Account");
                Console.WriteLine($"4: {MainMenu.Exit}");

                if (!int.TryParse(Console.ReadLine().Trim(), out optionResponse))
                {
                    Console.WriteLine("Please select a valid number option.");
                }
                else
                {
                    correctResponse = true;
                }
            }

            bool passedCheck = false;

            switch ((MainMenu)optionResponse)
            {
                case MainMenu.View:
                    Tools.AccountToWords(singleNumberDictionary, doubleNumberDictionary);
                    break;

                case MainMenu.Deposit:
                    Console.WriteLine("How much would you like to deposit?");
                    passedCheck = Tools.CheckAndConvertResponseToDecimal();
                    if (!passedCheck)
                    {
                        break;
                    }
                    Tools.ResponseToWords(singleNumberDictionary, doubleNumberDictionary);
                    Tools.Deposit(Tools.DecimalAmountResponse);
                    break;

                case MainMenu.Withdraw:
                    Console.WriteLine("How much would you like to withdraw?");
                    passedCheck = Tools.CheckAndConvertResponseToDecimal();
                    if (!passedCheck)
                    {
                        break;
                    }
                    Tools.ResponseToWords(singleNumberDictionary, doubleNumberDictionary);
                    Tools.Withdraw(Tools.DecimalAmountResponse);
                    break;

                case MainMenu.Exit:
                    Console.WriteLine("Thank you for using the bank! Exiting the system.");
                    Tools.ExitSystem = true;
                    break;

                default:
                    Console.WriteLine("Please select a valid number option.");
                    break;
            }
        }
    }
}