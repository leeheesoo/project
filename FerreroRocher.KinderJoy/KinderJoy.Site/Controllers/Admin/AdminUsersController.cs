using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Identity;
using KinderJoy.Site.Models.Admin;

namespace KinderJoy.Site.Controllers.Admin
{
    [Authorize(Roles = "Administrators")]
    [RoutePrefix("Manager/Admin")]
    public class AdminUsersController : Controller {
        private AppUserManager userManager;
        private AppRoleManager roleManager;
        public AdminUsersController(AppUserManager userManager, AppRoleManager roleManager) {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// 관리자 관리 목록화면
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index() {
            List<AppUser> list = userManager.Users.ToList();
            return View(list);
        }
        /// <summary>
        /// 관리자 수정 화면
        /// </summary>
        /// <param name="adminId">관리자 아이디</param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpGet]
        public ActionResult Edit(long adminId) {
            AppUser user = userManager.FindById(adminId);
            var roles = roleManager.Roles.ToList();

            AdminUserEditModel model = new AdminUserEditModel {
                AdminId = user.Id,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.PasswordHash,
                Roles = roles
            };
            return View(model);
        }
        /// <summary>
        /// 관리자 수정(패스워드변경)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpPost]
        public ActionResult Edit(AdminUserEditModel model, string[] selectRoles) {
            if (ModelState.IsValid) {
                var user = userManager.FindById(model.AdminId);
                if (model.WillChangePassword) {
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(model.Password);
                }
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Roles.Clear();
                foreach (string roleName in selectRoles) {
                    var Role = roleManager.FindByName(roleName);
                    AppUserRole userRole = new AppUserRole {
                        RoleId = Role.Id,
                        UserId = user.Id
                    };
                    user.Roles.Add(userRole);
                }

                var updateResult = userManager.Update(user);
                if (updateResult.Succeeded) {
                    TempData["message"] = "정보 수정이 완료되었습니다.";
                    return RedirectToAction("Index");
                }
                else {
                    AddErrorsFromResult(updateResult);
                }
            }
            return View(model);
        }
        /// <summary>
        /// 특정 관리자 삭제
        /// </summary>
        /// <param name="adminId">삭제할 관리자 아이디</param>
        /// <returns></returns>
        [Route("Delete")]
        public ActionResult Delete(long adminId) {
            AppUser deleteUser = userManager.FindById(adminId);
            IdentityResult result = userManager.Delete(deleteUser);
            if (result.Succeeded) {
                TempData["message"] = "삭제되었습니다.";
                return RedirectToAction("Index");
            }
            else {
                AddErrorsFromResult(result);
            }
            return View();
        }

        /// <summary>
        /// 관리자계정추가
        /// </summary>
        /// <returns></returns>
        [Route("AddUser")]
        [HttpGet]
        public ActionResult AddUser() {
            var roles = roleManager.Roles.ToList();
            AdminUserEditModel model = new AdminUserEditModel {
                Roles = roles
            };
            return View(model);
        }

        /// <summary>
        /// 관리자계정추가(DB저장 후 Index로 Redirect)
        /// </summary>
        /// <param name="user">추가할계정정보</param>
        /// <returns></returns>
        [Route("AddUser")]
        [HttpPost]
        public ActionResult AddUser(AdminUserEditModel model, string[] selectRoles) {
            var user = new AppUser {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            foreach (string roleName in selectRoles) {
                var Role = roleManager.FindByName(roleName);
                AppUserRole userRole = new AppUserRole {
                    RoleId = Role.Id,
                    UserId = user.Id
                };
                user.Roles.Add(userRole);
            }
            IdentityResult result = userManager.Create(user, model.Password);
            if (result.Succeeded) {
                TempData["message"] = "관리자 계정이 생성되었습니다.";
                return RedirectToAction("Index");
            }
            else {
                AddErrorsFromResult(result);
            }
            return View("Index");
        }
        /// <summary>
        /// 뷰에서 에러메세지 노출을 위해 ModelState에 메세지 추가하는 유틸함수
        /// </summary>
        /// <param name="result"></param>
        private void AddErrorsFromResult(IdentityResult result) {
            foreach (string error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }
    }
}