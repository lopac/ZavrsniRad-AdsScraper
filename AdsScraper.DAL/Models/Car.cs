namespace AdsScraper.DAL.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public bool IsRegistred { get; set; }

        public int OwnersCount { get; set; }

        public int PriceEuro { get; set; }

        public int Kilometers { get; set; }

        public int Engine_Id { get; set; }

        public int? Year { get; set; }

        public int? Model_Id { get; set; }

        public virtual Engine Engine { get; set; }

        public virtual Model Model { get; set; }
    }
}
