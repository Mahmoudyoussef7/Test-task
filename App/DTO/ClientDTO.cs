using EFL.Entities;

namespace App.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int MaritalStatusId { get; set; }

        public long MobileNumber { get; set; }

        public string Email { get; set; }

        public string ImagePath { get; set; }

        public string MaritalStatus { get; set; }
    }
}
