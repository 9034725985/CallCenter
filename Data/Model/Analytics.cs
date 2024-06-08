using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Data.Model;

[Table("analytics")]
public class Analytics
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("datakey", TypeName = "text")]
    public string? DataKey { get; set; }

    [Column("datavalue", TypeName = "text")]
    public string? DataValue { get; set; }

    [Column("referer", TypeName = "text")]
    public string? Referer { get; set; }

    [Column("ipaddress", TypeName = "text")]
    public string? IpAddress { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
