using Store.DataBaseManager;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;

namespace Store.Controllers
{
    public class PhoneShopController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(PhoneItem phoneItem)
        {
           await PhoneItemManager.AddPhoneAsync(phoneItem);
            var results = PhoneItemManager.GetPhones();
            return View("../Phone/AddResult");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "ExceptionFound")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var onephone = await PhoneItemManager.GetAsync(id);
            if (onephone != null)
            {
                return View(onephone);
            }
            else
            {
                var resultlist = await PhoneItemManager.GetPhonesAsync();
                var result = resultlist.ToPagedList(1, 3);
                return View(result);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var onephone = await PhoneItemManager.GetAsync(id);
            if (onephone == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                await PhoneItemManager.DeletePhoneAsync(id);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }
        [HttpGet]
        [HandleError(ExceptionType = typeof(ArgumentException), Master = "Index")]
        public async Task<ActionResult> Details(Guid id)
        {
            var result = await PhoneItemManager.GetAsync(id);
            if (result == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                return View("../Phone/Details", result);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var result = await PhoneItemManager.GetAsync(id);
            if (result == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(result);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(PhoneItem phoneItem)
        {
            await PhoneItemManager.EditPhoneAsync(phoneItem);
            //var result = InstrumentManager.GetInstruments();
            return RedirectToAction("../Home/Index");
        }

        [HttpPost]
        public ActionResult PhoneSearch(string name)
        {
            var res = PhoneItemManager.PhoneSearch(name);
            if (res.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView(res);
        }
    }
}