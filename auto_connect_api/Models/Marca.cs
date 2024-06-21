using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("marca")]
    public class Marca
    {
        [Key]
        public int id_marca { get; set; }
        public string? descricao { get; set; }
    }
}
