using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("notificacao")]
    public class Notificacao
    {
        [Key]
        public int id_notificacao { get; set; }
        public string? titulo { get; set; }
        public string? descricao { get; set; }
        public int? visualizada { get; set; }
        public DateTime data_notificacao { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_despesa { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_despesa")]
        public Despesa? despesa { get; set; }
    }
}
