using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressConsultation.Persistence.Models
{
    public class AddressModel
    {
        public AddressModel(string id, string state, string city)
        {
            Id = id;
            State = state;
            City = city;
        }
        public string Id { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; } 
    }
}
