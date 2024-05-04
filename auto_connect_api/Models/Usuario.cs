using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auto_connect_api.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public string cpf_cnpj { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public DateTime data_criacao { get; set; }

        public Usuario(string cpf_cnpj, string nome, string email, string senha, DateTime data_criacao)
        {
            this.cpf_cnpj = cpf_cnpj;
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.data_criacao = data_criacao;
        }
    }
}
