package com.cetaphilevent.controller.admin;

import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
@RequestMapping("/manager/core")
@PreAuthorize("isAuthenticated()")
public class CoreAdminController {
	
	@RequestMapping("naver-searching/prize-setting")
	public ModelAndView prizeSetting(){
		ModelAndView model = new ModelAndView("admin/core/prize-setting");
		return model;
	}	
}
