namespace caint.Models
{
    public class Tenant
    {
        public long id { get; set; }

        public string tenantName { get; set; }

        public bool active { get; set; }
    }
}