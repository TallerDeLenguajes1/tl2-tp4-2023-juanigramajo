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
        var cadetes = cadeteria.ListadoCadetes;
        return Ok(cadetes);
    }


    [HttpPost("AddPedido")] //agrega datos
    public ActionResult<Pedido> AddPedido(Pedido pedido)
    {
        var nuevoPedido = cadeteria.TomarPedido(pedido);
        return Ok(nuevoPedido);
    }

    [HttpPost("AddCadete")] //agrega datos
    public ActionResult<Cadete> AddCadete(Cadete cadete)
    {
        var nuevoCadete = cadeteria.AñadirCadete(cadete);
        return Ok(nuevoCadete);
    }


    [HttpPut("UpdatePedido")] //modifica datos
    public ActionResult<Pedido> UpdatePedido(Pedido pedido)
    {
        var updPed = cadeteria.UpdPedido(pedido);
        return Ok(updPed);
    }


    [HttpPut("AsignarPedido")]
    public ActionResult<Pedido> AsignarPedido(int nroPed, int cadeteID)
    {
        var updPed = cadeteria.AsignarCadeteAPedido(cadeteID, nroPed);
        return Ok(updPed);
    }

    [HttpPut("CambiarEstadoPedido")]
    public ActionResult<Pedido> CambiarEstadoPedido(int nroPed, string nuevoEstado)
    {
        var updEstadoPed = cadeteria.CambiarEstado(nroPed, nuevoEstado);
        return Ok(updEstadoPed);
    }

    [HttpPut("CambiarCadetePedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int nroPed, int cadeteID, int idCadeteNuevo)
    {
        var updCadetePed = cadeteria.ReasignarCadete(cadeteID, nroPed, idCadeteNuevo);
        return Ok(updCadetePed);
    }
   

}