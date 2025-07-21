using Microsoft.EntityFrameworkCore;
using YuvamHazir.Domain.Entities;

namespace YuvamHazir.Infrastructure.Context
{
    public class YuvamHazirDbContext : DbContext
    {
        public YuvamHazirDbContext(DbContextOptions<YuvamHazirDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<Product> Products { get; set; }//hilal burada controller eklerken tanımlı değil dedi diye ekledim bunu
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<AdoptionContract> AdoptionContracts { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<FosterRequest> FosterRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumComment> ForumComments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PatiPoint> PatiPoints { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Badge> Badges { get; set; }
        

        // Diğer DbSet'ler buraya eklenebilir

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Adoption: Owner ve NewOwner ilişkileri
            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Owner)
                .WithMany()
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.NewOwner)
                .WithMany()
                .HasForeignKey(a => a.NewOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Message: Sender ve Receiver ilişkileri
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserBadge: Composite Primary Key
            modelBuilder.Entity<UserBadge>()
                .HasKey(ub => new { ub.UserId, ub.BadgeId });

            // Order: User ve Address ilişkilerinde silme davranışını Restrict yap
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // BlogComment: User ilişkisi Restrict
            modelBuilder.Entity<BlogComment>()
                .HasOne(bc => bc.User)
                .WithMany()
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ForumComment: User ve Post ilişkilerinde silme davranışını Restrict yap
            modelBuilder.Entity<ForumComment>()
                .HasOne(fc => fc.User)
                .WithMany(u => u.ForumComments)
                .HasForeignKey(fc => fc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ForumComment>()
                .HasOne(fc => fc.Post)
                .WithMany(fp => fp.ForumComments)
                .HasForeignKey(fc => fc.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            // Answer: User ilişkisi Restrict
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // TÜM User ilişkileri Restrict
            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Owner)
                .WithMany()
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.NewOwner)
                .WithMany()
                .HasForeignKey(a => a.NewOwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ForumPost>()
                .HasOne(fp => fp.User)
                .WithMany(u => u.ForumPosts)
                .HasForeignKey(fp => fp.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BlogComment>()
                .HasOne(bc => bc.User)
                .WithMany(u => u.BlogComments)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ForumComment>()
                .HasOne(fc => fc.User)
                .WithMany(u => u.ForumComments)
                .HasForeignKey(fc => fc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ForumComment>()
                .HasOne(fc => fc.Post)
                .WithMany(fp => fp.ForumComments)
                .HasForeignKey(fc => fc.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PatiPoint>()
                .HasOne(pp => pp.User)
                .WithMany(u => u.PatiPoints)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FosterRequest>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FosterRequests)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Match>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserBadge>()
                .HasKey(ub => new { ub.UserId, ub.BadgeId });
            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Pets)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}
