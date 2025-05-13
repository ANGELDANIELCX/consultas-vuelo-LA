using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route ("api/vuelos")]
public class VuelosController : Controller{
    [HttpGet ("ciudades-origen")]
    public IActionResult CiudadesOrigen(){
        var client  = new MongoClient(CadenasConexion.MONGO_DB);
        var db = client .GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");
   
        var lista = collection.Distinct<string>("CiudadOrigen", FilterDefinition<Vuelo>.Empty).ToList();
  
        return Ok(lista);
    }

    [HttpGet ("ciudades-destino")]
    public IActionResult Ciudadesdestino(){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        var lista = collection.Distinct <string>("CiudadDestino", FilterDefinition<Vuelo>.Empety).ToList();
        return Ok(lista);
    }

    
    [HttpGet ("estatus ")]
    public IActionResult ListarEstatus(){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        var lista = collection.Distinct <string>("EstatusVuelo", FilterDefinition<Vuelo>.Empety).ToList();

        return Ok(lista);
    }

    [HttpGet("listar-vueltos ")]
    public IActionResult ListarVuelos(string estatus ){
            var client = new MongoClient(CadenaConexion.MONGO_DB);
            var db = client.GetDatabase("Aeropuerto");
            var collection = db.GetCollection<Vuelo>("Vuelos");

            List<FilterDefinition<Vuelos>> filters = new List<FilterDefinition<Vuelos>>();

            if(!string.IsNullOrWhiteSpace(estatus)){
                var filterEstatus = Builers<Vuelo>.Filter.Eq(x =>x.EstatusVuelo, estatus);
                filters.Add(filterEstatus);
            }

            List<Vuelo> vuelos;
            if(filters.Count>0){
                var filter = Builders<Vuelo>.Filter.And(filters); 
                vuelos = collection.Find(Filter).ToList();
            }

        return Ok();
    }

    
}
