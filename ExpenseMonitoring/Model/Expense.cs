using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMonitoring.Model
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;//Expense, Earned
        public DateTime Date { get; set; }

        [ForeignKey("UserInfo")]
        public int UserInfoId { get; set; }

        [ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }

        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public virtual UserInfo UserInfo { get; set; } 

    }
}
