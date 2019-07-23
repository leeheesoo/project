using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Site.Infrastructure.Admin;
using KinderJoy.Site.Models.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace KinderJoy.Site.Controllers.Admin
{   
    [RoutePrefix("manager")]
    public class ManagerController : Controller
    {
        private IAuthenticationManager authManager;
        private AppUserManager userManager;

        public ManagerController(IAuthenticationManager authManager, AppUserManager userManager) {
            this.authManager = authManager;
            this.userManager = userManager;
        }
        /// <summary>
        /// 킨더 관리자 페이지
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index() {
            if (!HttpContext.User.Identity.IsAuthenticated) {
                return RedirectToAction("Login");
            }
            return View();
        }
        #region Elmah 에러로그 관련 메서드
        [Route("errorlog")]
        [Authorize(Roles="Administrators")]
        public ActionResult ErrorLog(string type) {
            return new ElmahResult(type);
        }

        [Route("errorlog/detail")]
        [Authorize(Roles = "Administrators")]
        public ActionResult Detail(string type) {
            return new ElmahResult(type);
        }
        #endregion

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
        [AllowAnonymous]
        public async Task<ActionResult> Login(AdminLoginModel model, string ReturnUrl) {
            if (ModelState.IsValid) {
                AppUser user = await userManager.FindAsync(model.ID, model.Password);
                if (user == null) {
                    ModelState.AddModelError("","아이디 또는 비밀번호가 잘못되었습니다.");
                }
                else {
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
        /// <summary>
        /// 로그아웃
        /// </summary>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        [Route("logout")]
        [Authorize]
        public ActionResult Logout(string returnurl) {
            authManager.SignOut();
            return View("Login");
        }
        #endregion
    }
}