using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;

        public PhotoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Photo> AddPhotoAsync(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
            await SaveAllAsync();
            return photo;
        }

        public async Task<bool> DeletePhotoAsync(Photo photo)
        {
            _context.Remove(photo);
            await SaveAllAsync();
            return true;
        }

        public async Task<Photo> GetPhotoByIdAsync(string id)
        {
            return await _context.Photos.FirstOrDefaultAsync(
                x => x.Id == id);
        }

        public async Task<Photo> GetProfilePhotoAsync(string productId)
        {
            return await _context.Photos.FirstOrDefaultAsync(
                x => x.ProductId == productId && 
                x.ProfilePhoto == true);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Photo photo)
        {
            _context.Entry(photo).State = EntityState.Modified;
        }
    }
}