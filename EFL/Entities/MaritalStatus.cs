namespace EFL.Entities;

public partial class MaritalStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}
