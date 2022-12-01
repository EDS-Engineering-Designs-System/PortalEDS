using MongoDB.Driver;

namespace bim360issues.Repositorios
{
    public class ConexaoMongoDb
    {
        readonly string _dbNome;
        readonly string _conectionString;

        public ConexaoMongoDb(string dbNome, string conectionString)
        {
            _dbNome = dbNome;
            _conectionString = conectionString;
        }

        public IMongoDatabase Conectar()
        {
            return ClienteMongoSingleton.GetInstance(_conectionString).Client.GetDatabase(_dbNome);
        }

    }
}
