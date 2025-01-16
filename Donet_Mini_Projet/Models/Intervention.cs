namespace Donet_Mini_Projet.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public int ReclamationId { get; set; }
        public Reclamation Reclamation { get; set; }
        public DateTime DateIntervention { get; set; }
        public List<PieceDeRechange> PiecesUtilisees { get; set; } = new List<PieceDeRechange>();
        public decimal CoutMainDOeuvre { get; set; }

        public decimal CoutTotal => PiecesUtilisees.Sum(p => p.Prix) + CoutMainDOeuvre;

        public enum StatutIntervention
        {
            Planifiee,
            EnCours,
            Terminee
        }
        public StatutIntervention Statut { get; set; }
    }
}
