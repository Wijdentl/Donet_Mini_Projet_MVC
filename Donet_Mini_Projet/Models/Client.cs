using System.ComponentModel.DataAnnotations;

namespace Donet_Mini_Projet.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [Required]
        [EmailAddress] // Added validation for Email
        public string Email { get; set; }
        public ICollection<Article> Articles { get; set; } = new List<Article>();
        public ICollection<Reclamation> Reclamations { get; set; } = new List<Reclamation>();

        public Client()
        {
            Articles = new List<Article>();
            Reclamations = new List<Reclamation>();
        }
    }
}
