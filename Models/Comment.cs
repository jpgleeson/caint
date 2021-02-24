namespace caint.Models
{
    public class Comment
    {
        public long id { get; set; }

        public string name { get; set; }

        public string body { get; set; }

        public long threadId {  get; set;   }

        public bool approved {  get; set;   }

        public string ownerId { get; set; }

    }

    public class CommentDTO
    {
        public long id { get; set; }

        public string name { get; set; }

        public string body { get; set; }

        public long threadId {  get; set;   }
    }
}