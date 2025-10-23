using Dapper;
using MediatR;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Notes.Queries.GetNoteById;
using PostBinar.Domain.Abstraction;
using System.Data;

namespace PostBinar.Application.Notes.Queries.GetNoteById;

public sealed class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, Result<NoteDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetNoteByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<NoteDto>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT 
                n.""Title"",
                n.""Content"",
                n.""CategoryId"",
                n.""CreatedAt"",
                n.""UpdatedAt"",
                u.""FirstName"",
                u.""LastName""
            FROM ""notes"" AS n
            INNER JOIN ""users"" AS u ON u.""Id"" = n.""AuthorId""
            WHERE n.""Id"" = @NoteId
              AND n.""IsActive"" = TRUE;";

        var note = await connection.QuerySingleOrDefaultAsync<NoteDto>(
            new CommandDefinition(sql, new { NoteId = request.NoteId.Value }, cancellationToken: cancellationToken));

        if (note is null)
            return Result.Failure<NoteDto>(Error.NoData);

        return Result.Success(note);
    }
}
