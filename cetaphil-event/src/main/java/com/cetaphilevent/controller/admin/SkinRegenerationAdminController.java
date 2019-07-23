package com.cetaphilevent.controller.admin;

import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
@RequestMapping("/manager/skin-regeneration")
@PreAuthorize("isAuthenticated()")
public class SkinRegenerationAdminController {
	
	@RequestMapping("naver-searching/prize-setting")
	public ModelAndView prizeSetting(){
		ModelAndView model = new ModelAndView("admin/skinregeneration/prize-setting");
		return model;
	}	
}
