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
        // Nombre del archivo JSON que queremos leer
        string fileName = "groserias.json";

        // Ruta completa al archivo: combina la carpeta de la app (bin/Debug/net8.0)
        // con el nombre del archivo
        string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        // Mostrar en consola dónde está corriendo la app
        Console.WriteLine($"App base dir: {AppContext.BaseDirectory}");
        Console.WriteLine($"Buscando: {filePath}");

        // Verificar si el archivo existe en la carpeta de salida
        if (!File.Exists(filePath))
        {
            Console.WriteLine("¡No se encontró el archivo!");
            Console.WriteLine("Archivos en la carpeta de salida:");

            // Listar todos los archivos que sí existen en la carpeta de salida
            foreach (var f in Directory.GetFiles(AppContext.BaseDirectory))
                Console.WriteLine(" - " + Path.GetFileName(f));

            Console.WriteLine("\nAsegúrate de que 'groserias.json' esté en el proyecto y que tenga la opción 'Copy to Output Directory' = 'Copy if newer'.");
            Console.ReadKey(); // Pausa para que el usuario lea el mensaje
            return; // Sale del programa
        }

        // Leer todo el archivo JSON como texto en UTF-8
        string json = File.ReadAllText(filePath, Encoding.UTF8);

        // Mostrar el contenido crudo del JSON para depuración
        Console.WriteLine("\n--- Contenido del JSON leído ---");
        Console.WriteLine(json);
        Console.WriteLine("--- fin contenido ---\n");

        // Configuración del deserializador: no distinguir mayúsculas/minúsculas
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        List<Groseria> groserias = null;
        try
        {
            // Convertir el texto JSON en una lista de objetos Groseria
            groserias = JsonSerializer.Deserialize<List<Groseria>>(json, options);
        }
        catch (Exception ex)
        {
            // Si ocurre un error al deserializar, lo mostramos
            Console.WriteLine("Error al deserializar JSON:");
            Console.WriteLine(ex);
            Console.ReadKey();
            return;
        }

        // Mostrar cuántos elementos se cargaron
        Console.WriteLine($"\nTotal deserializado: {groserias?.Count ?? 0}");

        // Validar que la lista no esté vacía
        if (groserias == null || groserias.Count == 0)
        {
            Console.WriteLine("La lista está vacía o no se pudieron mapear las propiedades.");
            Console.ReadKey();
            return;
        }

        // Recorrer la lista e imprimir cada grosería con sus traducciones
        foreach (var g in groserias)
        {
            Console.WriteLine($"ES: {g?.Es ?? "<nulo>"} | EN: {g?.En ?? "<nulo>"} | FR: {g?.Fr ?? "<nulo>"}");
        }

        Console.WriteLine("\nFin. Presiona una tecla para salir.");
        Console.ReadKey(); // Espera que el usuario presione algo antes de cerrar
    }
}
