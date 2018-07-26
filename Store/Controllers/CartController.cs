﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        //установка сессии для корзины и получение доступа к ней
        private CartManager GetCart()
        {
            CartManager cart = (CartManager)Session["Cart"];
            if (cart == null)
            {
                cart = new CartManager();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult Index()
        {
            var resultList = GetCart().Lines;
            ViewBag.TotalSum = GetCart().ComputeTotalValue();
            return View(resultList);
        }
        public RedirectToRouteResult AddToCart(Guid id)
        {
            PhoneItem phone = PhoneItemManager.Get(id);
            if (phone != null)
            {
                GetCart().AddItem(phone, 1);
            }
            ViewBag.Count = GetCart().Lines.Count();
            return RedirectToAction("Index", "Home");
        }
        public RedirectToRouteResult MinusQuantityFromCart(Guid id)
        {
            PhoneItem phone = PhoneItemManager.Get(id);
            if (phone != null)
            {
                GetCart().AddItem(phone, -1);
            }
            ViewBag.Count = GetCart().Lines.Count();
            return RedirectToAction("Index", "Cart");
        }
        public RedirectToRouteResult PlusQuantityFromCart(Guid id)
        {
            PhoneItem phone = PhoneItemManager.Get(id);
            if (phone != null)
            {
                GetCart().AddItem(phone, 1);
            }
            ViewBag.Count = GetCart().Lines.Count();
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult RemoveFromCart(Guid id)
        {
            PhoneItem phone = PhoneItemManager.Get(id);
            if (phone != null)
            {
                GetCart().RemoveLine(phone);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}