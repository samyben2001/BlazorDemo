using BlazorDemo.Models.Entities;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Components
{
    public partial class DemineurCase
    {
        [Parameter]
        public DemineurCaseInfos? Case { get; set; }
        [Parameter]
        public EventCallback<DemineurCaseInfos> OnCaseSelected { get; set; }

        private void OnSelect()
        {
            Case!.IsActive = true;
            OnCaseSelected.InvokeAsync(Case);
        }
    }
}