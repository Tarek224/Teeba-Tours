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
        [ForeignKey("Program")]
        public int Program_Id { get; set; }
        [ForeignKey("Tour")]
        public int Tour_Id { get; set; }

        
        public Program Program { get; set; }

        
        public Tour Tour { get; set; }
    }

    //////////////////////////////////////////////////////////////////
    [Table(name: "Place")]
    public class Place
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50)]
        public string Name { get; set; }

        public Location Location { get; set; }

        public string long_Description { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Short_Description { get; set; }

        public Images Images { get; set; }

        public Popularity Popularity { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public decimal Foriegner_Ticket { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public decimal Egyptian_Ticket { get; set; }

        public int Rate { get; set; }

        public virtual List<Program> Programs { get; set; }

    }

    ////////////////////////////////////////////////////////////////////////////////////////
    [Table(name: "Program")]
    public class Program
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required."), MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Description { get; set; }

        public int Place_Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        //[RegularExpression]
        public string Duration { get; set; }

        [ForeignKey("Place_Id")]
        public Place Place { get; set; }

        public virtual List<Tour_Program> tour_Programs { get; set; }

    }

    /////////////////////////////////////////////////////////////////////////////////
    [Table(name: "Tour")]
    public class Tour
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required."), MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 25)]
        public int Num_pass { get; set; }

        public int Num_Days { get; set; }

        public Location Location { get; set; }

        [Range(1000, 8000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Title { get; set; }

        public Images Images { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string Hotel_Name { get; set; }

        public virtual List<Passenger> Passengers { get; set; }

        public virtual List<Tour_Program> tour_Programs { get; set; }
    }

    //////////////////////////////////////////////////////////////////////////////
    [Table(name: "Dependent")]
    public class Dependent
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        public int? National_ID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int Age { get; set; }
        
        public int Passenger_Id { get; set; }

        [ForeignKey("Passenger_Id")]
        public Passenger Passenger { get; set; }
    }

    /////////////////////////////////////////////////////////////////////////////
    [Table(name: "Passenger")]
    public class Passenger
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int National_ID { get; set; }

        [Required(ErrorMessage = "This field is required."),EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public int Phone { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required."), Range(18, 70)]
        public int Age { get; set; }

        public int Tour_Id { get; set; }

        [ForeignKey("Tour_Id")]
        public Tour Tour { get; set; }

        public virtual List<Dependent> Dependents { get; set; }
    }
}