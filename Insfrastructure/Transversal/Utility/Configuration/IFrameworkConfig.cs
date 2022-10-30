namespace IFramework.Infrastructure.Utility.Configuration
{
    public class IFrameworkConfig
    {
        public string ProfileImageSource { get; set; }
        public string DefaultProfileImage { get; set; }
        public bool UseLogInfoBefore { get; set; }
        public bool UseLogInfoAfter { get; set; }
        public bool UseValidation { get; set; }
        public bool UseAuthentication { get; set; }
        public bool UseCache { get; set; }
        public bool UseException { get; set; }
        public bool UseHistory { get; set; }
        public string SqliteDbFileName { get; set; }
        public string RedisHost { get; set; }
        public string RedisPort { get; set; }
        public string RedisAuth { get; set; }
        public string RedisDbIndex { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string EmailQueueHost { get; set; }
        public string EmailQueueName { get; set; }
        public string FromEmail { get; set; }
        public string FromEmailName { get; set; }
        public string EmailSubject { get; set; }
        public string DbConnectionStringName { get; set; }
        public bool GenerateDatabase { get; set; }
        public int CacheDuration { get; set; }
        public Token Token { get; set; }
    }
    public class Token
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int TokenExpireSecond { get; set; }
    }
}
