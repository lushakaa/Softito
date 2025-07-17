using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public int TotalPatiPoints { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<PatiPoint> PatiPoints { get; set; }
        public ICollection<UserBadge> UserBadges { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Adoption> Adoptions { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        public ICollection<FosterRequest> FosterRequests { get; set; }
        public ICollection<ForumPost> ForumPosts { get; set; }
        public ICollection<ForumComment> ForumComments { get; set; }
    }
}
