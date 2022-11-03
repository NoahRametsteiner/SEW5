namespace Fahrzeug_Telematik.Models
{
    public class Car { 
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Typ { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
