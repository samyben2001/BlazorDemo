using BlazorDemo.Models.Entities;
using System.Net.Http.Json;

namespace BlazorDemo.Pages
{
    public partial class DinoList
    {
        private Dino[]? _dinos;

        protected override async Task OnInitializedAsync()
        {
            HttpClient client = _factory.CreateClient("api");
            _dinos = await client.GetFromJsonAsync<Dino[]>("Dino");
        }
    }
}