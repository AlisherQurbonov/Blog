using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Mappers;
using api.Models;
using api.Services;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _log;

        private readonly IPostService _pc;

        private readonly ICommentService _cs;

        private readonly IMediaService _mc;

        public PostController(
            ILogger<PostController> log,
            IPostService pc,
            ICommentService cs,
            IMediaService mc
        )
        {
            _log = log;
            _pc = pc;
            _cs = cs;
            _mc = mc;
        }

        [HttpPost]
        [ActionName(nameof(PostAsync))]
        public async Task<IActionResult> PostAsync(NewPost post)
        {
            var media = post.Medias.Select(id => _mc.GetAsync(id).Result);
            var result = await _pc.CreateAsync(post.ToPostEntity(media));

           if(result.IsSuccess)
            {
                 _log.LogInformation($"Pizza create in DB: {post.ToPostEntity(media).Id}");
                return CreatedAtAction(nameof(PostAsync), new {id = post.ToPostEntity(media).Id }, post.ToPostEntity(media));
            }

            return BadRequest(result.Exception.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedia()
        {

            var images = await _pc.GetAllAsync();

            return Ok(images
                .Select(i =>
                {
                    return new {
                        Id = i.Id,
                        Title = i.Title,
                        Description=i.Description,
                        Content=i.Content,
                        Viewed=i.Viewed,
                        CreatedAt=i.CreatedAt,
                        ModifiedAt=i.ModifiedAt,
                        Comments=i.Comments

                    };
              }));
        }

    //  [HttpGet]
    //   public async Task<IActionResult> GetAsync()
    // {
    //     JsonSerializerOptions options = new()
    //     {
    //         ReferenceHandler = ReferenceHandler.Preserve,
    //         WriteIndented = true
    //     };

    //     var json = JsonSerializer.Serialize(await _pc.GetAllAsync(), options);
    //     return Ok(json);
    // }


        // [HttpGet]
        // [Route("{Id}")]
        // public async Task<IActionResult> GetPostAsync([FromRoute]Guid Id)
        // {
        //     var post = await _pc.GetAsync(Id);
               

        //     if(post is default(Entities.Post))
        //     {
        //         return NotFound($"User with ID {Id} not found");
        //     }

        //     return Ok(post);
              
        // }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetIdMedia(Guid Id)
        {
      
           if(!await _pc.ExistsAsync(Id))
        {
            return NotFound();
        }

            var images = await _pc.GetIdAsync(Id);

            return Ok(images
                .Select(i =>
                {
                    return new {
                        Id = i.Id,
                        Title = i.Title,
                        Description=i.Description,
                        Content=i.Content,
                        Viewed=i.Viewed,
                        CreatedAt=i.CreatedAt,
                        ModifiedAt=i.ModifiedAt,
                        Comments=i.Comments

                    };
              }));
        }

       [HttpPut]
       [Route("{id}")]
       public async Task<ActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]NewPost post) 
       {

            var media = post.Medias.Select(id => _mc.GetAsync(id).Result);
            var topostEntity = post.ToPostEntity(media);
            var result = await _pc.UpdatePostAsync(topostEntity);
            
             if (result.IsSuccess)
            {
                _log.LogInformation($"Post update in DB: {media}");
                return Ok();
            }

            return BadRequest(result.Exception.Message);
       }


        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid Id)
        {

            var post =  await _pc.DeleteAsync(Id);
              
            if (post.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(post.Exception.Message);
            
        }
    }
}
