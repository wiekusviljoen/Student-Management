namespace Crud_Database.Models.Domain
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public long Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Department { get; set; }
    }
}
