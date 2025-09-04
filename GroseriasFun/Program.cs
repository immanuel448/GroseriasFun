
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GroseriasFun;

class Program
{
    static void Main()
    {
        string filePath = "groserias.json";
        string json = File.ReadAllText(filePath);

        var groserias = JsonSerializer.Deserialize<List<Groseria>>(json);

        foreach (var g in groserias)
        {
            Console.WriteLine($"ES: {g.Es} | EN: {g.En} | FR: {g.Fr}");
        }

        Console.WriteLine("\nFin del listado. Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }
}

