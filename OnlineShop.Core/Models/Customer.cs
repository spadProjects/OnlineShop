﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
   public class Customer : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} باید کمتر از 100 کارکتر باشد")]
        public string Phone { get; set; }
        [Display(Name = "کد ملی")]
        [MaxLength(100, ErrorMessage = "{0} باید کمتر از 100 کارکتر باشد")]
        public string NationalCode { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
