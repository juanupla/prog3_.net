using PrimerApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerApi.Dto;

public class AvionDto
{
    public string Modelo { get; set; }

    public int CantidadPasajeros { get; set; }

    public string Matricula { get; set; }

    public DateTime FechaAlta { get; set; }

    //tiene que tener el mismo nombre que en la declaracion de la clave foranea
    public MarcaAvionDto MarcaAvion { get; set; }
    
    //public string MarcaAvionName { get; set; }
    //public DateTime MarcaAvionFechaAlta { get; set; }
}

