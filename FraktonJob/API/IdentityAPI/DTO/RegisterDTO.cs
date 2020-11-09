﻿using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.DTO
{
    public class RegisterDTO
    {
        #region Properties
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
        #endregion
    }
}
