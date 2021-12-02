using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Entities;

namespace api.Models
{
    public class PostUpdated
    {
        public Guid Id { get; set; }

        public Guid HeaderImageId { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public string Content { get; set; }

        public IEnumerable<Guid> Medias { get; set; }
    }
}
