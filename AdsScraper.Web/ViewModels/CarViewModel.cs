#region usings



#endregion

namespace AdsScraper.Web.ViewModels
{
    public class CarViewModel
    {
        public int ModelId { get; set; }
        public int OwnersCountId { get; set; }
        public int Year { get; set; }
        public int Kilometers { get; set; }
        public bool IsRegistred { get; set; }
        public int EngineTypeId { get; set; }
        public int Volume { get; set; }
        public int Power { get; set; }
        public int GearboxTypeId { get; set; }
        public int GearsCount { get; set; }
        public bool HasGasInstallation { get; set; }
    }
}