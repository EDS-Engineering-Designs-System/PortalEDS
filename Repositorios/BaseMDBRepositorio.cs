using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace bim360issues.Repositorios
{
    public class BaseMDBRepositorio<TColecao>
    {
        protected IMongoClient _client;
        protected IMongoDatabase _db;
        protected IMongoCollection<TColecao> _colecao;
        readonly string _nomeColecao;


        public BaseMDBRepositorio(ConexaoMongoDb conexaoMongo, string nomeColecao)
        {
            _db = conexaoMongo.Conectar();

            _nomeColecao = nomeColecao;

            _colecao = _db.GetCollection<TColecao>(nomeColecao);


        }

        public virtual TColecao Obter(string guid)
        {
            var filterGUID = Builders<TColecao>.Filter.Eq("GUID", guid);

            return _colecao.Find(filterGUID).FirstOrDefault();
        }

        public virtual List<TColecao> Obter()
        {
            return _colecao.AsQueryable<TColecao>().ToList();
        }

        public virtual void Inserir(IList<TColecao> lista)
        {
            _colecao.InsertMany(lista);
        }

        public virtual async Task BulkLoad(IList<WriteModel<TColecao>> lista)
        {
            await _colecao.BulkWriteAsync(lista);
        }

        public virtual void Inserir(TColecao modelo)
        {
            _colecao.InsertOne(modelo);
        }


        public virtual void Atualizar(TColecao modelo)
        {
            var filtroPorId = Builders<TColecao>.Filter.Eq("_id", modelo.ToBsonDocument().GetElement("_id").Value);
            _colecao.FindOneAndReplace(filtroPorId, modelo);
            //_colecao.UpdateOne(filtroPorId,modelo);//.ReplaceOne(filtroPorId, modelo);
        }

        public void ApagarVarios(FilterDefinition<TColecao> filtro)
        {
            _colecao.DeleteMany(filtro);
        }

        public virtual void Remover(TColecao modelo)
        {
            var filtroPorId = Builders<TColecao>.Filter.Eq("_id", modelo.ToBsonDocument().GetElement("_id").Value);
            _colecao.DeleteOne(filtroPorId);
        }



        public virtual List<TColecao> Encontrar(FilterDefinition<TColecao> filtro)
        {
            return _colecao.Find(filtro).ToList();
        }

        public bool PossuiUmRegistro(FilterDefinition<TColecao> filtro)
        {
            CountOptions countOptions = new CountOptions()
            {
                Limit = 1
            };
            var contagem = _colecao.CountDocuments(filtro, countOptions);

            return contagem > 0;

        }


        public virtual TColecao GetUmItemMax(FilterDefinition<TColecao> filtro, SortDefinition<TColecao> sortDefinition)
        {

            //var sort = Builders<TColecao>.Sort.Ascending("time");

            //var findOptions = new FindOptions<TColecao, TColecao>()
            //{
            //    Sort = sortDefinition
            //};

            return _colecao.Find(filtro).Sort(sortDefinition).Limit(1).Single();

            ///return default(TColecao);
        }

        public virtual List<TColecao> ObterListaDeArrayString(string campo, string[] array)
        {

            //IMongoCollection<BsonDocument> colecaoLocal = _db.GetCollection<BsonDocument>(_nomeColecao);
            var filter = Builders<TColecao>.Filter.AnyIn(campo, array); //"Source", new[] { "VG", "IGN" });
            var fluent = _colecao.Find(filter);
            //fluent.Options.Collation = new Collation("en");
            return fluent.Project<TColecao>("").ToList();

        }



        public long Contar(FilterDefinition<TColecao> filtro)
        {
            CountOptions countOptions = new CountOptions();

            return _colecao.CountDocuments(filtro, countOptions);
        }

        public virtual void SalvarJason(string json)
        {
            var collec = _db.GetCollection<BsonDocument>(_nomeColecao);
            var document = BsonSerializer.Deserialize<BsonDocument>(json);
            collec.InsertOneAsync(document);
        }

        public object ObterPorGuidCompartilhado(string gUID)
        {
            throw new NotImplementedException();
        }
    }
}
