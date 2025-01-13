namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        
        public Guid DifficultyId { get; set; }


        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
