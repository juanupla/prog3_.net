using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerApi.Models
{
    [Table("aviones")]
    public class Avion
    {

        public Guid Id { get; set; }

        public string Modelo { get; set; }

        public int CantidadPasajeros { get; set; }

        public string Matricula { get; set; }

        public DateTime FechaAlta { get; set; }

        [ForeignKey("IdMarca")] public MarcaAvion MarcaAvion { get; set; }
        public int IdMarca { get; set; }



    }
}
