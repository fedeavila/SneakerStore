using System.ComponentModel.DataAnnotations;

namespace SneakerStore.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "La marca es requerida!")]
        [Display(Name = "Marca")]
        public string BrandName { get; set; }

        public ICollection<Sneaker> SneakerList { get; set; } = new List<Sneaker>();
    }
}
