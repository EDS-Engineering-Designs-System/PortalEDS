using bim360issues.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace bim360issues.Repositorios
{
    public class RepoCredencial : RepoAbstrato
    {
        BaseMDBRepositorio<Credencial> _repoCredencial;
        public RepoCredencial(string conectionString) : base(conectionString)
        {
            _repoCredencial = new BaseMDBRepositorio<Credencial>(new ConexaoMongoDb("auth_rico3d", conectionString), "Credencial");
        }

        public List<Credencial> ObterPorFORGE_CLIENT_ID(string forgeClienteId)
        {
            return _repoCredencial
                .Encontrar(Builders<Credencial>
                .Filter.Eq(x => x.FORGE_CLIENT_ID, forgeClienteId));
        }

        internal void Apagar(Credencial credencial)
        {
            _repoCredencial.Remover(credencial);
        }

        internal void Inserir(Credencial credencial)
        {
            _repoCredencial.Inserir(credencial);
        }
    }
}
