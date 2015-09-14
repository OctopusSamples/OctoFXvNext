namespace OctoFX.Core.Model
{
    public class BeneficiaryAccount : IId
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public string Nickname { get; set; }
        public string AccountNumber { get; set; }
        public string SwiftBicBsb { get; set; }
        public virtual Currency Currency { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}