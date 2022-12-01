using Autodesk.Forge;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Threading.Tasks;

namespace bim360issues.Models
{

    public class Credencial : Entidade
    {
        public string FORGE_CLIENT_ID { get; set; }
        public string TokenInternal { get; set; }
        public string TokenPublic { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
}
