using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctoFX.Core.Model
{
    public class Quote : IId
    {
        protected Quote()
        {
        }

        public int Id { get; set; }
        [NotMapped]
        public CurrencyPair SellBuyCurrencyPair 
        { 
            get 
            {
                return (CurrencyPair)SellBuyCurrencyPairStringValue;
            }
            set
            {
                SellBuyCurrencyPairStringValue = value;
            } 
        }
        [Column("SellBuyCurrencyPair")]
        public string SellBuyCurrencyPairStringValue {get;set;}
        public decimal Rate { get; protected set; }
        public decimal SellAmount { get; protected set; }
        public decimal BuyAmount { get; protected set; }
        public DateTimeOffset QuotedDate { get; protected set; }
        public DateTimeOffset ExpiryDate { get; protected set; }

        public virtual bool HasExpired(DateTimeOffset now)
        {
            return now > ExpiryDate;
        }

        public static Quote Create(ExchangeRate rate, decimal sellQuantity, DateTimeOffset now)
        {
            return new Quote
            {
                SellBuyCurrencyPair = rate.SellBuyCurrencyPair,
                BuyAmount = rate.QuoteWhenIntendingToSell(sellQuantity),
                ExpiryDate = now.AddMinutes(5),
                QuotedDate = now,
                Rate = rate.Rate,
                SellAmount = sellQuantity
            };
        }
    }
}