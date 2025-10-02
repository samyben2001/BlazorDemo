using BlazorDemo.Models.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorDemo.Components
{
    public partial class DinoDetails
    {
        [Parameter]
        public int? dinoId { get; set; }
        [Parameter]
        public EventCallback CloseDetailsEvent { get; set; }
        private Dino? DinoInfos { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (dinoId is not null)
            {
                HttpClient client = _factory.CreateClient("api");
                DinoInfos = await client.GetFromJsonAsync<Dino>($"Dino/{dinoId}");
            }
        }
        private void CloseDetails()
        {
            CloseDetailsEvent.InvokeAsync();
        }
    }
}