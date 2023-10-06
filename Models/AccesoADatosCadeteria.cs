using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class AccesoADatosCadeteria
{

    public Cadeteria cadeteria = null;
    public abstract Cadeteria Obtener();
}

public class AccesoCadeteriaCSV : AccesoADatosCadeteria
{
    public override Cadeteria Obtener()
    {
        string filePath = "datosCadeteria.csv";
        Cadeteria cadeteria = new Cadeteria();
            
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

                cadeteria = new Cadeteria(columna1, int.Parse(columna2));
            }
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }

        return cadeteria;
    }
}

public class AccesoCadeteriaJSON : AccesoADatosCadeteria
{
    public override Cadeteria Obtener()
    {
        string filePath = "datosCadeteria.json";
        Cadeteria cadeteria = new Cadeteria();
            
        try
        {
            // Lee el contenido del archivo JSON y deserialízalo en una instancia de Cadeteria.
            string json = File.ReadAllText(filePath);
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }

        return cadeteria;
    }
}