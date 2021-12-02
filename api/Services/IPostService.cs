using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;

namespace api.Services
{
    public interface IPostService
    {
    Task<(bool IsSuccess, Exception Exception, Post Post)> CreateAsync(Post post);
    Task<List<Post>> GetAllAsync();

    Task<List<Post>> GetIdAsync(Guid id);
    Task<Post> GetAsync(Guid id);
    Task<(bool IsSuccess, Exception Exception, Post Post)> UpdatePostAsync(Post post);
    Task<bool> ExistsAsync(Guid id);
    Task<(bool IsSuccess, Exception Exception)> DeleteAsync(Post post,Guid id);
   
    }
}