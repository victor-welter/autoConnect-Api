using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public string cpf_cnpj { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public DateTime data_criacao { get; set; }

        // Propriedade de navegação
        public virtual ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}
