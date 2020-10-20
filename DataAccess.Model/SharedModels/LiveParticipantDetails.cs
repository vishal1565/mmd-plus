namespace DataAccess.Model.SharedModels
{
    public class LiveParticipantDetails
    {
        public string TeamId { get; set; }
        public long CurrentScore { get; set; }
        public long TotalScore { get; set; }
        public bool? IsAlive { get; set; }
        public bool IsRobot { get; set; }
        public string Location { get; set; }
        public int Lifelines { get; set; }

    }
}
