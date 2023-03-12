using InfoTrackTest.Models.Entities;
using InfoTrackTest.Models.SearchEngines;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackTest.Repositories.Context
{
    public class InfoTrackTestContext : DbContext
    {
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<SearchEngine> SearchEngine { get; set; }

        public InfoTrackTestContext(DbContextOptions<InfoTrackTestContext> options) : base(options)
        {
            // this.ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>(entity =>
            {
                entity.HasOne(d => d.SearchEngine)
                .WithMany(p => p.Histories)
                .HasForeignKey(d => d.SearchEngineId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SearchEngine>().HasData(
                new SearchEngine()
                {
                    Id = 1,
                    Name = "Google",
                    Url = $"https://www.google.co.uk/search?num={SearchEngineSeparatorConst.RESULT_NUMBER}&q={SearchEngineSeparatorConst.SEARCH_KEYWORDS}",
                    CreatedDateTime = DateTime.UtcNow,
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"
                },
                new SearchEngine()
                {
                    Id = 2,
                    Name = "Bing",
                    Url = $"https://www.bing.com/search?q={SearchEngineSeparatorConst.SEARCH_KEYWORDS}&count={SearchEngineSeparatorConst.RESULT_NUMBER}",
                    CreatedDateTime = DateTime.UtcNow,
                    UserAgent = "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm) Chrome/W.X.Y.Z Safari/537.36"
                },
                new SearchEngine()
                {
                    Id = 3,
                    Name = "Yahoo",
                    Url = $"https://uk.search.yahoo.com/search?p={SearchEngineSeparatorConst.SEARCH_KEYWORDS}&b={SearchEngineSeparatorConst.OFFSET}&pz={SearchEngineSeparatorConst.DEFAULT_OFFSET_SIZE}",
                    CreatedDateTime = DateTime.UtcNow,
                    DefaultPageSize = 7,
                    UserAgent = "Mozilla/5.0 (compatible; Yahoo! Slurp/3.0; http://help.yahoo.com/help/us/ysearch/slurp)"
                });
        }
    }
}
