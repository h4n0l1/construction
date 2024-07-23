using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
  [Table("users")]
  public class User
  {
    [Column("id")]
    public int Id { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("salt")]
    public string Salt { get; set; }

    [Column("email")]
    public string Email { get; set; }
  }
}