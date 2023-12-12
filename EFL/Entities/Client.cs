namespace EFL.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int MaritalStatusId { get; set; }

    public long MobileNumber { get; set; }

    public string Email { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public virtual MaritalStatus MaritalStatus { get; set; } = null!;
}
