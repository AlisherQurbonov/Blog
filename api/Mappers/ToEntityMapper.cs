using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace api.Mappers
{
    public static class ToEntityMapper
    {
         public static Entities.Post ToPostEntity(this Models.NewPost post, 
        IEnumerable<Entities.Media> media)
            => new Entities.Post()
            {
                Id = Guid.NewGuid(),
                HeaderImageId=post.HeaderImageId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Viewed = post.Viewed,
                CreatedAt=DateTimeOffset.UtcNow,
                ModifiedAt=post.CreatedAt,
                 Medias = media.ToList()

            };

          public static Entities.Comment ToCommentEntity(this Models.NewComment comment)
            => new Entities.Comment()
            {
                Id = Guid.NewGuid(),
                Author = comment.Author,
                Content = comment.Content,
                State = comment.State.ToEntityEComment(),
                PostId= comment.PostId
            };


        public static Entities.ECommentState ToEntityEComment(this Models.ECommentState? State)
        {
            return State switch
            {
                Models.ECommentState.Pending => Entities.ECommentState.Pending,
                Models.ECommentState.Approved => Entities.ECommentState.Approved,
                _ => Entities.ECommentState.Rejected,
            };
        }

     private static Entities.Media GetImageEntity(IFormFile file)
    {
        using var stream = new MemoryStream();

        file.CopyTo(stream);

        return new Entities.Media()
        {
            Id = Guid.NewGuid(),
            ContentType = file.ContentType,
            Data = stream.ToArray()
        };
    }

      

    
       
    }
}
