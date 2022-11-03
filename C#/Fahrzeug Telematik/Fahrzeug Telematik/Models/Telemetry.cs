namespace Fahrzeug_Telematik.Models
{
    public class Telemetry
    {
        public long Id { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public int? Speed { get; set; }
        public int? Capacity { get; set; }
        public long CarId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
