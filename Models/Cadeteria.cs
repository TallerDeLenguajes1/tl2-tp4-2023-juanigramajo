using System.Text.Json.Serialization;

public class Cadeteria
{
    public string Nombre { get; set; }
    public int Telefono { get; set; }
    public List<Cadete> ListadoCadetes { get; set; }
    public List<Pedido> ListadoPedidos { get; set; }
    private static Cadeteria cadeteriaSingleton;
    public static Cadeteria GetCadeteria()
    {
        if (cadeteriaSingleton==null)
        {
            cadeteriaSingleton = new Cadeteria("cadeteria", 38177363);
        }
        return cadeteriaSingleton;
    }


    public Cadeteria(){
    }

    public Cadeteria(string name, int phoneNumb){
        this.Nombre = name;
        this.Telefono = phoneNumb;
        this.ListadoCadetes = new List<Cadete>();
        this.ListadoPedidos = new List<Pedido>();
    }

    public void TomarPedido(int num, string observ, string status, string name, string address, int phoneNumb, string addressReferences){
        Pedido pedido = new Pedido(num, observ, status, name, address, phoneNumb, addressReferences);
        ListadoPedidos.Add(pedido);
    }

    public Pedido TomarPedido(Pedido pedido)
    {
        ListadoPedidos.Add(pedido);
        pedido.Nro = ListadoPedidos.Count;
        return pedido;
    }
    
    // public void ListarPedidos(){

    //     foreach (Pedido ped in this.ListadoPedidos)
    //     {            
    //         // Console.WriteLine($"\n--------------------------");
    //         ped.ListarPedido();
    //     }
    // }

    public Pedido DevolverTalPedido(int nroPedido){

        Pedido pedid = new Pedido();
        foreach (Pedido ped in this.ListadoPedidos)
        {
            if (ped.getNroPedido() == nroPedido)
            {
                pedid = ped;
            }
        }
        
        return pedid;
    }

    // public void VerDatosCadeteria(){
    //     Console.WriteLine("\nNombre: " + this.Nombre);
    //     Console.WriteLine("Telefono: " + this.Telefono);
    // }

    public string VerNombreCadeteria(){
        
        return this.Nombre;
    }

    public int VerTelefonoCadeteria(){
        
        return this.Telefono;
    }

    public List<Cadete> MostrarCadetes(){
        // foreach (Cadete cad in this.ListadoCadetes)
        // {
        //     Console.WriteLine($"\nCadete [{cad.getID()}]");
        //     Console.WriteLine($"Nombre: " + cad.getNombre());
        //     Console.WriteLine($"Dirección: " + cad.getDireccion());
        //     Console.WriteLine($"Telefono: " + cad.getTelefono());
        // }

        return this.ListadoCadetes;
    }

    public int JornalACobrar(int cadeteID){
        int jornal = 0;
        foreach (Cadete cad in this.ListadoCadetes)
        {
            if (cad.getID() == cadeteID)
            {
                jornal = (cad.getPedEntregados() * 500);
            }
        }
    
        return jornal;
    }

    // public void GenerarInforme(){
    //     int total = 0;
    //     foreach (Cadete cad in this.ListadoCadetes)
    //     {
    //         Console.WriteLine("\n------------------------------------------------");
    //         Console.WriteLine($"\nCadete Nº[{cad.getID()}]");
    //         Console.WriteLine("\nCantidad de envios en el día: " + cad.getPedEntregados());
    //         Console.WriteLine("\nTotal recaudado en el día: " + this.JornalACobrar(cad.getID()));
    //         total = total + this.JornalACobrar(cad.getID());
    //     }
    //     Console.WriteLine("\n\n\nTotal ganado en el día por la cadetería: " + total);
    // }

    // public void MostrarPedidosDeCadetes(){
    //     foreach (Cadete cad in this.ListadoCadetes)
    //     {
    //         cad.ListarPedidos();
    //     }
    // }

    // public void MostrarPedidosDeCadetes(int idCadete){

    //     int bandera = 0;

    //     Console.WriteLine("\n-----------------------");
    //     Console.WriteLine($"\nCadete Nº[{idCadete}]");

    //     foreach (Pedido ped in this.ListadoPedidos)
    //     {
    //         if (ped.getIDCadete() == idCadete)
    //         {
    //             ped.ListarPedido();
    //             bandera = 1;
    //         }
    //     }

    //     if (bandera == 0)
    //     {
    //         Console.WriteLine($"\n--- Aún no posee pedidos para entregar ---");
    //     }
    // }

    public Pedido AsignarCadeteAPedido(int cadeteID, int nroPed){

        Pedido pedReturn = new Pedido();

        foreach (Pedido ped in this.ListadoPedidos)
        {
            if (ped.getNroPedido() == nroPed)
            {
                foreach (Cadete cad in this.ListadoCadetes)
                {
                    if (cad.getID() == cadeteID)
                    {
                        ped.AsignarCadete(cad);
                        // Console.WriteLine($"\nPedido Nº[{nroPed}] asignado con éxito al cadete Nº[{cadeteID}]");
                    }
                }

                pedReturn = ped;
            }
        }

        return pedReturn;
    }

    // public void ReasignarCadete(int nroPedido, int idCadeteActual, int idCadeteNuevo){
        
    //     Pedido pedid = new Pedido();
        
    //     foreach (Cadete cad in this.ListadoCadetes)
    //     {
    //         if (cad.getID() == idCadeteActual)
    //         {
    //             // Busco el pedido para ser reasignado
    //             pedid = cad.devolverTalPedido(nroPedido);

    //             // Busco el cadete y le asigno el pedido
    //             foreach (Cadete cade in this.ListadoCadetes)
    //             {
    //                 if (cade.getID() == idCadeteNuevo)
    //                 {
    //                     cade.AsignarPedido(pedid);
    //                 }
    //             }

    //             // Elimino el pedido del listado de pedidos del cadete
    //             cad.EliminarPedidoSinEntregar(nroPedido);
    //         }
    //     }
    // }

    public Pedido ReasignarCadete(int nroPedido, int idCadeteActual, int idCadeteNuevo){
        
        Pedido pedReturn = new Pedido();

        foreach (Pedido ped in this.ListadoPedidos)
        {
            if (ped.getIDCadete() == idCadeteActual)
            {
                foreach (Cadete cad in this.ListadoCadetes)
                {
                    if (cad.getID() == idCadeteNuevo)
                    {
                        ped.AsignarCadete(cad);
                    }
                }

                pedReturn = ped;
            }
        }

        // Console.WriteLine($"\nPedido Nº[{nroPedido}] reasignado con éxito al cadete Nº[{idCadeteNuevo}]");
        return pedReturn;
    }

    public Cadete AñadirCadete(Cadete cadete){
        this.ListadoCadetes.Add(cadete);
        return cadete;
    }

    public void EliminarCadete(int id){
        foreach (Cadete cad in this.ListadoCadetes)
        {
            if (cad.getID() == id)
            {
                this.ListadoCadetes.Remove(cad);
                // Console.WriteLine("Se eliminó el cadete con éxito");
            }
        }
    }

    // public void ModificarCadete(int id, string name, string address, int phoneNumb){
    //     foreach (Cadete cad in this.ListadoCadetes)
    //     {
    //         if (cad.getID == id)
    //         {
    //             cad.ID = id;
    //             cad.Nombre = name;
    //             cad.Direccion = address;
    //             cad.Telefono = phoneNumb;
    //             Console.WriteLine("Se modificó el cadete con éxito");
    //         }
    //     }
    // }
    
    public void FinalizarPedido(int nro){

        for (int i = 0; i < this.ListadoPedidos.Count; i++)
        {
            Pedido ped = this.ListadoPedidos[i];
            if (ped.getNroPedido() == nro)
            {
                foreach (Cadete cad in this.ListadoCadetes)
                {
                    if (cad.getID() == ped.getIDCadete())
                    {
                        cad.SumarEntrega();
                    }
                }
                this.ListadoPedidos.Remove(ped);
                // Console.WriteLine($"\nSe entregó el pedido Nº[{nro}] con éxito");
                // Resta 1 a i para compensar el desplazamiento después de eliminar un elemento
                i--;
            }
        }
    }

    public void EliminarPedidoSinEntregar(int nro)
    {
        for (int i = 0; i < this.ListadoPedidos.Count; i++)
        {
            Pedido ped = this.ListadoPedidos[i];
            if (ped.getNroPedido() == nro)
            {
                this.ListadoPedidos.Remove(ped);
                // Console.WriteLine("\nSe eliminó el pedido con éxito");
                // Resta 1 a i para compensar el desplazamiento después de eliminar un elemento
                i--;
            }
        }
    }

    // codigo del profesor adaptado a mi clase
    public Pedido UpdPedido(Pedido pedido)
    {
      Pedido auxpedido = ListadoPedidos.FirstOrDefault(t => t.Nro == pedido.Nro);
      auxpedido.Obs = pedido.Obs;
      return auxpedido;
    }

    public Pedido CambiarEstado(int nroPedido, string nuevoEstado)
    {
        Pedido pedReturn = new Pedido();

        foreach (Pedido ped in this.ListadoPedidos)
        {
            if (ped.Nro == nroPedido)
            {
                ped.Estado = nuevoEstado;
            }

            pedReturn = ped;
        }
        
        return pedReturn;
    }
}