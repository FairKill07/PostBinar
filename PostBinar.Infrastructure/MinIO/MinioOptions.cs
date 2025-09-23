namespace PostBinar.Infrastructure.MinIO;

public sealed class MinioOptions
{
    public required string Endpoint { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string Bucket { get; set; }
    public bool WithSSL { get; set; }
    public int DownloadExpirySeconds { get; set; }
}
