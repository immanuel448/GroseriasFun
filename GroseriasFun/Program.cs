using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using GroseriasFun;

class Program
{
    static void Main()
    {
        string fileName = "groserias.json";
        string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        Console.WriteLine($"App base dir: {AppContext.BaseDirectory}");
        Console.WriteLine($"Buscando: {filePath}");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("¡No se encontró el archivo!");
            Console.WriteLine("Archivos en la carpeta de salida:");
            foreach (var f in Directory.GetFiles(AppContext.BaseDirectory))
                Console.WriteLine(" - " + Path.GetFileName(f));
            Console.WriteLine("\nAsegúrate de que 'groserias.json' esté en el proyecto y 'Copy to Output Directory' = 'Copy if newer'.");
            Console.ReadKey();
            return;
        }

        string json = File.ReadAllText(filePath, Encoding.UTF8);
        Console.WriteLine("\n--- Contenido del JSON leído ---");
        Console.WriteLine(json);
        Console.WriteLine("--- fin contenido ---\n");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        List<Groseria> groserias = null;
        try
        {
            groserias = JsonSerializer.Deserialize<List<Groseria>>(json, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al deserializar JSON:");
            Console.WriteLine(ex);
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nTotal deserializado: {groserias?.Count ?? 0}");
        if (groserias == null || groserias.Count == 0)
        {
            Console.WriteLine("La lista está vacía o no se pudieron mapear las propiedades.");
            Console.ReadKey();
            return;
        }

        foreach (var g in groserias)
        {
            Console.WriteLine($"ES: {g?.Es ?? "<nulo>"} | EN: {g?.En ?? "<nulo>"} | FR: {g?.Fr ?? "<nulo>"}");
        }

        Console.WriteLine("\nFin. Presiona una tecla para salir.");
        Console.ReadKey();
    }
}
