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
            var envVariable = "Data Source=traveltest.mssql.somee.com;Initial Catalog=traveltest;persist security info=False; user id=feliguz28_SQLLogin_1;pwd=rd487aqr86; TrustServerCertificate=True";
            if (!string.IsNullOrEmpty(envVariable))
            {
                _bloggingDatabase = envVariable;
                return envVariable;
            }
            return "";
        }
    }
}
