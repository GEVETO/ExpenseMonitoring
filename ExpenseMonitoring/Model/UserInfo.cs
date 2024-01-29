namespace ExpenseMonitoring.Model
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Expense> Expenses{ get; set; }

    }
}
