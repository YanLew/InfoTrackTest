namespace InfoTrackTest.Models.Histories
{
    public class HistoryDashboardDto
    {
        public string Keyword { get; set; }
        public string SearchEngineName { get; set; }
        public double AverageHighestRank { get; set; }
        public int HighestRank { get; set; }
        public int NotFoundCount { get; set; }
        public int TotalSearchCount { get; set; }
        public DateTime Date { get; set; }
    }
}
