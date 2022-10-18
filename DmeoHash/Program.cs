// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Hello, World!");

var hash = MD5.Create()
        .ComputeHash(Encoding.Default.GetBytes("bond007"));

Console.WriteLine(BitConverter.ToString(hash).Replace("-", ""));

var salt = RandomNumberGenerator.GetBytes(128 / 8);
var newHash = KeyDerivation.Pbkdf2("bond007", salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8);

Console.WriteLine(BitConverter.ToString(newHash).Replace("-", ""));
Console.ReadLine();



