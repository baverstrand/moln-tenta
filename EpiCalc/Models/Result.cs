using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EpiCalc.Models
{
    public class Result
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Calculation { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

