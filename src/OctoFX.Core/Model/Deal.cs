using System;

namespace OctoFX.Core.Model
{
    public class Deal : IId
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public BeneficiaryAccount NominatedBeneficiaryAccount { get; set; }
        public Currency BuyCurrency { get; set; }
        public Currency SellCurrency { get; set; }
        public decimal BuyAmount { get; set; }
        public decimal SellAmount { get; set; }
        public DealStatus Status { get; set; }
        public DateTimeOffset EnteredDate { get; set; }
    }
}