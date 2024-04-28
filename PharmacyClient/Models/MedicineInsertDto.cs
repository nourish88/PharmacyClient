
using System.ComponentModel;

namespace PharmacyClient.Models;

    public class MedicineInsertDto : IDataErrorInfo
{
        public string  Name { get; set; }
        public decimal  Price { get; set; }
        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            result = "Name is required.";
                        break;
                    case "Price":
                        if (Price == default)
                            result = "Price is required.";
                        break;
                }
                return result;
            }
        }
}

