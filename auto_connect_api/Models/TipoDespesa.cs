using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("tipos_despesa")]
    public class TipoDespesa
    {
        [Key]
        public int id_tipo_despesa { get; set; }
        public string? descricao { get; set; }
    }
}
