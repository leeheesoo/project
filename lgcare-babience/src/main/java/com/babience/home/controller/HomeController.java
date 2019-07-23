package com.babience.home.controller;

import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.ModelAndView;

@Controller
@RequestMapping("")
public class HomeController {
	@RequestMapping("")
	public ModelAndView index(Device device){
		ModelAndView model = new ModelAndView(device.isNormal() ? "fourth/main" : "fourth/main-mobile");
		return model;
	}

	/* 1차이벤트 버전 URL */
	@RequestMapping("v1")
	public ModelAndView index_v1(Device device){
		ModelAndView model = new ModelAndView(device.isNormal() ? "first/main" : "first/main-mobile");
		return model;
	}
	
	/* 2-3차이벤트 버전 URL */
	@RequestMapping("v2")
	public ModelAndView index_v2(Device device){
		ModelAndView model = new ModelAndView(device.isNormal() ? "renewal/main" : "renewal/main-mobile");
		return model;
	}
	
	@RequestMapping(value = "health", produces="text/plain")
	public @ResponseBody String healthCheck(){
		return "";
	}
}
