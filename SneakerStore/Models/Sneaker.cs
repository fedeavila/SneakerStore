using System.ComponentModel.DataAnnotations;

namespace SneakerStore.Models
{
    public class Sneaker
    {
        public int SneakerId { get; set; }

        [Required(ErrorMessage = "El modelo es requerido!")]
        [StringLength(50, ErrorMessage = "El modelo debe tener entre {2} y {1} caracteres!", MinimumLength = 3)]
        [Display(Name = "Modelo")]
        public string Model { get; set; }

        [Required(ErrorMessage = "La marca es requerida!")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una marca!")]
        [Display(Name = "Marca")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "El precio es requerido!")]
        [Display(Name = "Precio")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor a cero!")]
        public decimal Price { get; set; }

        [Display(Name = "Marca")]
        public virtual Brand? Brand { get; set; }
    }
}
