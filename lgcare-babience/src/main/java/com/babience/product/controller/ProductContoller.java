package com.babience.product.controller;

import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
@RequestMapping("kabrita_quality")
public class ProductContoller {
	@RequestMapping("")
	public ModelAndView product(Device device){
		return new ModelAndView(device.isNormal() ? "fourth/product" : "fourth/product-mobile");
	}
	
	/* 2-3차이벤트 버전 URL */
	@RequestMapping("v2")
	public ModelAndView product_v2(Device device){
		return new ModelAndView(device.isNormal() ? "renewal/product" : "renewal/product-mobile");
	}
}
