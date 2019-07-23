using Samsonite.Mall.Domain.Identity;
using Samsonite.Mall.Domain.Infrastructures;
using Samsonite.Mall.Models.AdminModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Samsonite.Mall.Infrastructure.Admin;

namespace Samsonite.Mall.Controllers.Admin {
    [RoutePrefix("manager")]
    public class AdminHomeController : Controller {
        private IAuthenticationManager authManager;
        private AppUserManager userManager;
        private AppRoleManager roleManager;

        public AdminHomeController(IAuthenticationManager authManager, AppUserManager userManager, AppRoleManager roleManager) {
            this.authManager = authManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [Route("")]
        public ActionResult Index() {
            if (!HttpContext.User.Identity.IsAuthenticated) {
                return RedirectToAction("Login");
            }
            return View();
        }


        #region 로그인
        /// <summary>
        /// 로그인 - 화면
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [HttpGet]
        public ActionResult Login(string returnUrl) {
            if (HttpContext.User.Identity.IsAuthenticated) {
                if (!HttpContext.User.IsInRole("Administrators") && returnUrl.ToLower().IndexOf("errorlog") > -1) {
                    return RedirectToAction("Index");
                }
                return Url.IsLocalUrl(returnUrl) ? (ActionResult)Redirect(returnUrl) : RedirectToAction("Index");
            }
            CheckAdminUserYN();
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// 로그인 - 로그인성공
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(AdminLoginModel model, string ReturnUrl) {
            if (ModelState.IsValid) {
                AppUser user = await userManager.FindAsync(model.ID, model.Password);
                if (user == null) {
                    //ModelState.AddModelError("Password", "아이디 또는 비밀번호가 잘못되었습니다.");
                    TempData["message"] = "아이디 또는 비밀번호가 잘못되었습니다.";
                } else {
                    authManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                    ClaimsIdentity ident = await userManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignOut();
                    authManager.SignIn(new AuthenticationProperties {
                        IsPersistent = true
                    }, ident);
                    ViewBag.AdminId = model.ID;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.returnUrl = ReturnUrl;
            return View(model);
        }

        private void CheckAdminUserYN() {
            var adminRole = roleManager.FindByNameAsync("Administrators");
            long adminRoleId = -1;
            if(adminRole.Result == null){
                var appRole = new AppRole {
                    Name = "Administrators"
                };
                var result = roleManager.CreateAsync(appRole).Result;
                if (result.Succeeded) {
                    adminRoleId = appRole.Id;
                }
            }

            var adminUser = userManager.FindByNameAsync("youma@mz.co.kr");
            if(adminUser.Result == null) {
                var appUser = new AppUser {
                    UserName = "youma@mz.co.kr",
                    Email = "youma@mz.co.kr",
                };
                var result = userManager.CreateAsync(appUser, "dbaldud123@").Result;

                if (result.Succeeded) {
                    appUser.Roles.Add(new AppUserRole {
                        UserId = appUser.Id,
                        RoleId = adminRoleId
                    });
                }
                userManager.Update(appUser);
            }
        }
        /// <summary>
        /// 로그아웃
        /// </summary>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        [Route("logout")]
        [Authorize]
        public ActionResult Logout(string returnurl) {
            authManager.SignOut();
            return RedirectToAction("Login");
        }
        #endregion

        #region Elmah 에러로그 관련 메서드
        [Route("errorlog")]
        [Authorize(Roles = "Administrators")]
        public ActionResult ErrorLog(string type) {
            return new ElmahResult(type);
        }

        [Route("errorlog/detail")]
        [Authorize(Roles = "Administrators")]
        public ActionResult Detail(string type) {
            return new ElmahResult(type);
        }
        #endregion
    }
}