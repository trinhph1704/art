namespace AWS.DTO.ArtworkDTO
{
    public class CreateArtwork
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<GerneArtworkDTO>? Genres { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrl2 { get; set; }
        public string Reason { get; set; }

    }
}
