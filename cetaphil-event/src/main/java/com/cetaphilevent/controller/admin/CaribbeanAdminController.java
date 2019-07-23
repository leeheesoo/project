package com.cetaphilevent.controller.admin;

import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import com.cetaphilevent.model.admin.User;

@Controller
@RequestMapping("/manager")
public class CaribbeanAdminController {
	
	@RequestMapping("/cetaphil-event")
	@PreAuthorize("isAuthenticated()")
	public ModelAndView index(){

		ModelAndView model = new ModelAndView("admin/caribbean/index");
		return model;
	}
}