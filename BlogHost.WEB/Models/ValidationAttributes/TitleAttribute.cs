using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BlogHost.WEB.Validators;

namespace BlogHost.WEB.Models.ValidationAttributes
{
    public class TitleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return TextValidator.ValidateTitle(value.ToString());
        }
    }
}
