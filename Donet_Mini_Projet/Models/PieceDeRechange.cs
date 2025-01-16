namespace Donet_Mini_Projet.Models
{
    public class PieceDeRechange
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Stock { get; set; }
        public bool EstDisponible => Stock > 0;
    }
}
