using System.ComponentModel.DataAnnotations;

namespace BlazorDemo.Models.Forms
{
    public class DinoCreationForm
    {
        public int? Id { get; set; }

        [Required(ErrorMessage ="L'espèce est requise!")]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "L'espèce doit contenir entre 1 et 128 caractères!")]
        public string Espece { get; set; } = String.Empty;

        [Required(ErrorMessage = "La taille est requise!")]
        [Range(1, int.MaxValue, ErrorMessage = "Le poids doit être posititf!")]
        public int Taille { get; set; }

        [Required(ErrorMessage = "Le poids est requis!")]
        [Range(1, int.MaxValue, ErrorMessage = "Le poids doit être posititf!")]
        public int Poids { get; set; }
    }
}
