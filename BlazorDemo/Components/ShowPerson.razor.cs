using BlazorDemo.Models.Entities;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Components
{
    public partial class ShowPerson
    {
        [Parameter]
        public Personne? Personne { get; set; }

        [Parameter]
        public EventCallback<Personne> OnPersonneSelected { get; set; }

        private void OnSelect()
        {
            OnPersonneSelected.InvokeAsync(Personne);
        }
    }
}