using auto_connect_api.Models;

namespace auto_connect_api.interfaces
{
    public interface IVeiculoRepository
    {
        void Add(Veiculo veiculo);

        List<Veiculo> Get(string? usuario_cpf_cnpj = null, string? where = null);

        Veiculo GetByIdVeiculo(int id_veiculo);

        bool DeleteVeiculoByIdVeiculo(int id_veiculo);
    }
}
