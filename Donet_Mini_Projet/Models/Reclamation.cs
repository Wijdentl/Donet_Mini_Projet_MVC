namespace Donet_Mini_Projet.Models
{
    public class Reclamation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string Description { get; set; }
        public enum StatutReclamation
        {
            EnAttente,
            EnCours,
            Résolue
        }
        public StatutReclamation Statut { get; set; }
        public DateTime DateSoumission { get; set; }
    }
}
