using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ModelPrincipal.Utilitarios
{
    public interface IDocument
    {
        [BsonId]
        Guid Id { get; set; }
    }
}
