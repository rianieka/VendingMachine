using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class CSV
    {
        public static Items CSVData(string CSV)
        {
            string[] values = CSV.Split(';');
            Items itemsdata = new Items();
            itemsdata.ID = Convert.ToInt32(values[0]);
            itemsdata.ItemName = values[1];
            itemsdata.Price = Convert.ToDouble(values[2]);
            itemsdata.Amount = Convert.ToInt32(values[3]);
            itemsdata.Reserved = Convert.ToInt32(values[4]);
            return itemsdata;
        }
    }
}
