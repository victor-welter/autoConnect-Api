using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("tipo_combustivel")]
    public class TipoCombustivel
    {
        [Key]
        public int id_tipo_combustivel { get; set; }
        public string? descricao { get; set; }
    }
}
