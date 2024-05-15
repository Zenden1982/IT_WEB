using System.ComponentModel.DataAnnotations;

namespace IT_WEB.Models
{
	public class ShopDto
	{
        [Required(ErrorMessage = "Поле \"Название\" является обязательным для заполнения.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле \"Описание\" является обязательным для заполнения.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле \"Адрес\" является обязательным для заполнения.")]
        public string address { get; set; }
	}
}
