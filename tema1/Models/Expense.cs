using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tema1.Models
{
    public enum TypeExpenses
    {
        food,
        utilities,
        transportation,
        outing,
        groceries,
        clothes,
        electronics,
        other
    }


    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Sum { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }

        [EnumDataType(typeof(TypeExpenses))]
        public TypeExpenses Type { get; set; }
    }
}
