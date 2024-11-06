using ECommons.FileWriter;
using System.IO.Pipes;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        if(args.Length > 0)
        {
            using PipeStream pipeClient = new AnonymousPipeClientStream(PipeDirection.In, args[0]);
            Console.WriteLine($"[FileWriterClient] Current TransmissionMode: {pipeClient.TransmissionMode}. Handle: {args[0]}");

            using StreamReader sr = new StreamReader(pipeClient);
            string? result;
            while((result = sr.ReadLine()) != null)
            {
                try
                {
                    Console.WriteLine($"[FileWriterClient] Received data, length={result.Length}");
                    var data = JsonSerializer.Deserialize<FileSaveStruct>(result) ?? throw new NullReferenceException();
                    if(data.Data == null) throw new NullReferenceException();
                    if(data.DataHash == null) throw new NullReferenceException();
                    if(data.Name == null) throw new NullReferenceException();
                    if(data.NameHash == null) throw new NullReferenceException();
                    if(!SHA1.HashData(data.Data).SequenceEqual(data.DataHash)) throw new InvalidDataException("Received data hash is not valid");
                    if(!SHA1.HashData(Encoding.UTF8.GetBytes(data.Name)).SequenceEqual(data.NameHash)) throw new InvalidDataException("Received name hash is not valid");
                    Console.WriteLine($"[FileWriterClient] Checks passed, writing to {data.Name}");
                    File.WriteAllBytes(data.Name, data.Data);
                    Console.WriteLine($"[FileWriterClient] SUCCESS: data saved");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"[FileWriterClient] ERROR: {ex.Message}");
                }
            }
            Console.WriteLine($"[FileWriterClient] ECommons.FileWriter is terminating.");
        }
    }
}