using System.ComponentModel.DataAnnotations.Schema;

namespace CascadeDropdown.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateTime { get; set; }
        public string Email {  get; set; }
        public int CountryId {  get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int StateId {  get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
        public int CityId {  get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
