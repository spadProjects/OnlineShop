using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Repositories;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoicesRepository _repo;

        public InvoicesController(InvoicesRepository repo)
        {
            _repo = repo;
        }

        // GET: Admin/Invoices
        public ActionResult Index()
        {
            var invoices = _repo.GetInvoices();
            var vm = new List<InvoiceTableViewModel>();
            foreach (var invoice in invoices)
            {
               vm.Add(new InvoiceTableViewModel(invoice)); 
            }
            return View(vm);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _repo.Get(id.Value);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {

                _repo.Update(invoice);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _repo.Get(id.Value);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersianDate = new PersianDateTime(invoice.AddedDate).ToString();
            return PartialView(invoice);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var invoice = _repo.Get(id);
            _repo.Delete(id);
            return RedirectToAction("Index");
        }

    }
}