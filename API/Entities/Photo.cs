namespace API.Entities
{
    public class Photo : BaseEntity
    {
        public string ImgUrl { get; set; }
        public bool ProfilePhoto { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}