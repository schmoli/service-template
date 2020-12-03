namespace Schmoli.Services.Core.Data.Postgres
{
    public class PostgresOptions
    {
        public const string Postgres = "Postgres";
        private const string ConnectionStringFormat = "Server={server};Port={port};Database={database};User Id={user};Password={password};{options}";
        public string Server { get; init; }
        public int Port { get; init; }
        public string User { get; init; }
        public string Password { get; init; }
        public string Database { get; init; }
        public string Options { get; init; }

        /// <summary>
        /// Build a formatted Connection String
        /// </summary>
        /// <returns>all properties in class converted into an EFCore compatible connection string.</returns>
        public string ConnectionString => ConnectionStringFormat
                    .Replace("{server}", Server)
                    .Replace("{port}", Port.ToString())
                    .Replace("{database}", Database)
                    .Replace("{user}", User)
                    .Replace("{password}", Password)
                    .Replace("{options}", Options);
    }
}
