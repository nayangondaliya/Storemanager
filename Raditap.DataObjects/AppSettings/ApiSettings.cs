namespace Raditap.DataObjects.AppSettings
{
    public class ApiSettings
    {
        public bool EnableSwagger { get; set; }
        public string[] IgnoreEncryptionApiPaths { get; set; }
        public string[] NoRequestEncryptionApiPaths { get; set; }
        public string[] NotExtendLoginSessionApiPaths { get; set; }
        public string[] LogFilterApiPaths { get; set; }
        public string[] NotLogMessageHttpClientNames { get; set; }
        public string[] EncryptAesFields { get; set; }
        public string[] DecryptAesFields { get; set; }
        public string[] IgnoreTokenApiPaths { get; set; }
        public string[] IgnoreHandshakeApiPaths { get; set; }
        public string[] IgnoreProcessingTimeApiPaths { get; set; }
    }
}
