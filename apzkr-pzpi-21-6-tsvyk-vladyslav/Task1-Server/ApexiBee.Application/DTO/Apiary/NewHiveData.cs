namespace ApexiBee.Application.DTO
{
    public class NewHiveData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfFrames { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Guid? ApiaryId { get; set; }

        public string SerialNumber { get; set; }
    }
}
