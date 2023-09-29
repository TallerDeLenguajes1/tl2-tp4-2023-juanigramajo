using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class AccesoADatos
{

    public AccesoADatos()
    {
    }
    public abstract Cadeteria CargarDatosCadeteria();
    public abstract void cargarDatosCadetes(Cadeteria cadeteria);
}

public class AccesoCSV : AccesoADatos
{
    public AccesoCSV() : base()
    {

    }
    public override Cadeteria CargarDatosCadeteria()
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

    public override void cargarDatosCadetes(Cadeteria cadeteria)
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
                cadeteria.AñadirCadete(cadete);
            }
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }
    }
}

public class AccesoJSON : AccesoADatos
{
    public AccesoJSON() : base()
    {

    }
    public override Cadeteria CargarDatosCadeteria()
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

    public override void cargarDatosCadetes(Cadeteria cadeteria)
    {
        string filePath = "datosCadetes.json"; // Reemplaza con la ruta de tu archivo CSV.
        
        try
        {
            // Lee el contenido del archivo JSON y deserialízalo en una lista de cadetes.
            string json = File.ReadAllText(filePath);
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);

            foreach (Cadete cadete in cadetes)
            {
                cadeteria.AñadirCadete(cadete);
            }
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }
    }
}