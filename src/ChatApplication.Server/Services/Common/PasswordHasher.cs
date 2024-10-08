namespace ChatApplication.Service.Services.Common
{
    public class PasswordHasher
    {
        public static string Hash(string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            return hash;
        }

        public static bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }


        private static string GenerateSalt()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
