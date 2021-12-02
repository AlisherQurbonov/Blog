using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class UpdateComment
    {
        public Guid Id { get; set; }
        
         [MaxLength(255)]
        public string Author { get; set; }

        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}