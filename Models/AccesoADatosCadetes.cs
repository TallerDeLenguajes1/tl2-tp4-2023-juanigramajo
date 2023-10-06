using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class AccesoADatosCadetes
{
    public List<Cadete> ListadoCadetes = new List<Cadete>();
    public abstract List<Cadete> Obtener();
    public void Guardar(List<Cadete> lista)
    {
        ListadoCadetes = lista;
    }
}

public class AccesoCadetesCSV : AccesoADatosCadetes
{
    public override List<Cadete> Obtener()
    {
        string filePath = "datosCadetes.csv"; // Reemplaza con la ruta de tu archivo CSV.
        
        try
        {
            // Lee todas las líneas del archivo CSV.
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');

                // Accede a los datos de cada columna según su posición.
                string columna1 = fields[0];
                string columna2 = fields[1];
                string columna3 = fields[2];
                string columna4 = fields[3];

                Cadete cadete = new Cadete(int.Parse(columna1), columna2, columna3, int.Parse(columna4));
                ListadoCadetes.Add(cadete);
            }
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }
        
        return ListadoCadetes;
    }
}

public class AccesoCadetesJSON : AccesoADatosCadetes
{
    public override List<Cadete> Obtener()
    {
        string filePath = "datosCadetes.json"; // Reemplaza con la ruta de tu archivo CSV.
        
        try
        {
            // Lee el contenido del archivo JSON y deserialízalo en una lista de cadetes.
            string json = File.ReadAllText(filePath);
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);

            foreach (Cadete cadete in cadetes)
            {
                ListadoCadetes.Add(cadete);
            }
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }
        
        return ListadoCadetes;
    }
}