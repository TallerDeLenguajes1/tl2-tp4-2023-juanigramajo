using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;

    private readonly ILogger<CadeteriaController> _logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();//para poder usar el patron de diseño singleton de la cadeteria para tener persistencia de datos
    }


    [HttpGet] 
    public ActionResult<string> GetNombreCadeteria()
    {
        return Ok(cadeteria.Nombre);
    }


    [HttpGet] //muestra datos
    [Route("Pedidos")]
    public ActionResult<IEnumerable<Pedido>> GetPedidos()
    {
        var pedidos = cadeteria.ListadoPedidos;
        return Ok(pedidos);
    }


    [HttpGet]
    [Route("Cadetes")]
    public ActionResult<IEnumerable<Cadete>> GetCadetes()
    {
        var cadetes = cadeteria.cargarCadetes.Obtener();
        return Ok(cadetes);
    }


    [HttpPost("AddPedido")] //agrega datos
    public IActionResult AddPedido(Pedido pedido)
    {
        int cant = cadeteria.ListadoPedidos.Count;
        var nuevoPedido = cadeteria.TomarPedido(pedido);

        if (cadeteria.ListadoPedidos.Count == cant + 1)
        {
            cadeteria.cargarPedidos.Guardar(cadeteria.ListadoPedidos);
            return Ok(nuevoPedido);
        }
        else
        {
            return StatusCode(500, "Ha ocurrido un error agregando el pedido.");
        }
    }

    [HttpPost("AddCadete")] //agrega datos
    public ActionResult<Cadete> AddCadete(Cadete cadete)
    {
        int cant = cadeteria.ListadoCadetes.Count;
        var nuevoCadete = cadeteria.AñadirCadete(cadete);

        if (cadeteria.ListadoCadetes.Count == cant + 1)
        {
            cadeteria.cargarCadetes.Guardar(cadeteria.ListadoCadetes);
            return Ok(nuevoCadete);
        }
        else
        {
            return StatusCode(500, "Ha ocurrido un error agregando el pedido.");
        }
    }


    [HttpPut("UpdatePedido")] //modifica datos
    public ActionResult<Pedido> UpdatePedido(Pedido pedido)
    {
        var updPed = cadeteria.UpdPedido(pedido);
        cadeteria.cargarPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok(updPed);
    }


    [HttpPut("AsignarPedido")]
    public IActionResult AsignarPedido(int nroPed, int cadeteID)
    {
        var updPed = cadeteria.AsignarCadeteAPedido(cadeteID, nroPed);
        cadeteria.cargarPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok($"Pedido {nroPed} asignado al cadete {cadeteID}.");
    }

    [HttpPut("CambiarEstadoPedido")]
    public IActionResult CambiarEstadoPedido(int nroPed, string nuevoEstado)
    {
        var updEstadoPed = cadeteria.CambiarEstado(nroPed, nuevoEstado);
        cadeteria.cargarPedidos.Guardar(cadeteria.ListadoPedidos);
        return Ok($"Estado del pedido {nroPed} cambiado a {nuevoEstado}.");
    }


    [HttpPut("CambiarCadetePedido")]
    public ActionResult CambiarCadetePedido(int nroPed, int cadeteID, int idCadeteNuevo)
    {
        var updCadetePed = cadeteria.ReasignarCadete(cadeteID, nroPed, idCadeteNuevo);
        cadeteria.cargarPedidos.Guardar(cadeteria.ListadoPedidos);

        return Ok($"Cadete del pedido {nroPed} asignado a {idCadeteNuevo}.");
    }
}