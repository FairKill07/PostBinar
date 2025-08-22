using CSharpFunctionalExtensions;

namespace PostBinar.Domain.Users
{
    public sealed class User : Abstraction.Entity<UserId>
    {
        private User(
            UserId id,
            string firstName,
            string lastName,
            string email,
            string passwordHash)
            : base(id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PasswordHash = passwordHash;
        }
        protected User() { } //EF core

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Photo? ProfilePhoto { get; private set; }
        public string? TelegramChatId { get; private set; }
        public string FullName => $"{FirstName} {LastName}";

        public static Result<User> Create(
            string firstName,
            string lastName,
            string email,
            string passwordHash)
        {
            var validationResult = ValidateParameters(firstName, lastName, email, passwordHash);

            if (validationResult.IsFailure)
            {
                return Result.Failure<User>(validationResult.Error);
            }

            var user = new User(
                UserId.New(),
                firstName,
                lastName,
                email,
                passwordHash);

            return Result.Success(user);
        }

        private static Result ValidateParameters(
                string firstName,
                string lastName,
                string email,
                string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure("First name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure("Last name is required");

            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure("Email is required");

            if (!email.Contains("@"))
                return Result.Failure("Invalid email format");

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure("Password hash is required");

            return Result.Success();
        }

        public void SetProfilePhoto(Photo photo)
        {
            ArgumentNullException.ThrowIfNull(photo);
            ArgumentNullException.ThrowIfNull(photo.Uri);

            this.ProfilePhoto = photo;
        }

        public void SetTelegramChatId(string chatId)
        {
            ArgumentNullException.ThrowIfNull(chatId);

            this.TelegramChatId = chatId;
        }
    }
}
