namespace WebAuction.Models
{
    public class RegisterViewModel
    {

        /// <summary>
        /// Ім'я
        /// </summary>
        /// <example>Іван</example>
        public string Name { get; set; }

        /// <summary>
        /// Електронна пошта
        /// </summary>
        /// <example>Ira@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        /// <example>Qwerty</example>
        public string Password { get; set; }
        /// <summary>
        /// Підтверження пароль
        /// </summary>
        /// <example>Qwerty</example>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Оберіть фото користувача
        /// </summary>
        public IFormFile? Avatar { get; set; }
    }

    public class UserViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Avatar { get; set; }

    }

    public class LoginUserModel
    {
        /// <summary>
        /// Електронна пошта
        /// </summary>
        /// <example>Ira@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Підтверження пароль
        /// </summary>
        /// <example>12345</example>
        public string Password { get; set; }
    }

    public class TokenViewModel
    {
        /// <summary>
        /// token
        /// </summary>
        /// <example>eyJpZCI6IjEzMzciLCJ1c2VybmFtZSI6ImJpem9uZSIsImlhdCI6MTU5NDIwOTYwMCwicm9sZSI6InVzZXIifQ</example>
        public string token { get; set; }
    }

    public class UserDelModel
    {
        /// <summary>
        /// id users
        /// </summary>
        /// <example>2</example>
        public long Id {  set; get; }
    }

    public class EditUserModel
    {
        /// <summary>
        /// id users
        /// </summary>
        /// <example>2</example>
        public long Id { set; get; }
        /// <summary>
        /// Ім'я
        /// </summary>
        /// <example>Іван</example>
        public string? Name { get; set; }

        /// <summary>
        /// Електронна пошта
        /// </summary>
        /// <example>Ira@gmail.com</example>
        public string? Email { get; set; }
       
        /// <summary>
        /// Оберіть фото користувача
        /// </summary>
        public IFormFile? Avatar { get; set; }

    }
}
