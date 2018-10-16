using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DziaroSopas.Models
{
    public class AdminModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }



    }
}