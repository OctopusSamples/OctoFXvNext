using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Mvc;
using OctoFX.Core.Model;
using OctoFX.Core.Repository;
using OctoFX.Core.Util;
using OctoFX.TradingWebsite.Models;

namespace OctoFX.TradingWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = repository
                .FindBy(a => a.Email == model.Email)
                .FirstOrDefault();

              if (account != null)
              {
                  if (PasswordHasher.VerifyPassword(model.Password, account.PasswordHashed))
                  {
                      var claims = new List<Claim>();
                      claims.Add(new Claim(ClaimTypes.Email, model.Email));
                      
                      await Context.Authentication.SignInAsync(
                          CookieAuthenticationDefaults.AuthenticationScheme, 
                          new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), 
                          new AuthenticationProperties() { IsPersistent = model.RememberMe }
                      );
                      
                      return string.IsNullOrWhiteSpace(returnUrl) ? (ActionResult)RedirectToAction("Index", "Home") : Redirect(returnUrl);
                  }
              }
              ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        public async Task<ActionResult> LogOff()
        {
            await Context.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = repository
                .FindBy(a => a.Email == model.Email)
                .FirstOrDefault();

            if (account != null)
            {
                ModelState.AddModelError("Email", "An account with this email address already exists.");
                return View(model);
            }

            account = new Account();
            account.Email = model.Email;
            account.Name = model.Name;
            account.PasswordHashed = PasswordHasher.HashPassword(model.Password);
            account.IsActive = true;

            repository.Add(account);
            repository.Save();

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, account.Email));
            
            await Context.Authentication.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)), 
                new AuthenticationProperties() { IsPersistent = true }
            );

            return RedirectToAction("Index", "Home");
        }
    }
}