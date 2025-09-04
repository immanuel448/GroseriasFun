using System.Text.Json;

string filePath = "groserias.json";
string json = File.ReadAllText(filePath);

var groserias = JsonSerializer.Deserialize<List<Groseria>>(json);

foreach (var g in groserias)
{
    Console.WriteLine($"ES: {g.Es} | EN: {g.En} | FR: {g.Fr}");
}

public class Groseria
{
    public int Id { get; set; }
    public string Es { get; set; }
    public string En { get; set; }
    public string Fr { get; set; }
}
