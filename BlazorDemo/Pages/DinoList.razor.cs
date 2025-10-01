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
        private void UpdateDino()
        {
            throw new NotImplementedException();
        }
        private async Task DeleteDino(int dinoId)
        {
            HttpClient client = _factory.CreateClient("api");
            await client.DeleteAsync($"Dino/{dinoId}");
        }
        private void GetDinoDetails()
        {
            throw new NotImplementedException();
        }
    }
}