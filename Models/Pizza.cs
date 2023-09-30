using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [Column(TypeName = "text")]
        public string Descrizione { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }
        public int Prezzo { get; set; }

        public Pizza(string nome, string descrizione, string image, int prezzo)
        {
            Nome = nome;
            Descrizione = descrizione;
            Image = image;
            Prezzo = prezzo;
        }
    }
}
