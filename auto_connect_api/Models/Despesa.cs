using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("despesa")]
    public class Despesa
    {
        [Key]
        public int id_despesa { get; set; }
        public DateTime data { get; set; }
        public double odometro { get; set; }
        public double preco_unitario { get; set; }
        public double quantidade { get; set; }
        public double preco_total { get; set; }
        public int manutencao_preventiva { get; set; }
        public string? descricao { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_veiculo { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_veiculo")]
        public Veiculo? veiculo { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_local { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_local")]
        public Local? local { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_tipo_despesa { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_tipo_despesa")]
        public TipoDespesa? tipo_despesa { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_tipo_combustivel { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_tipo_combustivel")]
        public TipoCombustivel? tipo_combustivel { get; set; }

        // Define explicitamente a chave estrangeira
        public int? id_tipo_problema { get; set; }

        // Propriedade de navegação para a entidade relacionada
        [ForeignKey("id_tipo_problema")]
        public TipoProblema? tipo_problema { get; set; }
    }
}
