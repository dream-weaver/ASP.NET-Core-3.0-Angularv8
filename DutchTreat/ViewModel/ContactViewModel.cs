using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModel
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(150)]
        public string Subject { get; set; }
        [Required, StringLength(250)]
        public string Message { get; set; } 
    }
}
