using Domain.Common;

namespace Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid SenderWalletId { get; set; }
        public Guid ReceiverWalletId { get; set; }
        public decimal Amount { get; set; }
        public bool IsSuccesful { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public Wallet SenderWallet { get; set; }
        public Wallet ReceiverWallet { get; set; }

    }
}
