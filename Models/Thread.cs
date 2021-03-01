namespace caint.Models
{
    public class Thread
    {
        public long id { get; set; }

        public string hostname { get; set; }

        public string path { get; set; }

        public string ownerId { get; set; }

        public bool noApproval { get; set; }
    }

    public class ThreadDTO
    {
        public long id { get; set; }
    }

    public class NewThreadDTO
    {
        public string hostname {get; set;}
        public string path {get; set;}
    }
}