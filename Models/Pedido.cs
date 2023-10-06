
public class Pedido
{
    public int Nro { get; set; }
    public string Obs { get; set; }
    public Cliente cliente { get; set; }
    public string Estado { get; set; }
    public Cadete cadete { get; set; }

    public Pedido(){
    }

    public Pedido(int number, string observ, string status, string name, string address, int phoneNumb, string addressReferences){
        this.Nro = number;
        this.Obs = observ;
        this.cliente = new Cliente(name, address, phoneNumb, addressReferences);
        this.Estado = status;
    }

    public Pedido(int nro)
    {
        this.Nro = nro;
        this.Obs = "ninguna";
        this.cliente = CrearClienteAleatorio(); // Crear cliente aleatorio
        this.Estado = "En preparacion";
        this.cadete = null;
    }

    public Cliente CrearClienteAleatorio()
    {
        Random random = new Random();
        string nombre = "Cliente" + random.Next(1, 100);
        string direccion = "Dirección" + random.Next(1, 100);
        int telefono = random.Next(100000000, 999999999);
        string datosReferenciaDireccion = "Referencia" + random.Next(1, 100);
        Cliente clienteNuevo = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
        
        return clienteNuevo;
    }

    public void AsignarCadete(Cadete cadeteEnviado){
        this.cadete = cadeteEnviado;
    }

    public int getNroPedido(){

        return this.Nro;
    }

    public string getObs(){
        
        return this.Obs;
    }

    public string getEstado(){
        
        return this.Estado;
    }

    public int getIDCadete(){
        if (cadete == null)
        {
            return -1;
        } else
        {
            return this.cadete.getID();
        }
    }
    
    // public void ListarPedido(){
    //     Console.WriteLine($"\nPedido Nº[{this.Nro}]");
    //     Console.WriteLine($"Observación: " + this.Obs);
    //     Console.WriteLine($"Nombre del cliente: " + this.cliente.GetNombre());
    //     Console.WriteLine($"Estado: " + this.Estado);
    // }

    // public void VerDireccionCliente(){
    //     Console.WriteLine("La dirección del cliente es: " + this.cliente.GetDireccion());
    //     Console.WriteLine(this.cliente.GetDatosReferenciaDireccion());
    // }

    // public void VerDatosCliente(){
    //     Console.WriteLine("\nDatos del cliente:\n");
    //     Console.WriteLine("Nombre: " + this.cliente.GetNombre());
    //     Console.WriteLine("Direccion: " + this.cliente.GetDireccion());
    //     Console.WriteLine("Telefono: " + this.cliente.GetTelefono());
    //     Console.WriteLine("Datos de referencia de la direccion: " + this.cliente.GetDatosReferenciaDireccion());
    // }
}