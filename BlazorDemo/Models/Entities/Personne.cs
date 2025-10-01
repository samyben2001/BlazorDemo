namespace BlazorDemo.Models.Entities
{
    public class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }


        public Personne(int id, string nom, string prenom, string email, string tel)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Tel = tel;
        }
    }
}
