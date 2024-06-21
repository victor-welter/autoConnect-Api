using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("categoria")]
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }
        public string? descricao { get; set; }
    }
}
