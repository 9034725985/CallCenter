using System.ComponentModel.DataAnnotations.Schema;

namespace CallCenter.Data.Model;
[Table("myperson")]
public class MyPerson
{
    [Column("id")]
    public int Id { get; set; }
    [Column("externalid")]
    public Guid ExternalId { get; set; }
    [Column("createdby")]
    public int CreatedBy { get; set; }
    [Column("createddate")]
    public DateTime CreatedDate { get; set; }
    [Column("modifiedby")]
    public int ModifiedBy { get; set; }
    [Column("modifieddate")]
    public DateTime ModifiedDate { get; set; }
    [Column("title")]
    public string Title { get; set; } = "";
    [Column("legalname")]
    public string LegalName { get; set; } = "";
    [Column("preferredname")]
    public string PreferredName { get; set; } = "";
    [Column("alias")]
    public string Alias { get; set; } = "";
}