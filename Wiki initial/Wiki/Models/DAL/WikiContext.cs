using System.Data.Entity;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL {
    public class WikiContext : DbContext {

        public DbSet<Wiki.Models.Biz.Utilisateur> Utilisateurs {get; set;}
        public DbSet<Wiki.Models.Biz.Article> Articles { get; set; }
        public WikiContext() : base("name=WIKI") {; } // The name of the connection string (which you'll add to the Web.config file later) is passed in to the constructor.

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Langue)
                .IsFixedLength();

            modelBuilder.Entity<Utilisateur>()
                .HasMany(e => e.lstArticle)
                .WithRequired(e => e.utilisateur)
                .HasForeignKey(e => e.IdContributeur)
                .WillCascadeOnDelete(false);
        }
    }
}