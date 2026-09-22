using Domain.Common;

namespace Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public int Balance { get; set; }
        public string UserId { get; set; }

        // Navigation property 
        public ICollection<Transaction> SentTransactions { get; set; }
        public ICollection<Transaction> ReceivedTransactions { get; set; }

        public Wallet()
        {
            SentTransactions = new HashSet<Transaction>();
            ReceivedTransactions = new HashSet<Transaction>();
        }
    }
}
