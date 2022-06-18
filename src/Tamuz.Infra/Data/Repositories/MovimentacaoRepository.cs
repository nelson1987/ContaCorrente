using Tamuz.Domain.Entities;
using Tamuz.Domain.Repositories;

namespace Tamuz.Infra.Data.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private static Dictionary<int, Movimentacao> pessoas = new Dictionary<int, Movimentacao>();
        public async Task Add(Movimentacao pessoa)
        {
            await Task.Run(() => pessoas.Add(pessoa.Id, pessoa));
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => pessoas.Remove(id));
        }

        public async Task Edit(Movimentacao pessoa)
        {
            await Task.Run(() =>
            {
                pessoas.Remove(pessoa.Id);
                pessoas.Add(pessoa.Id, pessoa);
            });
        }

        public async Task<Movimentacao> Get(int id)
        {
            return await Task.Run(() => pessoas.GetValueOrDefault(id));
        }

        public async Task<IEnumerable<Movimentacao>> GetAll()
        {
            return await Task.Run(() => pessoas.Values.ToList());
        }
    }
}
