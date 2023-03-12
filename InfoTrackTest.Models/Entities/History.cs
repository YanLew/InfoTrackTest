using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTrackTest.Models.Entities
{
    public class History : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int SearchEngineId { get; set; }
        public string Keyword { get; set; }
        public string Rank { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTime { get; set; }

        [ForeignKey(nameof(SearchEngineId))]
        [InverseProperty(nameof(InfoTrackTest.Models.Entities.SearchEngine.Histories))]
        public virtual SearchEngine SearchEngine { get; set; }

    }
}
