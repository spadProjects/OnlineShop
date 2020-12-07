using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Infrastructure.Repositories;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class DiscountsController : Controller
    {
        private readonly DiscountsRepository _repo;
        private readonly OffersRepository _offerRepo;
        private readonly BrandsRepository _brandRepo;
        private readonly ProductGroupsRepository _productGroupRepo;
        private readonly ProductsRepository _productRepo;

        public DiscountsController(DiscountsRepository repo, OffersRepository offerRepo, BrandsRepository brandRepo, ProductGroupsRepository productGroupRepo, ProductsRepository productRepo)
        {
            _repo = repo;
            _offerRepo = offerRepo;
            _brandRepo = brandRepo;
            _productGroupRepo = productGroupRepo;
            _productRepo = productRepo;
        }
        // GET: Admin/Discounts
        public ActionResult Index()
        {
            return View(_repo.GetDiscounts());
        }
        public ActionResult Create()
        {
            var vm = new CreateDiscountViewModel();
            vm.Offers = _offerRepo.GetAll();
            vm.Brands = _brandRepo.GetAll();
            vm.ProductGroups = _productGroupRepo.GetAll();
            vm.Products = _productRepo.GetAll();
            return View(vm);
        }
    }
}