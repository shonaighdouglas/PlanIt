namespace PlanIt.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public string? UserImage { get; set; }
    }
}
