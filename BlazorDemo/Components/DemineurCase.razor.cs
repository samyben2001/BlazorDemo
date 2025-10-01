using BlazorDemo.Enums;
using BlazorDemo.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorDemo.Components
{
    public partial class DemineurCase
    {
        [Parameter]
        public DemineurCaseInfos? Case { get; set; }
        [Parameter]
        public EventCallback<DemineurCaseInfos> OnCaseSelected { get; set; }

        [Parameter]
        public EventCallback<DemineurCaseInfos> OnCaseFlaged { get; set; }

        private void OnSelect(MouseEventArgs args)
        {
            if (args.AltKey)
            {
                OnCaseFlaged.InvokeAsync(Case);
            }
            else
            {
                OnCaseSelected.InvokeAsync(Case);
            }
        }

        private string SetClass()
        {
            if (!Case!.IsActive)
            {
                if (!Case.IsFlaged)
                {
                    return "demineurCase";
                }
                else
                {
                    return "demineurCaseFlaged";
                }
            }
            else
            {
                if (Case.Etat == DemineurEtat.Zero)
                {
                    return "demineurCaseZeroOn";
                }
                else if (Case.Etat == DemineurEtat.Bombe)
                {
                    return "demineurCaseBombOn";
                }
                else
                {
                    return "demineurCaseOn";
                }
            }
        }
    }
}