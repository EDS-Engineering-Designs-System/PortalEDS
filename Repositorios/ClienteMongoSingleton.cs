using MongoDB.Driver;

namespace bim360issues.Repositorios
{
    public class ClienteMongoSingleton
    {
        private static ClienteMongoSingleton _instance;

        public ClienteMongoSingleton(string pConnectionString)
        {
            ConnectionString = pConnectionString;
            SetClientConnection(pConnectionString);
        }

        public void SetClientConnection(string pConnectionString)
        {
            if (pConnectionString == "local")
                Client = new MongoClient("mongodb://localhost");
            else if (pConnectionString == "cosmosRico3d")
                Client = new MongoClient("mongodb://rico3d:yLqhkUyYaBcbAXjhviPepzmoinBxfV0Glx7f4JKNwarJGxOtJgTmgkNBecb3vUUxehrhSRraoShkzBmJnewYbw==@rico3d.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@rico3d@");
            else if (pConnectionString == "testeAtlas")
                Client = new MongoClient($"mongodb+srv://rico3d:umsamija45@cluster0.ceblc.mongodb.net/Forge?retryWrites=true&w=majority");
            
            else
                Client = new MongoClient(pConnectionString);
        }

        public static ClienteMongoSingleton GetInstance(string pConnectionString)
        {
            if (_instance == null)
                _instance = new ClienteMongoSingleton(pConnectionString);
            return _instance;
        }

        public static void Destroy()
        {
            _instance = null;
        }
        public IMongoClient Client { get; set; }
        public string ConnectionString { get; set; }
    }
}
