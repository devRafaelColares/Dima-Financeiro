namespace Dima.Core.Requests.Transactions
{
    public class GetTransactionsByPeriodRequest : PagedRequest
    {
        public DateTime? StarteDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}