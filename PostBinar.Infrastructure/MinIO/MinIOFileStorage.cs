using PostBinar.Domain.Enums;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Projects;
using Minio;
using Minio.DataModel.Args;
using Microsoft.Extensions.Options;
using PostBinar.Application.Abstractions.Interfaces.IFileStorage;

namespace PostBinar.Infrastructure.MinIO
{
    public sealed class MinIOFileStorage : IFileStorage
    {
        private readonly MinioOptions _options;
        private readonly IMinioClient _minio;
        private readonly IFileHelper _fileHelper;

        public MinIOFileStorage(IOptions<MinioOptions> options, IFileHelper fileHelper)
        {
            _options = options.Value;

            var builder = new MinioClient()
            .WithEndpoint(_options.Endpoint)
            .WithCredentials(_options.AccessKey, _options.SecretKey);

            if (_options.WithSSL)
                builder = builder.WithSSL();

            _minio = builder.Build();
            _fileHelper = fileHelper;
        }

        public async Task<bool> DeleteFileAsync(string storageKey, CancellationToken cancellationToken)
        {
            await _minio.RemoveObjectAsync(
                new RemoveObjectArgs()
                    .WithBucket(_options.Bucket)
                    .WithObject(storageKey), cancellationToken
            );

            return true;
        }

        public async Task EnsureBucketExistsAsync(CancellationToken cancellationToken)
        {
            if (!await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(_options.Bucket), cancellationToken))
            {
                await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(_options.Bucket), cancellationToken);
            }
        }

        public async Task<string> GetFileDownloadUrlAsync(string storageKey, CancellationToken cancellationToken)
        {
            return await _minio.PresignedGetObjectAsync(
            new PresignedGetObjectArgs()
                .WithBucket(_options.Bucket)
                .WithObject(storageKey)
                .WithExpiry(_options.DownloadExpirySeconds)
                );
        }

        public async Task<FileStorage> UploadFileAsync(
            ProjectId projectId,
            Guid objectId,
            string storageKey,
            Stream fileStream,
            StorageObjectType file,
            string fileName,
            string? mimeType,
            long size,
            CancellationToken cancellationToken)
        {
            ValidateUpload(projectId, size);

            await _minio.PutObjectAsync(
                BuildPutObjectArgs(storageKey, fileStream, size, mimeType),
                cancellationToken);

            return _fileHelper.CreateStoredFile(
                projectId,
                file,
                objectId,
                fileName,
                storageKey,
                size,
                mimeType);
        }

        // Helper methods
        private void ValidateUpload(ProjectId projectId, long size)
        {
            if (string.IsNullOrWhiteSpace(projectId.ToString()))
                throw new ArgumentException("userId required", nameof(projectId));
            if (size <= 0)
                throw new InvalidOperationException("Empty file.");
        }

        private PutObjectArgs BuildPutObjectArgs(string objectKey, Stream stream, long size, string? contentType)
        {
            return new PutObjectArgs()
                .WithBucket(_options.Bucket)
                .WithObject(objectKey)
                .WithStreamData(stream)
                .WithObjectSize(size)
                .WithContentType(contentType ?? "application/octet-stream");
        }
    }
}
