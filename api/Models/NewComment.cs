using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models
{
    public class NewComment
    {
        [MaxLength(255)]
        public string Author { get; set; }

        public string Content { get; set; }

       [JsonConverter(typeof(JsonStringEnumConverter))]
        public ECommentState? State { get; set; }
        public Guid PostId { get; set; }
        
        
    }
}
