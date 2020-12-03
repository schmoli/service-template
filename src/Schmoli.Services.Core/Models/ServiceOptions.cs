namespace Schmoli.Services.Core.Models
{
    public class ServiceOptions
    {

        public const string Service = "Service";
        public string Name { get; init; }
        public string Description { get; init; }
        public Maintainer Maintainer { get; init; }


    }

    public class Maintainer
    {
        public string Name { get; init; }
        public string EmailAddress { get; init; }
        public string Website { get; init; }
    }
}
