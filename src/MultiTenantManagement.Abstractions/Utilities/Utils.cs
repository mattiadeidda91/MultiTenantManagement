using System.Security.Cryptography;
using System.Text;

namespace MultiTenantManagement.Abstractions.Utilities
{
    public static class Utils
    {
        public static string GenerateRamdomString(int lenght, bool isOnlyAlphabetic = false)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var randomNumber = new byte[lenght];

            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);

            if (!isOnlyAlphabetic)
            {
                return Convert.ToBase64String(randomNumber);
            }
            else
            {
                var randomString = new StringBuilder();
                foreach (var num in randomNumber)
                {
                    randomString.Append(alphabet[num % alphabet.Length]);
                }

                return randomString.ToString();
            }
        }
    }
}
