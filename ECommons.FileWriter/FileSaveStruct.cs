using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommons.FileWriter;
[Serializable]
public class FileSaveStruct
{
    public string? Name { get; set; }
    public byte[]? Data { get; set; }
    public byte[]? NameHash { get; set; }
    public byte[]? DataHash { get; set; }
}
