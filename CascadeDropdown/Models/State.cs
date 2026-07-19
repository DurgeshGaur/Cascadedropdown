using System.ComponentModel.DataAnnotations.Schema;

namespace CascadeDropdown.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId {  get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}
