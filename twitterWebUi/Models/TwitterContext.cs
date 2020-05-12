using Microsoft.EntityFrameworkCore;


namespace twitterWebUi.Models
{
    public class TwitterContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=TwitterDB;User Id=sa;Password=123;");
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DbSet<Retweet> Retweets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }

    }
}