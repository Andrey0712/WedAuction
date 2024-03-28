using System.ComponentModel.DataAnnotations;

namespace WebAuction.Models
{
    

    /// <summary>
    /// Модель для створення категорії
    /// </summary>

    public class CreateCategoryViewModel
    {
        [Display(Name = "Назва категорії"),Required(ErrorMessage ="Поле не може бути пустим")]
        public string Name { get; set; }
        [Display(Name ="Фото категорії")]
        public IFormFile Image { get; set; }

    }

    /// <summary>
    /// Модель для виведення категорії
    /// </summary>
    public class ItemCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    /// <summary>
    /// Модель для видалення категорії
    /// </summary>
    public class DelCItemCategoryModel
    {
        public int Id { get; set; }
    }
}
