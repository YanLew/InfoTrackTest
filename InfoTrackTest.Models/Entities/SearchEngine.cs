using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoTrackTest.Models.Entities
{
    public class SearchEngine : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string UserAgent { get; set; }
        public int? DefaultPageSize { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTime { get; set; }

        [InverseProperty("SearchEngine")]
        public virtual ICollection<History> Histories { get; set; }

    }
}
