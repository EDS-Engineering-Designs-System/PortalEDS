using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bim360issues.Models
{
    public abstract class Entidade
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; private set; }
    }
}
