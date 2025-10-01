using BlazorDemo.Models.Entities;
using System.Threading.Tasks;

namespace BlazorDemo.Pages
{
    public partial class Contacts
    {
        private List<Personne> _personnes = new List<Personne>();
        private Personne? _selectedPersonne = null;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
            _personnes = new()
            {
                new Personne(1, "ben beya", "samy", "sam.ben@gmail.com", "+32473813737"),
                new Personne(2, "ben beya", "fouad", "fouad.ben@gmail.com", "+32498813637"),
                new Personne(3, "ben beya", "lina", "lina.ben@gmail.com", "+32473813939"),
            };
        }

        private void OnPersonneSelected(Personne personne) { 
            _selectedPersonne = personne;
        }
    }
}