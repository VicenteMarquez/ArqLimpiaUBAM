using System.Security.Cryptography;
using System.Text;
using Application.Contracts;
using Konscious.Security.Cryptography;

namespace Application.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        var salt = GenerateSalt(16);

        using var argon = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon.Salt = salt;
        argon.DegreeOfParallelism = 4;
        argon.MemorySize = 131072;
        argon.Iterations = 4;

        var hash = argon.GetBytes(32);

        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(hashedPassword);

        var parts = hashedPassword.Split(':');
        if (parts.Length != 2) throw new FormatException("Invalid hash format.");

        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);

        using var argon = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon.Salt = salt;
        argon.DegreeOfParallelism = 4;
        argon.MemorySize = 131072;
        argon.Iterations = 4;

        var computedHash = argon.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }

    private static byte[] GenerateSalt(int length)
    {
        var salt = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
}