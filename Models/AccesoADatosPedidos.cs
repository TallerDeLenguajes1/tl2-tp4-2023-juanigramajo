using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class AccesoADatosPedidos
{
    public List<Pedido> ListadoPedidos = new List<Pedido>();
    public abstract List<Pedido> Obtener();
    public void Guardar(List<Pedido> listado)
    {
        ListadoPedidos = listado;
        string contenido = JsonSerializer.Serialize(ListadoPedidos);

        string filePath = "pedidos.json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        File.WriteAllText(filePath, contenido);
    }
}

public class AccesoPedidosCSV : AccesoADatosPedidos
{
    public override List<Pedido> Obtener()
    {
        string filePath = "pedido.csv";
        Pedido pedido = new Pedido();
            
        try
        {
            // Lee todas las líneas del archivo CSV.
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');

                // Accede a los datos de cada columna según su posición.
                int numeroPedido = int.Parse(line);
                pedido = new Pedido(numeroPedido);

                ListadoPedidos.Add(pedido);
            }
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }

        return ListadoPedidos;
    }
}

public class AccesoPedidosJSON : AccesoADatosPedidos
{
    public override List<Pedido> Obtener()
    {
        string filePath = "pedidos.json"; // Reemplaza con la ruta de tu archivo CSV.
        
        try
        {
            // Lee el contenido del archivo JSON y deserialízalo en una lista de cadetes.
            string json = File.ReadAllText(filePath);
            ListadoPedidos = JsonSerializer.Deserialize<List<Pedido>>(json);
        }
        catch (IOException e)
        {
            // Console.WriteLine($"Error al leer el archivo: {e.Message}");
        }
    
    return ListadoPedidos;
    }
}