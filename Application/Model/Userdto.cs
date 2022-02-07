using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class Userdto : IValidatableObject
    {
        [Required(ErrorMessage = "نام کاربری وارد نشده است.")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "ایمیل وارد نشده است.")]
        [StringLength(100)]
        [DataType(dataType: DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "رمز عبور وارد نشده است.")]
        [StringLength(500)]
        public string Password { get; set; }

        [Required(ErrorMessage = "نام وارد نشده است.")]
        [StringLength(100)]
        public string FullName { get; set; }

        public int Age { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمیتواند Test باشد", new[] { nameof(UserName) });
            if (Password.Equals("123456"))
                yield return new ValidationResult("رمز عبور نمیتواند 123456 باشد", new[] { nameof(Password) });

        }

    }
}
