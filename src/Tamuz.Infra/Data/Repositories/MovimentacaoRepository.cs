using Tamuz.Domain.Movimentacao;
using Tamuz.Domain.Repositories;

namespace Tamuz.Infra.Data.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private static Dictionary<int, MovimentacaoModel> pessoas = new Dictionary<int, MovimentacaoModel>();
        public async Task Add(MovimentacaoModel pessoa)
        {
            await Task.Run(() => pessoas.Add(pessoa.Id, pessoa));
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => pessoas.Remove(id));
        }

        public async Task Edit(MovimentacaoModel pessoa)
        {
            await Task.Run(() =>
            {
                pessoas.Remove(pessoa.Id);
                pessoas.Add(pessoa.Id, pessoa);
            });
        }

        public async Task<MovimentacaoModel> Get(int id)
        {
            return await Task.Run(() => pessoas.GetValueOrDefault(id));
        }

        public async Task<IEnumerable<MovimentacaoModel>> GetAll()
        {
            return await Task.Run(() => pessoas.Values.ToList());
        }
    }
}
