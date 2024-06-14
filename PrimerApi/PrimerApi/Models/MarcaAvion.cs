using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerApi.Models
{
    [Table("marcaAviones")]
    public class MarcaAvion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
