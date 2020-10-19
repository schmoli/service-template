namespace Schmoli.ServiceTemplate.Resources
{
    public class PrimaryItemResource
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SecondaryItemResource SecondaryItem { get; set; }
    }
}
