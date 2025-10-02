using BlazorDemo.Models.Entities;
using System.Net.Http.Json;

namespace BlazorDemo.Pages
{
    public partial class DinoList
    {
        private Dino[]? _dinos;
        private int? _dinoSelectedId;

        protected override async Task OnInitializedAsync()
        {
            HttpClient client = _factory.CreateClient("api");
            _dinos = await client.GetFromJsonAsync<Dino[]>("Dino");
        }

        private void AddDino()
        {
            _nav.NavigateTo("/dino/creation");
        }

        private void UpdateDino(int id)
        {
            _nav.NavigateTo($"/dino/creation/{id}");
        }

        private async Task DeleteDino(int dinoId)
        {
            HttpClient client = _factory.CreateClient("api");
            await client.DeleteAsync($"Dino/{dinoId}");
            _dinos = _dinos.Where(d => d.Id != dinoId).ToArray();
            if (_dinoSelectedId == dinoId)
                _dinoSelectedId = null;
        }

        private void GetDinoDetails(int dinoId)
        {
            _dinoSelectedId = dinoId;
        }

        private void CloseDetails()
        {
            _dinoSelectedId = null;
        }
    }
}