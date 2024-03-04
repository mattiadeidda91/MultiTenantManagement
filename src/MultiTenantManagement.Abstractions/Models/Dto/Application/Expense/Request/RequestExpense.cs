namespace MultiTenantManagement.Abstractions.Models.Dto.Application.Expense.Request
{
    public class RequestExpense
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Description { get; set; }
        public string Recipient { get; set; } = null!;
        public double Price { get; set; }
        public string? ActivityName { get; set; }

        public Guid SiteId { get; set; }
    }
}
