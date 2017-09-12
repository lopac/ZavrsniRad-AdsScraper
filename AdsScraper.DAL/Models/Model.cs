namespace AdsScraper.DAL.Models
{
    using System.Collections.Generic;

    public class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
