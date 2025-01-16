namespace Donet_Mini_Projet.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int GarantieEnMois { get; set; }
        public DateTime DateAchat { get; set; } 
        public bool EstSousGarantie => (DateTime.Now - DateAchat).TotalDays <= GarantieEnMois * 30;
        public decimal Prix { get; set; }
        public ICollection<PieceDeRechange> PieceDeRechanges { get; set; } = new List<PieceDeRechange>();
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public Article()
        {
            PieceDeRechanges = new List<PieceDeRechange>();
        }
    }
}
