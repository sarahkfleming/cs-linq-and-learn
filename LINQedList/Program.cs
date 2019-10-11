using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQedList
{
    class Program
    {
        static void Main(string[] args)
        {

            // Given the collections of items shown below, use LINQ to build the requested collection, and then Console.WriteLine() each item in that resulting collection.

            // Find the words in the collection that start with the letter 'L'
            List<string> fruits = new List<string>() { "Lemon", "Apple", "Orange", "Lime", "Watermelon", "Loganberry" };

            IEnumerable<string> LFruits = fruits.Where(fruit => fruit.StartsWith("L"));

            foreach (string fruit in LFruits)
            {
                Console.WriteLine(fruit);
                Console.WriteLine();
            }

            // Which of the following numbers are multiples of 4 or 6
            List<int> numbers = new List<int>()
                {
                    15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
                };

            IEnumerable<int> fourSixMultiples = numbers.Where(number => number % 6 == 0 || number % 4 == 0);

            foreach (int number in fourSixMultiples)
            {
                Console.WriteLine(number);
                Console.WriteLine();
            }

            // Order these student names alphabetically, in descending order (Z to A)
            List<string> names = new List<string>()
                {
                    "Heather", "James", "Xavier", "Michelle", "Brian", "Nina",
                    "Kathleen", "Sophia", "Amir", "Douglas", "Zarley", "Beatrice",
                    "Theodora", "William", "Svetlana", "Charisse", "Yolanda",
                    "Gregorio", "Jean-Paul", "Evangelina", "Viktor", "Jacqueline",
                    "Francisco", "Tre"
                };

            List<string> descend = names.OrderBy(name => name).ToList();

            foreach (string name in descend)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            // Build a collection of these numbers sorted in ascending order
            List<int> sortedNumbers = numbers.OrderBy(number => number).ToList();

            foreach (int number in sortedNumbers)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine();

            // Output how many numbers are in this list      
            Console.WriteLine($"Numbers count: {numbers.Count()}");

            // How much money have we made?
            List<double> purchases = new List<double>()
                {
                    2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
                };

            Console.WriteLine($"Sum of purchases: ${purchases.Sum()}");

            // What is our most expensive product?
            List<double> prices = new List<double>()
                {
                    879.45, 9442.85, 2454.63, 45.65, 2340.29, 34.03, 4786.45, 745.31, 21.76
                };

            Console.WriteLine($"The highest price is ${prices.Max()}");
            Console.WriteLine();


            /*
            Store each number in the following List until a perfect square
            is detected.

            Ref: https://msdn.microsoft.com/en-us/library/system.math.sqrt(v=vs.110).aspx
            */

            List<int> wheresSquaredo = new List<int>()
                {
                    66, 12, 8, 27, 82, 34, 7, 50, 19, 46, 81, 23, 30, 4, 68, 14
                };

            List<int> noSquareRoots = wheresSquaredo.TakeWhile(number => Math.Sqrt(number) % 1 != 0).ToList();

            foreach (int number in noSquareRoots)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine();

            List<Customer> customers = new List<Customer>() {
            new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
            new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
            new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
            new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
            new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
            new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
            new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
            new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
            new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
            new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
        };

            /*
            Given the same customer set, display how many millionaires per bank.
            Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq

            Example Output:
            WF 2
            BOA 1
            FTB 1
            CITI 1
            */

            List<Customer> millionaires = customers
                .Where(customer => customer.Balance > 999999)
                .ToList();
           
            var millionairesByBank = millionaires.GroupBy(
                customer => customer.Bank,
                (key, value) => new
                {
                    BankName = key,
                    MillionaireCount = value.Count()
                });

            foreach (var millionaire in millionairesByBank)
            {
                Console.WriteLine($"{millionaire.BankName} customers: {millionaire.MillionaireCount}");
            }

            /* Challenge Exercise: Introduction to Joining Two Related Collections
            As a light introduction to working with relational databases, this example works with two collections of data - banks and customers - that are related through the Bank attribute on the customer. In that attribute, we store the abbreviation for a bank. However, we want to get the full name of the bank when we produce our output. This is called joining the collections together. */

            /* TASK:
            As in the previous exercise, you're going to output the millionaires,
            but you will also display the full name of the bank. You also need
            to sort the millionaires' names, ascending by their LAST name.

            Example output:
                Tina Fey at Citibank
                Joe Landy at Wells Fargo
                Sarah Ng at First Tennessee
                Les Paul at Wells Fargo
                Peg Vale at Bank of America */

            // Create some banks and store in a List
            List<Bank> banks = new List<Bank>() {
            new Bank(){ Name="First Tennessee", Symbol="FTB"},
            new Bank(){ Name="Wells Fargo", Symbol="WF"},
            new Bank(){ Name="Bank of America", Symbol="BOA"},
            new Bank(){ Name="Citibank", Symbol="CITI"},
        };

            /*
                You will need to use the `Where()`
                and `Select()` methods to generate
                instances of the following class.

                public class ReportItem
                {
                    public string CustomerName { get; set; }
                    public string BankName { get; set; }
                }
            */

            List<ReportItem> millionaireReport = millionaires.Join(
                banks,
                millionaire => millionaire.Bank,
                bank => bank.Symbol,
                (millionaire, bank) =>
                    new ReportItem
                    {
                        CustomerName = millionaire.Name,
                        BankName = bank.Name
                    }
            ).ToList();

            Console.WriteLine();                        
            
            foreach (var item in millionaireReport)
            {
                Console.WriteLine($"{item.CustomerName} at {item.BankName}");
            }
        }
    }
}
