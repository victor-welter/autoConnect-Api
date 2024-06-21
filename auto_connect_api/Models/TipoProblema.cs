using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("tipos_problema")]
    public class TipoProblema
    {
        [Key]
        public int id_tipo_problema { get; set; }
        public string? descricao { get; set; }
    }
}
