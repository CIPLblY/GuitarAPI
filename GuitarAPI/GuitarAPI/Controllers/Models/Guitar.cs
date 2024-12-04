namespace GuitarAPI.Controllers.Models // Здесь указано имя проекта
{
    public class Guitar
    {
        public int Id { get; set; }
        public string Model { get; set; } // Например, Telecaster, Stratocaster
        public string Manufacturer { get; set; } // Производитель
    }
}