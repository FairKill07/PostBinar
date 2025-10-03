using Dapper;
using MediatR;
using PostBinar.Application.Abstractions.Interfaces;
using PostBinar.Application.Notes.Queries.GetNoteById;

public sealed class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto?>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetNoteByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<NoteDto?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
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
            FROM ""notes"" n
            INNER JOIN ""users"" u ON u.""Id"" = n.""AuthorId""
            WHERE n.""Id"" = @NoteId";


        var note = await connection.QuerySingleOrDefaultAsync<NoteDto>(sql, new { NoteId = request.NoteId.Value });

        return note;
    }
}
