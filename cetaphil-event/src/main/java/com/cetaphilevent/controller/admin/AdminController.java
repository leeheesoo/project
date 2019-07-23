package com.cetaphilevent.controller.admin;

import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import com.cetaphilevent.model.admin.User;

@Controller
@RequestMapping("/manager")
public class AdminController {
	
	@RequestMapping("")
	@PreAuthorize("isAuthenticated()")
	public ModelAndView index(){

		ModelAndView model = new ModelAndView("admin/index");
		return model;
	}
	
	@RequestMapping(value="login",method=RequestMethod.GET)
	public ModelAndView login(){
		ModelAndView model = new ModelAndView("admin/login");
		return model;
	}
	
	@RequestMapping(value="add-user",method=RequestMethod.GET)
	@PreAuthorize("hasAnyAuthority('Administrator')")
	public ModelAndView AddUser(){
		ModelAndView model = new ModelAndView("admin/user/add-user");
		
		User user =  new User();
		model.addObject("user",user);
		
		return model;
	}
}