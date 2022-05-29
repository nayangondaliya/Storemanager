namespace Raditap.DataObjects.AppSettings
{
    public class DatabaseSettings
    {
        public string Connection { get; set; }
        public string ReadOnlyConnection { get; set; }
        public int TimeoutInSeconds { get; set; }
        public string EncryptionKey { get; set; }
        public string AesKey { get; set; }
        public string AesIv { get; set; }
    }
}
