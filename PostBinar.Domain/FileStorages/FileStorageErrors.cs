using PostBinar.Domain.Abstraction;

namespace PostBinar.Domain.FileStorages;

public static class FileStorageErrors
{
    public static readonly NotFoundError NotFound =
        new("FileStorage.NotFound", "The file with the specified identifier was not found");

    public static readonly Error InvalidProjectId =
        new("FileStorage.InvalidProjectId", "Project ID is required");

    public static readonly Error InvalidObjectId =
        new("FileStorage.InvalidObjectId", "Object ID is required");

    public static readonly Error InvalidFileName =
        new("FileStorage.InvalidFileName", "File name is required");

    public static readonly Error InvalidStorageKey =
        new("FileStorage.InvalidStorageKey", "Storage key is required");

    public static readonly Error InvalidMimeType =
        new("FileStorage.InvalidMimeType", "Mime type is required");

    public static readonly Error InvalidFileSize =
        new("FileStorage.InvalidFileSize", "File size cannot be negative");

    public static readonly Error FailedCreate =
        new("FileStorage.FailedCreate", "Failed to create file storage entry");

    public static readonly Error FailedUpdate =
        new("FileStorage.FailedUpdate", "Failed to update file storage information");

    public static readonly Error AlreadyInactive =
        new("FileStorage.AlreadyInactive", "File is already inactive");

    public static readonly Error AlreadyActive =
        new("FileStorage.AlreadyActive", "File is already active");
}

