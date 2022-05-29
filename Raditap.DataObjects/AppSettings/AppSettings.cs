namespace Raditap.DataObjects.AppSettings
{
    public class AppSettings
    {
        public string AesKey { get; set; }
        public string AesIv { get; set; }
        public string[] ValidCustomerTypes { get; set; }
        public int CustomerLoginSessionTimeoutInMinutes { get; set; }
        public string[] SkippedAuthenticationRoutes { get; set; }
        public string FileWritePath { get; set; }
        public string FileExtension { get; set; }
        public string FileNameFormat { get; set; }
    }
}
