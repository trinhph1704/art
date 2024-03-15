namespace AWS.DTO.Premium
{
    public class PremiumArtworkDTO
    {
        public string? PremiumID { get; set; }
        public string UserID { get; internal set; }
        public DateTime CreateDate { get; set; }
        public string price { get; set; }
        public DateTime Day_expire { get; set; }

    }
}
