using API.Entities;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
        void Update(Photo photo);
        Task<Photo> AddPhotoAsync(Photo photo);
        Task<bool> DeletePhotoAsync(Photo photo);
        Task<Photo> GetPhotoByIdAsync(string id);
        Task<bool> SaveAllAsync();
    }
}