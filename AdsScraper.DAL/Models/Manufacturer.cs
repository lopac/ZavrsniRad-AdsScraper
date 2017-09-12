namespace AdsScraper.DAL.Models
{
    using System.Collections.Generic;

    public class Manufacturer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manufacturer()
        {
            Models = new HashSet<Model>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Model> Models { get; set; }
    }
}
