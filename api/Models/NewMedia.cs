using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace api.Models
{
    public class NewMedia
    {  
    public IEnumerable<IFormFile> Data { get; set; }
        
    }
}