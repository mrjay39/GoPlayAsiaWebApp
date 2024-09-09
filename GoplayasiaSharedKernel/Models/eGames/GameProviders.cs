namespace GoplayasiaSharedKernel.Models.eGames
{
    public class GameProviders
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int SortKey { get; set; }
        public int LiveCount { get; set; }
        public int SlotsCount { get; set; }
        public int FishingCount { get; set; }
        public int ArcadeCount { get; set; }
        public int CasinoCount { get; set; }
        public bool Selected { get; set; } = false;
    }
}
