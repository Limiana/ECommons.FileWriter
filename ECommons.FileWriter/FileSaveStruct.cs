using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommons.FileWriter;
[Serializable]
public class FileSaveStruct
{
    public string? Name { get; set; }
    public byte[]? Data { get; set; }
    public byte[]? NameHash { get; set; }
    public byte[]? DataHash { get; set; }

    public FileSaveStruct()
    {
    }

    public FileSaveStruct(string name, string data)
    {
        Name = name;
        NameHash = SHA1.HashData(Encoding.UTF8.GetBytes(name));
        var dataBytes = Encoding.UTF8.GetBytes(data);
        Data = dataBytes;
        DataHash = SHA1.HashData(dataBytes);
    }

    public FileSaveStruct(string name, byte[] dataBytes)
    {
        Name = name;
        NameHash = SHA1.HashData(Encoding.UTF8.GetBytes(name));
        Data = dataBytes;
        DataHash = SHA1.HashData(dataBytes);
    }

    public string Serialize() => JsonSerializer.Serialize(this);
}
