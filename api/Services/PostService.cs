using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Services
{
    public class PostService : IPostService
    {
        private readonly ApiContext _ctx;
        private readonly ILogger<PostService> _log;

        public PostService(ILogger<PostService> logger, ApiContext context)
        {
            _ctx = context;
            _log = logger;
        }
        public async Task<(bool IsSuccess, Exception Exception, Post Post)> CreateAsync(Post post)
        {
         try
        {
            await _ctx.Posts.AddAsync(post);
            await _ctx.SaveChangesAsync();

            _log.LogInformation($"Post create in DB: {post}");

            return (true, null, post);
        }
        catch(Exception e)
        {
             _log.LogInformation($"Create post to DB failed: {e.Message}", e);
            return (false, e, null);
        }

        }

        public async Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Post post, Guid Id)
        {
            try
            {

            try
            {

                var medias = post.Medias.ToList();
                    
                    foreach(var media in medias)
                    {
                        _ctx.Medias.Remove(media);
                        await _ctx.SaveChangesAsync();
                    }

                 var comments = post.Comments.ToList();
                    
                    foreach(var comment in comments)
                    {
                        _ctx.Comments.Remove(comment);
                        await _ctx.SaveChangesAsync();
                    }
        
            _ctx.Posts.Remove(await GetAsync(Id));
            await _ctx.SaveChangesAsync();
            _log.LogInformation($"Post remove in DB: {Id}");
           
            }
            catch
            {
            _ctx.Posts.Remove(await GetAsync(Id));
            await _ctx.SaveChangesAsync();
            }
            return (true, null);
            }

              catch(Exception e)
            {
                return (false, e);
            }
        }

        public Task<bool> ExistsAsync(Guid id)
          => _ctx.Posts
        .AnyAsync(p => p.Id == id);

        public Task<List<Post>> GetAllAsync()
        => _ctx.Posts
            .AsNoTracking()
            .Include(m => m.Comments)
            .Include(m => m.Medias)
            .ToListAsync();
         public Task<List<Post>> GetIdAsync(Guid id)
        => _ctx.Posts
            .AsNoTracking()
            .Where(i => i.Id == id)
             .Include(m => m.Comments)
            .Include(m => m.Medias)
            .ToListAsync();
        public Task <Post> GetAsync(Guid id)
        => _ctx.Posts.FirstOrDefaultAsync(a => a.Id == id);
        

        public async Task<(bool IsSuccess, Exception Exception,Post Post)> UpdatePostAsync(Post post)
        {
            try
            {
                if(await _ctx.Posts.AnyAsync(t => t.Id == post.Id))
                {
                    _ctx.Posts.Update(post);
                    await _ctx.SaveChangesAsync();

                    return (true, null,post);
                }
                else
                {
                    return (false, new Exception($"Post with given ID: {post.Id} doesnt exist!"),null);
                }
            }
            catch(Exception e)
            {
                return (false, e,null);
            }
        }
    }
}