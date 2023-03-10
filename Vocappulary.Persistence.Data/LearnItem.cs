using SQLite;

namespace Vocappulary.Persistence.Data;

[Table("LearnItem")]
public class LearnItem : IEntity
{
    [PrimaryKey, AutoIncrement, Column("_id")]
    public int Id { get; set; }

    [MaxLength(1024)]
    [NotNull]
    public string Phrase { get; set; }

    [MaxLength(1024)]
    [NotNull]
    public string Translation { get; set; }

}

