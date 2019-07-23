using INGLife.Event.Domain.Identity;
using INGLife.Event.Domain.Infrastructures;
using INGLife.Event.Infrastructures;
using INGLife.Event.Models.AdminModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace INGLife.Event.Controllers.Admin {
    [IpAddressFilter(AllowIpAddresses = new string[] { "127.0.0.1", "211.60.50.131" })]
    [Authorize(Roles = "Administrators")]
    [RoutePrefix("manager/users")]
    public class AdminUserManageController : Controller {
        private AppUserManager userManager;
        private AppRoleManager roleManager;
        public AdminUserManageController (AppUserManager userManager, AppRoleManager roleManager) {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// 관리자 관리 목록화면
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index () {
            List<AppUser> list = userManager.Users.ToList();
            return View(list);
        }
        /// <summary>
        /// 관리자 수정 화면
        /// </summary>
        /// <param name="adminId">관리자 아이디</param>
        /// <returns></returns>
        [Route("edit")]
        [HttpGet]
        public ActionResult Edit (long adminId) {
            AppUser user = userManager.FindByIdAsync(adminId).Result;
            var roles = roleManager.Roles.ToList();

            AdminUserEditModel model = new AdminUserEditModel {
                AdminId = user.Id,
                UserName = user.UserName,
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
        [Route("edit")]
        [HttpPost]
        public ActionResult Edit (AdminUserEditModel model, string[] selectRoles) {
            if (ModelState.IsValid) {
                var user = userManager.FindByIdAsync(model.AdminId).Result;
                if (model.WillChangePassword) {
                    user.PasswordHash = userManager.PasswordHasher.HashPassword(model.Password);
                }
                user.Roles.Clear();
                foreach (string roleName in selectRoles) {
                    var Role = roleManager.FindByNameAsync(roleName).Result;
                    AppUserRole userRole = new AppUserRole {
                        RoleId = Role.Id,
                        UserId = user.Id
                    };
                    user.Roles.Add(userRole);
                }

                var updateResult = userManager.UpdateAsync(user).Result;
                if (updateResult.Succeeded) {
                    TempData["message"] = "정보 수정이 완료되었습니다.";
                    return RedirectToAction("Index");
                } else {
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
        [Route("delete")]
        public ActionResult Delete (long adminId) {
            AppUser deleteUser = userManager.FindByIdAsync(adminId).Result;
            IdentityResult result = userManager.DeleteAsync(deleteUser).Result;
            if (result.Succeeded) {
                TempData["message"] = "삭제되었습니다.";
                return RedirectToAction("Index");
            } else {
                AddErrorsFromResult(result);
            }
            return View();
        }

        /// <summary>
        /// 관리자계정추가
        /// </summary>
        /// <returns></returns>
        [Route("add")]
        [HttpGet]
        public ActionResult AddUser () {
            var roles = roleManager.Roles.ToList();
            AdminUserCreateModel model = new AdminUserCreateModel {
                Roles = roles
            };
            return View(model);
        }

        /// <summary>
        /// 관리자계정추가(DB저장 후 Index로 Redirect)
        /// </summary>
        /// <param name="user">추가할계정정보</param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public ActionResult AddUser (AdminUserCreateModel model) {
            if (!ModelState.IsValid) {
                TempData["message"] = "정보를 정확하게 입력해주세요.";
            }
            var user = new AppUser {
                UserName = model.UserName
            };
            if (model.SelectRoles != null) {
                foreach (var roleName in model.SelectRoles) {
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
                } else {
                    AddErrorsFromResult(result);
                }
            } else {
                TempData["message"] = "역할을 선택해주세요.";
            }
            model.Roles = roleManager.Roles.ToList();
            return View(model);
        }
        /// <summary>
        /// 뷰에서 에러메세지 노출을 위해 ModelState에 메세지 추가하는 유틸함수
        /// </summary>
        /// <param name="result"></param>
        private void AddErrorsFromResult (IdentityResult result) {
            foreach (string error in result.Errors) {
                ModelState.AddModelError("", error);
                TempData["message"] = error;
            }
        }
    }
}