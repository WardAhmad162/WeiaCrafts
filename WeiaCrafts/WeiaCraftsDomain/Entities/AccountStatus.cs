using System.ComponentModel.DataAnnotations;

namespace WeiaCraftsDomain.Entities;

public class AccountStatus
{
    [Key]
    public int Id { get; set; }
    public string StatusName { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}



