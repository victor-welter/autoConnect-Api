using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("local")]
    public class Local
    {
        [Key]
        public int id_local { get; set; }
        public string? nome { get; set; }
        public string? endereco { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_categoria { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_categoria")]
        public Categoria? categoria { get; set; }
    }
}
