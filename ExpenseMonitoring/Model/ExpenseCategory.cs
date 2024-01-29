namespace ExpenseMonitoring.Model
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
