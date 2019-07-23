package com.babience.nickname.controller;

import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
@RequestMapping(value = "kabrita_nickname")
public class NicknameController {
	@RequestMapping(value = "")
	public ModelAndView nickname(Device device) {
		ModelAndView model = new ModelAndView(device.isNormal() ? "first/nickname-event" : "first/nickname-event-mobile");
		model.addObject("type", "nickname");
		return model;
	}
}
