using System;
using System.Collections.Generic;
using System.Linq;

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


              public static Entities.Post ToUpdatePostEntity(this Models.PostUpdated post, IEnumerable<Entities.Media> media)
            => new Entities.Post()
            {
                Id = post.Id,
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
       
    }
}
