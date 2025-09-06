using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using GroseriasFun;

class Program
{
    // Variables globales para que Menu() pueda acceder
    static List<Groseria> groserias;
    static string filePath;
    static JsonSerializerOptions options;

    static void Main()
    {
        string fileName = "groserias.json";
        filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Cargar JSON
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
        groserias = JsonSerializer.Deserialize<List<Groseria>>(json, options);

        if (groserias == null || groserias.Count == 0)
        {
            Console.WriteLine("La lista está vacía o no se pudieron mapear las propiedades.");
            Console.ReadKey();
            return;
        }

        // Mostrar resumen inicial
        Console.WriteLine($"\nTotal de groserías cargadas: {groserias.Count}");
        Console.WriteLine("\nPresiona cualquier tecla para entrar al menú...");
        Console.ReadKey();

        // Invocar menú interactivo
        Menu();
    }

    public static void Menu()
    {
        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== Groserías Fun ===");
            Console.WriteLine("1) Listar todas");
            Console.WriteLine("2) Buscar traducción");
            Console.WriteLine("3) Agregar grosería");
            Console.WriteLine("4) Salir");
            Console.Write("Elige opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ListarGroserias();
                    break;
                case "2":
                    BuscarTraduccion();
                    break;
                case "3":
                    AgregarGroseria();
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presiona cualquier tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ListarGroserias()
    {
        foreach (var g in groserias)
            Console.WriteLine($"ES: {g.Es} | EN: {g.En} | FR: {g.Fr}");

        Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
        Console.ReadKey();
    }

    static void BuscarTraduccion()
    {
        Console.Write("\nEscribe la grosería a buscar: ");
        string input = Console.ReadLine();

        Console.WriteLine("Selecciona idioma: (1) EN (2) FR");
        string idioma = Console.ReadLine();

        var encontrada = groserias
            .FirstOrDefault(g => g.Es.Contains(input, StringComparison.OrdinalIgnoreCase));

        if (encontrada != null)
        {
            string resultado = idioma == "2" ? encontrada.Fr : encontrada.En;
            Console.WriteLine($"Traducción: {resultado}");
        }
        else
        {
            Console.WriteLine("No encontrada en el diccionario.");
        }

        Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
        Console.ReadKey();
    }

    static void AgregarGroseria()
    {
        Console.Write("\nEscribe la grosería en español: ");
        string es = Console.ReadLine();

        Console.Write("Traducción EN: ");
        string en = Console.ReadLine();

        Console.Write("Traducción FR: ");
        string fr = Console.ReadLine();

        int nuevoId = groserias.Count > 0 ? groserias.Max(g => g.Id) + 1 : 1;

        var nueva = new Groseria { Id = nuevoId, Es = es, En = en, Fr = fr };
        groserias.Add(nueva);

        File.WriteAllText(filePath, JsonSerializer.Serialize(groserias, options));

        Console.WriteLine("Grosería agregada correctamente. Presiona cualquier tecla...");
        Console.ReadKey();
    }
}
