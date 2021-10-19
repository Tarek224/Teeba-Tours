using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Teeba_Project.Models
{
    public enum Location
    {
        Aswan = 1,
        Luxor = 2
    }
    public enum Images
    {
        img1 = 1,
        img2 = 2,
        img3 = 4,
        img4 = 8,
        img5 = 16
    }

    public enum Popularity
    {
        Popular,
        NotPopular
    }

    /*****************************************************************************/
    [Table(name: "Tour_Program")]
    public class Tour_Program
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int? Program_Id { get; set; }

        public int? Tour_Id { get; set; }

        [ForeignKey("Program_Id")]
        public Program Program { get; set; }

        [ForeignKey("Tour_Id")]
        public Tour Tour { get; set; }
    }

    //////////////////////////////////////////////////////////////////
    [Table(name: "Place")]
    public class Place
    {
        public int ID { get; set; }

        [Required]
        [RegularExpression("/w[A-z]")]
        [MaxLength(50)]
        public string Name { get; set; }

        public Location Location { get; set; }

        public string long_Description { get; set; }

        [Required]
        public string Short_Description { get; set; }

        public Images Images { get; set; }

        public Popularity Popularity { get; set; }

        [Required]
        public decimal Foriegner_Ticket { get; set; }

        [Required]
        public decimal Egyptian_Ticket { get; set; }

        public int Rate { get; set; }

        public List<Program> Programs { get; set; }

    }

    ////////////////////////////////////////////////////////////////////////////////////////
    [Table(name: "Program")]
    public class Program
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int? Place_Id { get; set; }

        [Required]
        //[RegularExpression]
        public string Duration { get; set; }

        [ForeignKey("Place_Id")]
        public Place Place { get; set; }

        public List<Tour_Program> tour_Programs { get; set; }

    }

    /////////////////////////////////////////////////////////////////////////////////
    [Table(name: "Tour")]
    public class Tour
    {
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(5, 20)]
        public int Num_pass { get; set; }

        public int Num_Days { get; set; }

        public Location Location { get; set; }

        [Range(1000, 5000)]
        public decimal Price { get; set; }

        [Required]
        public string Title { get; set; }

        public Images Images { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string Hotel_Name { get; set; }

        public List<Passenger> Passengers { get; set; }

        public List<Tour_Program> tour_Programs { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////
    [Table(name: "Dependent")]
    public class Dependent
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int? National_ID { get; set; }

        [Required]
        public int Age { get; set; }

        public string Nationality { get; set; }

        public int? Passenger_Id { get; set; }

        [ForeignKey("Passenger_Id")]
        public Passenger Passenger { get; set; }
    }

    /////////////////////////////////////////////////////////////////////////////
    [Table(name: "Passenger")]
    public class Passenger
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int National_ID { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }

        public string Address { get; set; }

        [Required, Range(18, 70)]
        public int Age { get; set; }

        public int Tour_Id { get; set; }

        [ForeignKey("Tour_Id")]
        public Tour Tour { get; set; }

        public virtual List<Dependent> Dependents { get; set; }
    }
}