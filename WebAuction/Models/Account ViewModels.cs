﻿namespace WebAuction.Models
{
    public class RegisterViewModel
    {

        /// <summary>
        /// Ім'я
        /// </summary>
        /// <example>Іван</example>
        public string? Name { get; set; }
        
        /// <summary>
        /// Електронна пошта
        /// </summary>
        /// <example>Email@gmail.com</example>
        public string? Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        /// <example>Qwerty</example>
        public string? Password { get; set; }
        /// <summary>
        /// Підтверження пароль
        /// </summary>
        /// <example>Qwerty</example>
        public string? ConfirmPassword { get; set; }
        /// <summary>
        /// Оберіть фото користувача
        /// </summary>
        public IFormFile? Avatar { get; set; }
    }
}
