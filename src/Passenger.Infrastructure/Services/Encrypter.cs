using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private const int DeriveBytesIterationsCount = 10000;
        private const int SaltSize = 40;

        public string GetSalt(string value)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Can not generate salt from an empty value.", nameof(value));
            }

            var saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())     
                rng.GetBytes(saltBytes); 
                
            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.Empty())
            {
                throw new ArgumentException("Can not generate hash from an empty value.", nameof(value));
            }
            if (salt.Empty())
            {
                throw new ArgumentException("Can not use an empty salt from hashing value.", nameof(value));
            }

            using (var rfc = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount))
                return Convert.ToBase64String(rfc.GetBytes(SaltSize));     
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length*sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
