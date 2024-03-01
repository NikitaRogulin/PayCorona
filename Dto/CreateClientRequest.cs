using System.ComponentModel.DataAnnotations;

namespace PayCorona.Dto
{
    public class CreateClientRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Нужно заполнить имя")]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Login { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
