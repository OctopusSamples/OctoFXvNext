namespace OctoFX.Core.Model
{
    public class Account : IId
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHashed { get; set; }
        public bool IsActive { get; set; }
    }
}