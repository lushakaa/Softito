using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}