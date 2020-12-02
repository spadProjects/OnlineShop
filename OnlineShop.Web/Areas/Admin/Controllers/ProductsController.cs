﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Helpers;
using OnlineShop.Infrastructure.Repositories;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductsRepository _repo;
        private readonly ProductGroupsRepository _pgRepo;
        private readonly ProductFeatureValuesRepository _featureRepo;
        private readonly ProductMainFeaturesRepository _mainFeatureRepo;

        public ProductsController(ProductsRepository repo, ProductGroupsRepository pgRepo, ProductFeatureValuesRepository featureRepo, ProductMainFeaturesRepository mainFeatureRepo)
        {
            _repo = repo;
            _pgRepo = pgRepo;
            _featureRepo = featureRepo;
            _mainFeatureRepo = mainFeatureRepo;
        }

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(_repo.GetProducts());
        }
        public ActionResult Create()
        {
            ViewBag.ProductGroups = _pgRepo.GetProductGroups();
            return View();
        }
        [HttpPost]
        public int? Create(NewProductViewModel product)
        {
            if (!ModelState.IsValid) return null;
            var prod = new Product();
            prod.Title = product.Title;
            prod.ShortDescription = product.ShortDescription;
            prod.Description = HttpUtility.UrlDecode(product.Description, System.Text.Encoding.Default);
            prod.BrandId = product.Brand;
            prod.ProductGroupId = product.ProductGroup;
            prod.Rate = product.Rate;
            prod.ShortDescription = product.ShortDescription;
            var addProduct = _repo.Add(prod);
            #region Adding Product Features

            foreach (var feature in product.ProductFeatures)
            {
                if (feature.IsMain)
                {
                    var model = new ProductMainFeature();
                    model.ProductId = addProduct.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    model.Quantity = feature.Quantity??0;
                    model.Price = feature.Price ?? 0;
                    _repo.AddProductMainFeature(model);
                }
                else
                {
                    var model = new ProductFeatureValue();
                    model.ProductId = addProduct.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    _repo.AddProductFeature(model);
                }
            }
            #endregion
            return addProduct.Id;

        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductGroups = _pgRepo.GetProductGroups();
            var product = _repo.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public int? Edit(NewProductViewModel product)
        {
            if (!ModelState.IsValid) return null;
            var prod = _repo.GetProduct(product.ProductId.Value);
            prod.Title = product.Title;
            prod.ShortDescription = product.ShortDescription;
            prod.Description = HttpUtility.UrlDecode(product.Description, System.Text.Encoding.Default);
            prod.BrandId = product.Brand;
            prod.ProductGroupId = product.ProductGroup;
            prod.Rate = product.Rate;
            prod.ShortDescription = product.ShortDescription;
            var updateProduct = _repo.Update(prod);
            #region Removing Previous Product Features
            foreach (var mainFeature in prod.ProductMainFeatures)
                _mainFeatureRepo.Delete(mainFeature.Id);
            foreach (var feature in prod.ProductFeatureValues)
                _featureRepo.Delete(feature.Id);
            #endregion

            #region Adding Product Features

            foreach (var feature in product.ProductFeatures)
            {
                if (feature.IsMain)
                {
                    var model = new ProductMainFeature();
                    model.ProductId = updateProduct.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    model.Quantity = feature.Quantity ?? 0;
                    model.Price = feature.Price ?? 0;
                    _repo.AddProductMainFeature(model);
                }
                else
                {
                    var model = new ProductFeatureValue();
                    model.ProductId = updateProduct.Id;
                    model.FeatureId = feature.FeatureId;
                    model.SubFeatureId = feature.SubFeatureId;
                    model.Value = feature.Value;
                    _repo.AddProductFeature(model);
                }
            }
            #endregion
            return updateProduct.Id;

        }
        [HttpPost]
        public bool UploadImage(int id, HttpPostedFileBase File)
        {
            #region Upload Image
            if (File != null)
            {
                var product = _repo.Get(id);
                if (product.Image != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("/Files/ProductGroupImages/Image/" + product.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ProductGroupImages/Image/" + product.Image));

                    if (System.IO.File.Exists(Server.MapPath("/Files/ProductGroupImages/Thumb/" + product.Image)))
                        System.IO.File.Delete(Server.MapPath("/Files/ProductImages/Thumb/" + product.Image));
                }
                // Saving Temp Image
                var newFileName = Guid.NewGuid() + Path.GetExtension(File.FileName);
                File.SaveAs(Server.MapPath("/Files/ProductImages/Temp/" + newFileName));
                // Resize Image
                ImageResizer image = new ImageResizer(850, 400, true);
                image.Resize(Server.MapPath("/Files/ProductImages/Temp/" + newFileName),
                    Server.MapPath("/Files/ProductImages/Image/" + newFileName));

                ImageResizer thumb = new ImageResizer(200, 200, true);
                thumb.Resize(Server.MapPath("/Files/ProductImages/Temp/" + newFileName),
                    Server.MapPath("/Files/ProductImages/Thumb/" + newFileName));

                // Deleting Temp Image
                System.IO.File.Delete(Server.MapPath("/Files/ProductImages/Temp/" + newFileName));
                product.Image = newFileName;
                _repo.Update(product);
                return true;

            }
            #endregion

            return false;
        }
        public JsonResult GetProductGroupFeatures(int id)
        {
            var features = _pgRepo.GetProductGroupFeatures(id);
            var obj = features.Select(item => new FeaturesObjViewModel() {Id = item.Id, Title = item.Title}).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductFeatures(int id)
        {
            var mainFeatures = _repo.GetProductMainFeatures(id);
            var features = _repo.GetProductFeatures(id);
            var obj = mainFeatures.Select(mainFeature => new ProductFeaturesViewModel()
                {
                    ProductId = mainFeature.ProductId,
                    FeatureId = mainFeature.FeatureId,
                    SubFeatureId = mainFeature.SubFeatureId,
                    IsMain = true,
                    Value = mainFeature.Value,
                    Quantity = mainFeature.Quantity,
                    Price = mainFeature.Price
                })
                .ToList();
            obj.AddRange(features.Select(feature => new ProductFeaturesViewModel() 
                {
                    ProductId = feature.ProductId, 
                    FeatureId = feature.FeatureId.Value, 
                    Value = feature.Value,
                    IsMain = false,
                    SubFeatureId = feature.SubFeatureId
                }));

            return Json(obj.GroupBy(a=>a.FeatureId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductGroupBrands(int id)
        {
            var brands = _pgRepo.GetProductGroupBrands(id);
            var obj = brands.Select(item => new BrandsObjViewModel() { Id = item.Id, Name = item.Name }).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFeatureSubFeatures(int id)
        {
            var subFeatures = _repo.GetSubFeaturesByFeatureId(id);
            var obj = subFeatures.Select(item => new SubFeaturesObjViewModel() {Id = item.Id, Value = item.Value}).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}