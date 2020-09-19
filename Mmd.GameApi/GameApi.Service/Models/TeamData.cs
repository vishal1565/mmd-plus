namespace GameApi.Service.Models
{
    public class TeamData
    {
        public string TeamId { get; set; }
        public long CurrentScore { get; set; }
        public long TotalScore { get; set; }
        public bool IsAlive { get; set; }
    }
}