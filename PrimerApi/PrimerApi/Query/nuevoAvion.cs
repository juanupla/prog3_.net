using PrimerApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerApi.Query;
public class nuevoAvion
{
    public string Modelo { get; set; }

    public int CantidadPasajeros { get; set; }

    public string Matricula { get; set; }

    public DateTime FechaAlta { get; set; }

    public int IdMarca { get; set; }

}

