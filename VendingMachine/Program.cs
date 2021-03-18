using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace VendingMachine
{
    class Program
    {

        public static List<Items> ListItems = File.ReadAllLines("Data.csv").Skip(1).Select(v => CSV.CSVData(v)).ToList();

        public static int calculate(int ID, int AmountTake)
        {
            var itemsunit = from d in ListItems
                            where d.ID.Equals(ID)
                            select d;
            int ReservedAmount = itemsunit.FirstOrDefault().Amount - AmountTake;
            var Reserved = ListItems.Where(x => x.ID == ID).FirstOrDefault();
            Reserved.Reserved = ReservedAmount;
            return ReservedAmount;
        }
        static void Cancel()
        {
            Console.Write("Thank You!");
        }
        static void Buying()
        {
            int Items = 0;
            int amount = 0;
            int Reserved = 0;
            Console.WriteLine("Here items bellow!");
            Console.WriteLine("[1] Biskuit (6000)");
            Console.WriteLine("[2] Chips (8000)");
            Console.WriteLine("[3] Oreo (10000)");
            Console.WriteLine("[4] Tango (12000)");
            Console.WriteLine("[3] Cokelat (15000)");
            Console.Write("Please Select Your Option Above:");
            Items = Convert.ToInt32(Console.ReadLine());
            var itemsunit = from d in ListItems
                            where d.ID.Equals(Items)
                            select d;
            int AmountsItem = itemsunit.FirstOrDefault().Reserved;
            if (AmountsItem == 0)
            {
                AmountsItem = itemsunit.FirstOrDefault().Amount;
            }
            Console.WriteLine("Amount item rest:" + AmountsItem.ToString());
            Console.Write("Please insert amount you buy:");
            amount = Convert.ToInt32(Console.ReadLine());
            Reserved = calculate(Items, amount);
            if (Reserved < 0)
            {
                string choose = "";
                Console.WriteLine("Sorry items amount is not enough.");
                Console.Write("Do you want to change?Please choose Y/N -->");
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "Y":
                        Buying();
                        break;
                    case "N":
                        Cancel();
                        break;
                }
            }
            else
            {
                double money = 0;
                double totalmoney = 0;
                string choose = "";
                Console.Write("Please insert your money:");
                money = Convert.ToDouble(Console.ReadLine());
                if (money != 2000 || money != 5000 || money != 10000 || money != 20000 || money != 50000)
                {
                    Console.WriteLine("Please insert money with fractions 2000,5000,10000,20000,50000.");
                    money = Convert.ToInt32(Console.ReadLine());
                }
                else
                {
                    while (money < amount * itemsunit.FirstOrDefault().Price)
                    {
                        totalmoney = money;
                        Console.Write("Please insert your money. Or choose N if you want to cancel.");                        
                        money = Convert.ToInt32(Console.ReadLine());
                        if(choose=="N")
                        {
                            Cancel();
                            break;
                        }
                        else
                        {
                            if (money != 2000 || money != 5000 || money != 10000 || money != 20000 || money != 50000)
                            {
                                Console.WriteLine("Please insert money with fractions 2000,5000,10000,20000,50000.");
                                money = totalmoney;
                            }
                            else
                            {
                                totalmoney = money + totalmoney;
                                money = totalmoney;
                            }
                        }
                    }
                    if((money-(amount * itemsunit.FirstOrDefault().Price))% 2000!=0 || (money - (amount * itemsunit.FirstOrDefault().Price)) % 5000 != 0|| (money - (amount * itemsunit.FirstOrDefault().Price)) % 10000 != 0|| (money - (amount * itemsunit.FirstOrDefault().Price) % 20000) != 0|| (money - (amount * itemsunit.FirstOrDefault().Price)) % 50000 != 0)
                    {
                        Console.WriteLine("Your money rest convert to saldo."); 
                    }
                    else
                    {
                        Console.WriteLine("Please pick your items and your money rest:"+ (money - (amount * itemsunit.FirstOrDefault().Price)).ToString());
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            int Menu = 0;
            Console.WriteLine("Hello,Welcome!");
            Console.WriteLine("[1] Buying item");
            Console.WriteLine("[2] Topup Saldo");
            Console.WriteLine("[3] Checking Saldo");
            Console.Write("Please Select Your Option Above:");
            Menu = Convert.ToInt32(Console.ReadLine());
            switch (Menu.ToString())
            {
                case "1":
                    Buying();
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}
