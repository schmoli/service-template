namespace Schmoli.ServiceTemplate.Models
{
    public class PrimaryItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long SecondaryItemId { get; set; }

        public SecondaryItem SecondaryItem { get; set; }
    }
}
