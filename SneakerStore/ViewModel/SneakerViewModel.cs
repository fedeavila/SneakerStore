using SneakerStore.Models;

namespace SneakerStore.ViewModel
{
    public class SneakerViewModel
    {
        public List<Sneaker> sneakers { get; set; } = new List<Sneaker>();
        public string filtroMarca { get; set; }
    }


}
