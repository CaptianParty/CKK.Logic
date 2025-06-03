using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class CheckoutModel
    {
        [Required]
        public Customer CustomerInfo { get; set; } = new();
        
    }
}
