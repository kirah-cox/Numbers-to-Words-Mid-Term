using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEnums;

namespace MyTools
{
    public class Tools
    {
        public static decimal AmountInAccount { get; set; } = 0.00m;
        public static decimal DecimalAmountResponse { get; set; } = 0;
        public static string AmountResponse { get; set; } = "";
        public static string NumbersAsWords { get; set; } = "";
        public static bool HaveOnlyOneDollar { get; set; } = false;
        public static int ForeachLineNumber { get; set; } = 0;
        public static bool ExitSystem { get; set; } = false;

        public static bool CheckAndConvertResponseToDecimal()
        {
            AmountResponse = Console.ReadLine();
            char[] charAmountResponse = AmountResponse.ToCharArray();
            string updatedAmountResponse = "";

            if (AmountResponse == "" || AmountResponse == null)
            {
                Console.WriteLine("Please put the amount in the format of xxx,xxx.xx with no zeroes in front.");
                return false;
            }

            if (charAmountResponse[0] == '-')
            {
                Console.WriteLine("You cannot deposit or withdraw a negative number.");
                return false;
            }

            foreach (char c in charAmountResponse)
            {
                if (c != ',')
                {
                    updatedAmountResponse += c;
                }
            }

            if (!decimal.TryParse(updatedAmountResponse, out decimal convertedAmountResponse))
            {
                Console.WriteLine("The amount needs to be in number form. Please put the amount in the format of xxx,xxx.xx with no zeroes in front.");
                return false;
            }

            if (convertedAmountResponse < 1.00m)
            {
                Console.WriteLine("The minimum amount to deposit or withdraw is $1.00.");
                return false;
            }

            string checkResponse = convertedAmountResponse.ToString("#,0.00");

            if (checkResponse != AmountResponse)
            {
                Console.WriteLine("Please put the amount in the format of xxx,xxx.xx with no zeroes in front.");
                return false;
            }

            DecimalAmountResponse = convertedAmountResponse;
            return true;
        }


        public static void Deposit(decimal amount)
        {
            if (AmountInAccount + amount > 999_999_999_999.99m)
            {
                Console.WriteLine("You cannot have more than $999,999,999,999.99 in your account.");
                return;
            }

            AmountInAccount += amount;

            Console.WriteLine($"You have deposited {NumbersAsWords}.");

            DecimalAmountResponse = 0;
        }

        public static void Withdraw(decimal amount)
        {
            if (AmountInAccount - amount < 0.00m)
            {
                Console.WriteLine("You do not have enough in your account to withdraw that amount.");
                return;
            }

            AmountInAccount -= amount;

            Console.WriteLine($"You have withdrawn {NumbersAsWords}.");

            DecimalAmountResponse = 0;
        }

        public static void AccountToWords(Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary)
        {
            NumbersAsWords = "";
            ForeachLineNumber = 0;
            HaveOnlyOneDollar = false;

            int amountInAccountLength = AmountInAccount.ToString().Length;
            string localAmountInAccount = AmountInAccount.ToString();
            
            string amountInAccount = AmountInAccount.ToString("#,0.00");
            string[] amountInAccountArray = amountInAccount.Split(',');

            foreach (string amount in amountInAccountArray)
            {
                MainConvert(amount, singleNumberDictionary, doubleNumberDictionary, amountInAccountLength, localAmountInAccount);

                ForeachLineNumber++;
            }

            NumbersAsWords = NumbersAsWords.Trim();

            Console.WriteLine($"You have {NumbersAsWords} in your account.");
        }

        public static void ResponseToWords(Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary)
        {
            NumbersAsWords = "";
            ForeachLineNumber = 0;
            HaveOnlyOneDollar = false;

            int trueAmountAccountLength = DecimalAmountResponse.ToString().Length;
            string localTrueAmountResponse = DecimalAmountResponse.ToString();

            string[] amountInResponse = AmountResponse.Split(',');

            foreach (string amount in amountInResponse)
            {
                MainConvert(amount, singleNumberDictionary, doubleNumberDictionary, trueAmountAccountLength, localTrueAmountResponse);

                ForeachLineNumber++;
            }

            NumbersAsWords = NumbersAsWords.Trim();
        }

        public static void MainConvert(string amount, Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary, int numberLength, string localTrueAndAccount)
        {
            switch (amount.Length)
            {
                case 1:
                    ConvertWithOneNumber(amount, singleNumberDictionary, doubleNumberDictionary, numberLength, localTrueAndAccount);
                    break;

                case 2:
                    ConvertWithTwoNumbers(amount, singleNumberDictionary, doubleNumberDictionary, numberLength, localTrueAndAccount);
                    break;

                case 3:
                    ConvertWithThreeNumbers(amount, singleNumberDictionary, doubleNumberDictionary, numberLength, localTrueAndAccount);
                    break;

                case 4:
                    string twoCharAmount = $"{amount[2]}{amount[3]}";
                    ConvertWithOneNumber(amount, singleNumberDictionary, doubleNumberDictionary, numberLength, localTrueAndAccount);
                    ConvertWithDecimals(twoCharAmount, singleNumberDictionary, doubleNumberDictionary);
                    break;

                case 5:
                    twoCharAmount = $"{amount[3]}{amount[4]}";

                    ConvertWithTwoNumbers(amount, singleNumberDictionary, doubleNumberDictionary, numberLength, localTrueAndAccount);
                    ConvertWithDecimals(twoCharAmount, singleNumberDictionary, doubleNumberDictionary);
                    break;

                case 6:
                    twoCharAmount = $"{amount[4]}{amount[5]}";

                    ConvertWithThreeNumbers(amount, singleNumberDictionary, doubleNumberDictionary, numberLength, localTrueAndAccount);
                    ConvertWithDecimals(twoCharAmount, singleNumberDictionary, doubleNumberDictionary);
                    break;
                default:
                    break;
            }
        }

        public static void ConvertWithThreeNumbers(string amount, Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary, int numberLength, string localTrueAndAccount)
        {
            string doubleAmount = $"{amount[1]}{amount[2]}";

            if (amount[0] == '0')
            {
                if (amount[1] == '0' && amount[2] == '0')
                {
                    NumbersAsWords += "";
                    return;
                }
                else if (singleNumberDictionary.ContainsKey(amount[2]) && amount[1] == '0')
                {
                    NumbersAsWords += $" {singleNumberDictionary[amount[2]]}";
                }
                else if (doubleNumberDictionary.ContainsKey(amount[1].ToString()) && amount[2] != '0')
                {
                    NumbersAsWords += $" {doubleNumberDictionary[amount[1].ToString()]} {singleNumberDictionary[amount[2]]}";
                }
                else if (singleNumberDictionary.ContainsKey(amount[2]) && amount[1] == '0')
                {
                    NumbersAsWords += $" {singleNumberDictionary[amount[2]]}";
                }
                else if (doubleNumberDictionary.ContainsKey(amount[1].ToString()))
                {
                    NumbersAsWords += $" {doubleNumberDictionary[amount[1].ToString()]}";
                }
                else if (doubleNumberDictionary.ContainsKey(doubleAmount))
                {
                    NumbersAsWords += $" {doubleNumberDictionary[doubleAmount]}";
                }
            }
            else
            {
                NumbersAsWords += $" {singleNumberDictionary[amount[0]]} hundred";

                if (amount[1] == '0' && amount[2] == '0')
                {
                }
                else if (doubleNumberDictionary.ContainsKey(amount[1].ToString()) && amount[2] != '0')
                {
                    NumbersAsWords += $" and {doubleNumberDictionary[amount[1].ToString()]} {singleNumberDictionary[amount[2]]}";
                }
                else if (singleNumberDictionary.ContainsKey(amount[2]) && amount[1] == '0')
                {
                    NumbersAsWords += $" and {singleNumberDictionary[amount[2]]}";
                }
                else if (doubleNumberDictionary.ContainsKey(amount[1].ToString()))
                {
                    NumbersAsWords += $" and {doubleNumberDictionary[amount[1].ToString()]}";
                }
                else if (doubleNumberDictionary.ContainsKey(doubleAmount))
                {
                    NumbersAsWords += $" and {doubleNumberDictionary[doubleAmount]}";
                }
            }

            ConvertLargeNumbers(numberLength, localTrueAndAccount);
        }

        public static void ConvertWithTwoNumbers(string amount, Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary, int numberLength, string localTrueAndAccount)
        {
            string doubleAmount = $"{amount[0]}{amount[1]}";

            if (doubleNumberDictionary.ContainsKey(amount[0].ToString()) && amount[1] != '0')
            {
                NumbersAsWords += $" {doubleNumberDictionary[amount[0].ToString()]} {singleNumberDictionary[amount[1]]}";
            }
            else if (doubleNumberDictionary.ContainsKey(amount[0].ToString()))
            {
                NumbersAsWords += $" {doubleNumberDictionary[amount[0].ToString()]}";
            }
            else if (doubleNumberDictionary.ContainsKey(doubleAmount))
            {
                NumbersAsWords += $" {doubleNumberDictionary[doubleAmount]}";
            }

            ConvertLargeNumbers(numberLength, localTrueAndAccount);
        }

        public static void ConvertWithOneNumber(string amount, Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary, int numberLength, string localTrueAndAccount)
        {
            if (amount[0] == '0')
            {
                NumbersAsWords += " zero";
            }
            else if (singleNumberDictionary.ContainsKey(amount[0]))
            {
                NumbersAsWords += $" {singleNumberDictionary[amount[0]]}";
            }

            HaveOnlyOneDollar = (amount[0] == '1' && numberLength == 4) ? true : false;

            ConvertLargeNumbers(numberLength, localTrueAndAccount);
        }

        public static void ConvertWithDecimals(string twoCharAmount, Dictionary<char, string> singleNumberDictionary, Dictionary<string, string> doubleNumberDictionary)
        {

            string doubleAmount = $"{twoCharAmount[0]}{twoCharAmount[1]}";

            if (!HaveOnlyOneDollar && doubleAmount == "00")
            {
                NumbersAsWords += " dollars and zero cents";
            }
            else if (HaveOnlyOneDollar && doubleAmount == "00")
            {
                NumbersAsWords += " dollar and zero cents";
            }
            else if (HaveOnlyOneDollar && doubleAmount != "01")
            {
                if (doubleNumberDictionary.ContainsKey(twoCharAmount[0].ToString()) && twoCharAmount[1] != '0')
                {
                    NumbersAsWords += $" dollar and {doubleNumberDictionary[twoCharAmount[0].ToString()]} {singleNumberDictionary[twoCharAmount[1]]} cents";
                }
                else if (singleNumberDictionary.ContainsKey(twoCharAmount[1]) && twoCharAmount[0] == '0')
                {
                    NumbersAsWords += $" dollar and {singleNumberDictionary[twoCharAmount[1]]} cents";
                }
                else if (doubleNumberDictionary.ContainsKey(twoCharAmount[0].ToString()))
                {
                    NumbersAsWords += $" dollar and {doubleNumberDictionary[twoCharAmount[0].ToString()]} cents";
                }
                else if (doubleNumberDictionary.ContainsKey(doubleAmount))
                {
                    NumbersAsWords += $" dollar and {doubleNumberDictionary[doubleAmount]} cents";
                }
            }
            else if (HaveOnlyOneDollar && doubleAmount == "01")
            {
                NumbersAsWords += $" dollar and one cent";
            }
            else if (!HaveOnlyOneDollar && doubleAmount == "01")
            {
                NumbersAsWords += $" dollars and one cent";
            }
            else
            {
                if (doubleNumberDictionary.ContainsKey(twoCharAmount[0].ToString()) && twoCharAmount[1] != '0')
                {
                    NumbersAsWords += $" dollars and {doubleNumberDictionary[twoCharAmount[0].ToString()]} {singleNumberDictionary[twoCharAmount[1]]} cents";
                }
                else if (singleNumberDictionary.ContainsKey(twoCharAmount[1]) && twoCharAmount[0] == '0')
                {
                    NumbersAsWords += $" dollars and {singleNumberDictionary[twoCharAmount[1]]} cents";
                }
                else if (doubleNumberDictionary.ContainsKey(twoCharAmount[0].ToString()))
                {
                    NumbersAsWords += $" dollars and {doubleNumberDictionary[twoCharAmount[0].ToString()]} cents";
                }
                else if (doubleNumberDictionary.ContainsKey(doubleAmount))
                {
                    NumbersAsWords += $" dollars and {doubleNumberDictionary[doubleAmount]} cents";
                }
            }
        }


        public static void ConvertLargeNumbers(int numberLength, string localTrueAndAccount)
        {
            string removedCharLocalTrueAndAccount = localTrueAndAccount.Substring(1);

            if (decimal.Parse(removedCharLocalTrueAndAccount) == 0.00m)
            {
                if ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 0)
                {
                    NumbersAsWords += " billion";
                }
                else if (((numberLength == 12 || numberLength == 11 || numberLength == 10) && ForeachLineNumber == 0) || ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 1))
                {
                    NumbersAsWords += " million";
                }
                else if (((numberLength == 9 || numberLength == 8 || numberLength == 7) && ForeachLineNumber == 0) || ((numberLength == 12 || numberLength == 11 || numberLength == 10) && ForeachLineNumber == 1) || ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 2))
                {
                    NumbersAsWords += " thousand";
                }
            }
            else if (decimal.Parse(removedCharLocalTrueAndAccount) < 99.99m)
            {
                if ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 0)
                {
                    NumbersAsWords += " billion and";
                }
                else if (((numberLength == 12 || numberLength == 11 || numberLength == 10) && ForeachLineNumber == 0) || ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 1))
                {
                    NumbersAsWords += " million and";
                }
                else if (((numberLength == 9 || numberLength == 8 || numberLength == 7) && ForeachLineNumber == 0) || ((numberLength == 12 || numberLength == 11 || numberLength == 10) && ForeachLineNumber == 1) || ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 2))
                {
                    NumbersAsWords += " thousand and";
                }
            }
            else if ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 0)
            {
                NumbersAsWords += " billion,";
            }
            else if (((numberLength == 12 || numberLength == 11 || numberLength == 10) && ForeachLineNumber == 0) || ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 1))
            {
                NumbersAsWords += " million,";
            }
            else if (((numberLength == 9 || numberLength == 8 || numberLength == 7) && ForeachLineNumber == 0) || ((numberLength == 12 || numberLength == 11 || numberLength == 10) && ForeachLineNumber == 1) || ((numberLength == 15 || numberLength == 14 || numberLength == 13) && ForeachLineNumber == 2))
            {
                NumbersAsWords += " thousand,";
            }
        }
    }
}
