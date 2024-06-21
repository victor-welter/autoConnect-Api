using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("modelo")]
    public class Modelo
    {
        [Key]
        public int id_modelo { get; set; }
        public string? descricao { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_marca { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_marca")]
        public Marca? marca { get; set; }
    }
}
