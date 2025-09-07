using Microsoft.EntityFrameworkCore;
using PostBinar.Domain.Categorys;
using PostBinar.Domain.Comments;
using PostBinar.Domain.FileStorages;
using PostBinar.Domain.Notes;
using PostBinar.Domain.ProjectMemberships;
using PostBinar.Domain.Projects;
using PostBinar.Domain.TaskItems;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces
{
    public interface IPostBinarDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Project> Projects { get; }
        DbSet<TaskItem> TaskItems { get; }
        DbSet<ProjectMembership> ProjectMemberships { get; }
        DbSet<Note> Notes { get; }
        DbSet<FileStorage> FileStorages { get; }
        DbSet<Comment> Comments { get; }
        DbSet<NoteCategory> NoteCategories { get; }
        DbSet<TaskCategory> TaskCategories { get; } 
        DbSet<Specialization> Specializations { get; }
    }
}
