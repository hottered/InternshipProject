namespace WebApiSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public int PositionId { get; set; }
        public string IdNumber { get; set; }
        public DateTime Joined { get; set; }
    }
}
