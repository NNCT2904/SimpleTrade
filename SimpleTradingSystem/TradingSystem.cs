using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace SimpleTradingSystem
{
    public enum MenuOptions
    {
        [Display(Name = "List all accounts")]
        ListAllAccount = 1,
        [Display(Name = "Add an Item to an account")]
        AddNewItem = 2,
        [Display (Name = "Display an Account's Items")]
        DisplayItem = 3,
        [Display(Name = "Trade items")]
        TradeItem = 4,
        [Display(Name = "Add a new Account")]
        AddNewAccount = 5,
        [Display(Name = "Quit")]
        Quit = 6,
    }

    class TradingSystem
    {
        static void Main(string[] args)
        {
            Market thisMarket = new Market();
            Account ncng = new Account("Thanh");
            Account dummy = new Account("Test");
            thisMarket.AddAccount(ncng);
            thisMarket.AddAccount(dummy);
            Item a = new Item("Note 9", "Samsung");
            Item b = new Item("Dell G7", "Dell, GTX 1050ti");
            Item c = new Item("Dualshock 4", "Sony, controller");
            Item d = new Item("Nintendo Switch", "Nintendo");
            ncng.AddItem(a);
            ncng.AddItem(b);
            ncng.AddItem(c);
            ncng.AddItem(d);
            while (true)
            {
                //Clear Console each time some thing is executed
                Console.Clear();
                Console.WriteLine("-------A simple Trading System by Thanh Nguyen-------");
                Console.WriteLine($"\nWelcome, admin\n");
                //Print UI, take actions based on user's Input
                MenuOptions option = ReadUserInput();
                switch (option)
                {
                    case MenuOptions.ListAllAccount:
                        {
                            ListAllAccount(thisMarket);
                            break;
                        }
                    case MenuOptions.AddNewItem:
                        {
                            AddNewItem(thisMarket);
                            break;
                        }
                    case MenuOptions.DisplayItem:
                        {
                            DisplayItems(thisMarket);
                            break;
                        }
                    case MenuOptions.TradeItem:
                        {
                            Trade(thisMarket);
                            break;
                        }
                    case MenuOptions.AddNewAccount:
                        {
                            AddNewAccount(thisMarket);
                            break;
                        }
                    case MenuOptions.Quit:
                        {
                            return;
                        }
                }
            }
        }
        //Methods
        private static void ListAllAccount(Market market)
        {
            market.PrintAllAccount();
            Console.ReadKey();
        }
        private static void AddNewItem(Market market)
        {
            Account account = FindAccount(market);
            if (account != null)
            {
                Console.WriteLine("New item name?");
                string name = Console.ReadLine();
                Console.WriteLine("New item's description?");
                string desc = Console.ReadLine();

                account.AddItem(new Item(name, desc));
            }
            else
            {
                Console.WriteLine("Action canceled!");
                Console.ReadKey();
            }

        }

        private static void DisplayItems(Market market)
        {
            Account account = FindAccount(market);
            if (account != null)
            {
                account.PrintAccount();
                account.PrintItems();
                
            }
            else Console.WriteLine("Action canceled!");
            Console.ReadKey();
        }

        private static void AddNewAccount(Market market)
        {
            Console.WriteLine("Input new account name:");
            string accountName = Console.ReadLine();
            market.AddAccount(new Account(accountName));
        }

        private static void Trade(Market market)
        {
            Console.WriteLine("From account: ");
            Account fromAccount = FindAccount(market);
            if (fromAccount == null)
            {
                Console.WriteLine("Transaction canceled!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("To account: ");
            Account toAccount = FindAccount(market);
            if (fromAccount != null && toAccount != null)
            {
                Console.WriteLine($"Which item from {fromAccount.Name}?");
                Item fromItem = FindItem(fromAccount);
                Console.WriteLine($"Which item from {toAccount.Name}?");
                Item toItem = FindItem(toAccount);

                Transaction tradeTransaction = new Transaction(fromAccount, toAccount, fromItem, toItem);
                market.ExecuteTransaction(tradeTransaction);
            }
            else
            {
                Console.WriteLine("Transaction canceled!");
                Console.ReadKey();
                return;
            }


        }

        private static Item FindItem(Account account)
        {
            account.PrintItems();
            Console.WriteLine("Choose an item");
            int userInput = InputToInt(Console.ReadLine());
            return account.Items[userInput - 1];
        }

        private static Account FindAccount(Market market)
        {
            Console.WriteLine("Input an account name: ");
            Account search = market.Search(Console.ReadLine());
            if (search != null)
            {
                Console.WriteLine("Search Result:");
                search.PrintAccount();
            }
            else Console.WriteLine("No search result!");
            return search;
        }

        private static MenuOptions ReadUserInput()
        {
            int userInput;

            //Print out the menu options, add more options "MenuOptions" enum above will print more options
            Console.WriteLine("Trading System Options");
            foreach (MenuOptions choices in Enum.GetValues(typeof(MenuOptions)))
            {
                Console.WriteLine("-{0}. {1}", (int)choices, GetMenuOptionDisplay(choices));
            }
            Console.Write("Select one option: ");

            //User input validation, stricted to range of the item in MenuOptions
            do
            {
                userInput = InputToInt(Console.ReadLine());
                if (userInput <= 0 || userInput > Enum.GetValues(typeof(MenuOptions)).Length)
                {
                    Console.WriteLine("Unknown option, try again!");
                }
            } while (userInput <= 0 || userInput > Enum.GetValues(typeof(MenuOptions)).Length);

            //Return the option based on the user input, expected Number of options in Menu
            return (MenuOptions)userInput;
        }

        private static bool YNQuestion(string message)
        {
            Console.WriteLine(message);
            string proceed;
            do
            {
                proceed = Console.ReadLine().ToLower();
                switch (proceed)
                {
                    case "y":
                        {
                            return true;
                        }
                    case "n":
                        {
                            return false;
                        }
                    default:
                        {
                            Console.WriteLine("Unknown option, please try again");
                            proceed = Console.ReadLine().ToLower();
                            break;
                        }
                }
            } while (proceed != "y" && proceed != "n" && string.IsNullOrEmpty(proceed));
            return false;
        }
        //Only allow inout as Integer
        private static int InputToInt(string inputNumberAsString)
        {
            int inputNumber;
            while (!int.TryParse(inputNumberAsString, out inputNumber))
            {
                Console.WriteLine("This is not quite a number");
                inputNumberAsString = Console.ReadLine();
            }
            return inputNumber;
        }

        private static string GetMenuOptionDisplay(MenuOptions option)
        {
            var fieldInfo = option.GetType().GetField(option.ToString());

            DisplayAttribute[] descriptionAttribute = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            //If there is an Display attribute then return it, else retuen the option in string.
            return (descriptionAttribute.Length > 0) ? descriptionAttribute[0].Name : option.ToString();
        }
    }
}
