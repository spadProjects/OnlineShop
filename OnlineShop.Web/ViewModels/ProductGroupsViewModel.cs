﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Core.Models;

namespace OnlineShop.Web.ViewModels
{
    public class NewProductGroupViewModel
    {
        public string Title { get; set; }
        public List<int> BrandIds { get; set; }
        public int ParentGroupId { get; set; }
        public List<int> ProductGroupFeatureIds { get; set; }
    }
    public class UpdateProductGroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> BrandIds { get; set; }
        public int ParentGroupId { get; set; }
        public List<int> ProductGroupFeatureIds { get; set; }
    }
}