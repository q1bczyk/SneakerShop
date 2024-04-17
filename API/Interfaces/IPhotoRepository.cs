using API.Entities;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
        void Update(Photo photo);
        Task<Photo> AddPhotoAsync(Photo photo);
        Task<bool> SaveAllAsync();
    }
}