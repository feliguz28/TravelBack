namespace Travel.Utilities
{
    public class ConfigConst
    {
        private static string? _bloggingDatabase;
        public static string StringConnection()
        {
            if (!string.IsNullOrEmpty(_bloggingDatabase))
            {
                return _bloggingDatabase;
            }
            var envVariable = "Data Source=.;Initial Catalog=travel;Integrated Security=true;TrustServerCertificate=True";
            if (!string.IsNullOrEmpty(envVariable))
            {
                _bloggingDatabase = envVariable;
                return envVariable;
            }
            return "";
        }
    }
}
