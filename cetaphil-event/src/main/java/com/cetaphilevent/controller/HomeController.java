package com.cetaphilevent.controller;

import java.io.IOException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequestMapping("")
public class HomeController {
	
	@RequestMapping("")
	public  void index(Device device, HttpServletResponse response, HttpServletRequest request) throws IOException {
		String pagePath = device.isNormal() ? "caribbean/index" : "caribbean/mobile";
		response.sendRedirect(request.getContextPath() + "/core");
	}
	
	@RequestMapping(value = "health", produces="text/plain")
	public @ResponseBody String healthCheck(){
		return "";
	}
}