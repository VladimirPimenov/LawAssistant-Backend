using System.Security.Cryptography;
using System.Text;

using LawAssistant.Application.Contracts;

namespace LawAssistant.Application.Services
{
    public class SHA256HashService : IHashService
    {
        public string Hash(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hashed = SHA256.HashData(bytes);

            return Convert.ToHexString(hashed);
        }

        public bool Verify(string str, string hashedStr)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hashed = SHA256.HashData(bytes);

            return hashed.SequenceEqual(Convert.FromHexString(hashedStr));
        }
    }
}
