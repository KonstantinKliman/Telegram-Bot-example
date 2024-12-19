using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot.Core.Entities;

[Table("users", Schema = "public")]
public class User
{
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("chat_id")]
    [Required]
    public required long ChatId { get; set; }

    [Column("username")]
    [Required]
    public required string Name { get; set; }

    [Column("first_name")]
    [Required]
    public required string FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }
}
