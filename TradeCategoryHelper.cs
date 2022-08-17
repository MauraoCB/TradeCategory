using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeCategory.Models;

namespace TradeCategory
{
   public static class TradeCategoryHelper
    {
        public static List<string> ListClassifiedCatgories(List<Trade> trades, DateTime referenceDate)
        {

            List<string> unclassifiedCategories = new List<string>();
            List<string> classifiedCategories = new List<string>();

            foreach (var item in trades)
            {
                TimeSpan daysToNextPaymnet = referenceDate - item.NextPaymentDate;

                if (daysToNextPaymnet.TotalDays > 30)
                {
                    unclassifiedCategories.Add("0;EXPIRED");
                }

                if (item.Value > 1000000 && item.ClientSector == "Private")
                {
                    unclassifiedCategories.Add("1;HIGHRISK");
                }

                if (item.Value > 1000000 && item.ClientSector == "Public")
                {
                    unclassifiedCategories.Add("1;MEDIUMRISK");
                }
            }

            unclassifiedCategories.Sort();

            foreach (var item in unclassifiedCategories)
            {
                classifiedCategories.Add(item.Split(';')[1]);
            }
         
            return classifiedCategories;
        }
    }
}
