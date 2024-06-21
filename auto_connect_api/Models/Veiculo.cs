using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("veiculo")]
    public class Veiculo
    {
        [Key]
        public int id_veiculo { get; set; }
        public string? ano { get; set; }
        public string? placa { get; set; }
        public double? odometro { get; set; }

        // Define explicitamente a chave estrangeira
        public string? usuario_cpf_cnpj { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("usuario_cpf_cnpj")]
        public Usuario? Usuario { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_marca { get; set; }

        [ForeignKey("id_marca")]
        public Marca? Marca { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_modelo { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_modelo")]
        public Modelo? Modelo { get; set; }
    }
}
