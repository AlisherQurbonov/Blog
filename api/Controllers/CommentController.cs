using System;
using System.Threading.Tasks;
using api.Mappers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _com;
        private readonly ICommentService _ser;

        public CommentController(ILogger<CommentController> com, ICommentService service)
        {
            _com = com;
            _ser = service;
        }

        [HttpPost]
        [ActionName(nameof(CommentAsync))]
        public async Task<IActionResult> CommentAsync(NewComment comment)
        {
            var result = await _ser.CreateAsync(comment.ToCommentEntity());

            if(result.IsSuccess)
            {
                 _com.LogInformation($"Comment create in DB: {comment.ToCommentEntity().Id}");
                return CreatedAtAction(nameof(CommentAsync), new {id = comment.ToCommentEntity().Id }, comment.ToCommentEntity());
            }

            return BadRequest(result.Exception.Message);
        }

       [HttpPut]
       [Route("{id}")]
       public async Task<ActionResult> UpdateAsync([FromRoute]Guid id, [FromBody]NewComment comment) 
       {

            var tocommentEntity = comment.ToCommentEntity();
            var result = await _ser.UpdateCommentAsync(tocommentEntity);
            
             if (result.IsSuccess)
            {
                _com.LogInformation($"Post update in DB: {comment}");
                return Ok(result);
            }

            return BadRequest(result.Exception.Message);
       }


        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid Id)
        {

            var post =  await _ser.DeleteAsync(Id);
              
            if (post.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(post.Exception.Message);
            
        }


    }
}