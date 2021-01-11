using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Figure.Contracts.Db
{
    public class FigureRecord
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public FigureType Type { get; set; }
        public string Params { get; set; }
    }
}
