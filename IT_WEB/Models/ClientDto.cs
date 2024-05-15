using System.ComponentModel.DataAnnotations;

namespace IT_WEB.Models
{
    public class ClientDto
    {
        [Required(ErrorMessage = "Поле \"Название\" является обязательным для заполнения.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле \"Номер телефона\" является обязательным для заполнения.")]
        public string Number { get; set; }
    }
}
