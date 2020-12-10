namespace caint.Models
{
    public class Comment
    {
        public long id { get; set; }

        public string name { get; set; }

        public string body { get; set; }

        public long threadId {  get; set;   }

    }
}