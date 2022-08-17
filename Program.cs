
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TradeCategory.Models;

namespace TradeCategory
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var inputCategories = new List<Trade>();

            int tradeNumber = 0;

            DateTime referenceDate;

            try
            {
                Console.WriteLine("Reference date: ");
                 referenceDate = Convert.ToDateTime(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter a valid date format (mm/dd/yyyy)");
                return;
            }


            try
            {
                Console.WriteLine("Number of trades: ");
                tradeNumber = Convert.ToInt16(Console.ReadLine());

                if (tradeNumber < 0)
                {
                    Console.WriteLine("please enter a number greater than 0");
                    return;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter with a valid number");
                return;
            }

            for (int i = 0; i < tradeNumber; i++)
            {
            newTrade:

                Console.WriteLine("Trade ('Amount' 'Sector'  'Pending Payment Date (mm/dd/yyyy)'): ");
                string trade = Console.ReadLine(); 

                try
                {
                    string[] newTrade = trade.Split(' ');

                    Trade newItem = new Trade();

                    newItem.Value = Convert.ToDouble(newTrade[0]);
                    newItem.ClientSector = newTrade[1];
                    newItem.NextPaymentDate = Convert.ToDateTime(newTrade[2]);
                    
                    if (!"Public;Private".Contains(newItem.ClientSector))
                    {
                        Console.WriteLine("For the client sector only Public and Private are accepted");
                        goto newTrade;
                    }

                    inputCategories.Add(newItem);    
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input");
                    goto newTrade;
                }
            }
         
            var classifiedCategories = TradeCategoryHelper.ListClassifiedCatgories(inputCategories, referenceDate);

            foreach (var category in classifiedCategories)
            {
                Console.WriteLine(category);
            }
        }
    }
}
