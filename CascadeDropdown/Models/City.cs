using System.ComponentModel.DataAnnotations.Schema;

namespace CascadeDropdown.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId {  get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int StateId {  get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
