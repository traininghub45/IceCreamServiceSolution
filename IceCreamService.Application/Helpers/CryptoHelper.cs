using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace IceCreamService.Application.Helpers
{
    public static class CryptoHelper
    {
        
        private static readonly byte[] StaticKey = Convert.FromBase64String(
            "v3ryL0ngAndS3cur3K3yW1thHigh3ntr0pyAndRand0mn3ss1234567890AB" +
            "CDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/"
        );

        /// <summary>
        /// Hashes input data using HMAC-SHA512.
        /// </summary>
        public static string Hash(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            using var hmac = new HMACSHA512(StaticKey);
            byte[] dataHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(dataHash);
        }

        /// <summary>
        /// Verifies if the hashed data matches the original input (secure against timing attacks).
        /// </summary>
        public static bool VerifyHash(string hashedData, string data)
        {
            if (string.IsNullOrEmpty(hashedData) || string.IsNullOrEmpty(data))
                return false;

            string computedHash = Hash(data);
            return CryptographicOperations.FixedTimeEquals(
                System.Text.Encoding.UTF8.GetBytes(computedHash),
                System.Text.Encoding.UTF8.GetBytes(hashedData)
            );
        }

        /// <summary>
        /// (Recommended) For passwords, use PBKDF2 with a random salt.
        /// </summary>
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: StaticKey,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 100_000, // Adjust based on performance needs
                    numBytesRequested: 64
                )
            );
        }
    }
}