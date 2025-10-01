namespace BlazorDemo.Models.Entities
{
    public class Dino
    {
        public int Id { get; set; }
        public string Espece { get; set; }
        public int Taille { get; set; }
        public int Poids { get; set; }
        public Dino(int id, string espece, int taille, int poids)
        {
            Id = id;
            Espece = espece;
            Taille = taille;
            Poids = poids;
        }
    }
}
