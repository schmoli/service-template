namespace Schmoli.Services.Core.Data.Postgres
{
    public class PostgresOptions
    {
        public const string Postgres = "Postgres";
        private const string ConnectionStringFormat = "Server={server};Port={port};Database={database};User Id={user};Password={password};{options}";
        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Options { get; set; }

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
