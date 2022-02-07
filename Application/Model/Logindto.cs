using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class Logindto : IValidatableObject
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید.")]
        [MinLength(3,ErrorMessage = "نام کاربری حداقل باید 3 کاراکتر باشد.")]
        [StringLength(100)]

        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید.")]
        [MinLength(8, ErrorMessage = "رمز ورود حداقل باید 8 کاراکتر باشد.")]
        [StringLength(100)]
        public string Password { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return null;
        }
    }
}
