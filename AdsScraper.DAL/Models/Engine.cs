namespace AdsScraper.DAL.Models
{
    using System.Collections.Generic;
    public enum GearboxType
    {
        Manual,
        Automatic
    }
    public class Engine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Engine()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public int Volume { get; set; }

        public int Power { get; set; }

        public GearboxType GearboxType { get; set; }

        public int GearsCount { get; set; }

        public bool HasGasInstallation { get; set; }

        public int? Fuel_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }

        public virtual Fuel Fuel { get; set; }
    }
}
