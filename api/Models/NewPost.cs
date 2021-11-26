using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace api.Models
{
    public class NewPost
    {
    public Guid HeaderImageId  { get; set; }

    [MaxLength(255)]
    [Required]
    public string Title { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }
    public string Content { get; set; }
    public uint Viewed { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public ICollection<NewComment> Comments { get; set; }
    public IEnumerable<IFormFile> Files { get; set; }
    }
}