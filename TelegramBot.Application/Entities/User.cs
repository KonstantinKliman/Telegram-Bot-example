using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Application.Entities;

[Table("users", Schema = "public")]
public class User
{
    [Column("id")]
    [Key]
    public long Id { get; set; }

    [Column("chat_id")]
    [Required]
    public required long ChatId { get; set; }

    [Column("username")]
    public string? Name { get; set; }

    [Column("first_name")]
    [Required]
    public required string FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }
}