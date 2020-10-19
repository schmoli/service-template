namespace Schmoli.Services.Core.Models
{
    public class ServiceOptions
    {

        public const string Service = "Service";
        public string Name { get; set; }
        public string Description { get; set; }

        public Maintainer Maintainer { get; set; }


    }

    public class Maintainer
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }

        public string Website { get; set; }
    }
}
