using BlazorDemo.Models.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace BlazorDemo.Pages
{
    public partial class DinoCreation
    {
        [SupplyParameterFromForm]
        private DinoCreationForm Dino { get; set; }

        [Parameter]
        public int? Id { get; set; } = null;

        // EditContext nécessaire si besoin de validation externe au FormModel
        private EditContext? _context;

        protected override async Task OnInitializedAsync()
        {
            Dino = new DinoCreationForm();
            _context = new EditContext(Dino);
            ValidationMessageStore messageStore = new ValidationMessageStore(_context);
            _context.OnFieldChanged += (sender, eventArgs) => Validate(_context, messageStore, eventArgs.FieldIdentifier);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id is not null)
            {
                HttpClient client = _factory.CreateClient("api");
                Dino = await client.GetFromJsonAsync<DinoCreationForm>($"Dino/{Id}");
                _context = new EditContext(Dino);
            }
        }

        private void Validate(EditContext context, ValidationMessageStore messageStore, FieldIdentifier field)
        {
            messageStore.Clear();

            if (Dino.Espece.ToLower() == "jos")
            {
                messageStore.Add(context.Field("Espece"), "Aucune Espèce ne peut s'appelée 'Jos'");
                context.NotifyValidationStateChanged();
            }
        }
        private async Task Submit()
        {
            if (Id is null)
            {
                HttpClient client = _factory.CreateClient("api");
                await client.PostAsJsonAsync($"Dino/", Dino);
            }
            else
            {
                HttpClient client = _factory.CreateClient("api");
                await client.PutAsJsonAsync($"Dino/{Id}", Dino);
            }
            _nav.NavigateTo("/dino");

        }
    }
}