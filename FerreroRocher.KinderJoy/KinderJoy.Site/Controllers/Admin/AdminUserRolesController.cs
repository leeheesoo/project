﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Infrastructure;
using Microsoft.AspNet.Identity;

namespace KinderJoy.Site.Controllers.Admin {
    [Authorize(Roles = "Administrators")]
    [RoutePrefix("Manager/Roles")]
    public class AdminUserRolesController : Controller {
        private AppRoleManager roleManager;
        public AdminUserRolesController(AppRoleManager roleManager) {
            this.roleManager = roleManager;
        }

        /// <summary>
        /// 역할 리스트
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public ActionResult Index() {
            List<AppRole> list = roleManager.Roles.ToList();
            return View(list);
        }

        /// <summary>
        /// 역할 추가 화면
        /// </summary>
        /// <returns></returns>
        [Route("AddRole")]
        [HttpGet]
        public ActionResult AddRole() {
            return View();
        }

        /// <summary>
        /// 역할 추가 저장
        /// </summary>
        /// <param name="txtRole">역할이름</param>
        /// <returns></returns>
        [Route("AddRole")]
        [HttpPost]
        public ActionResult AddRole(string txtRole) {
            AppRole role = new AppRole {
                Name = txtRole
            };
            IdentityResult result = roleManager.Create(role);
            if (result.Succeeded) {
                TempData["message"] = "역할이 성공적으로 생성되었습니다.";
                return RedirectToAction("Index");
            }
            else {
                AddErrorsFromResult(result);
            }
            return View();
        }

        [Route("Delete")]
        public ActionResult Delete(long roleId) {
            AppRole role = roleManager.FindById(roleId);
            IdentityResult result = roleManager.Delete(role);
            if (result.Succeeded) {
                TempData["message"] = "역할이 성공적으로 삭제되었습니다.";
                return RedirectToAction("Index");
            }
            else {
                AddErrorsFromResult(result);
            }
            return View();
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