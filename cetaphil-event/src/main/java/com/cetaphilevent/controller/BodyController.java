package com.cetaphilevent.controller;

import javax.servlet.http.HttpServletRequest;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

@Controller
@RequestMapping("/body")
public class BodyController {
	
	private Logger logger = LoggerFactory.getLogger(getClass());

	/*
	 * 1.    메인페이지 주소 : http://cetaphil.pentacle.co.kr/body
	 * 2.    서리나의 동안바디 주소 : http://cetaphil.pentacle.co.kr/body/contents
	 * 3.    동안바디 계산기 주소 : http://cetaphil.pentacle.co.kr/body/calculator
	 * 4.    동안바디 측정기 주소 : http://cetaphil.pentacle.co.kr/body/age
	 * 5.    동안바디 스페셜 키트 주소 : http://cetaphil.pentacle.co.kr/body/gift
	 * 6.    동안바디 챌린지 주소 : http://cetaphil.pentacle.co.kr/body/challenge
	 * */
	@RequestMapping(value = {"", "/contents", "/calculator", "/age", "/gift", "/challenge"})
	public ModelAndView Index(Device device, HttpServletRequest request) {
		ModelAndView model = new ModelAndView(device.isNormal() ? "body/pc" : "body/mobile");
		//meta tag
		model.addObject("ogtitle", "세타필 #동안바디 이벤트!");
		model.addObject("ogdescription", "동안의 완성은 바디!");
		model.addObject("ogurl", "https://cetaphil.pentacle.co.kr" + request.getRequestURI());
		
		String path = request.getRequestURI().replaceAll("/", "").replace("body", "");
		if (path != null && !path.isEmpty()) {
			model.addObject("path", path);
			
			//ogdescription
			switch (path) {
				case "contents":					
					model.addObject("ogdescription", "동안바디를 확인하시고 푸짐한 경품도 받아보세요.");					
					break;
				case "calculator":
					model.addObject("ogdescription", "세타필 얼굴 vs 바디 계산기를 통해 푸짐한 경품을 받아보세요.");
					break;
				case "age":
					model.addObject("ogdescription", "지금 나의 바디 나이 확인하고, 푸짐한 경품도 받아보세요.");
					break;
				case "challenge":
					model.addObject("ogdescription", "동안바디챌린지! 제품 후기 남기고 푸짐한 경품도 받아보세요.");					
					break;
				default:
					break;
			}
			
			//ogurl
			String queryString = request.getQueryString();
			if (queryString != null && !queryString.isEmpty()) {
				if (path.equals("contents") || path.equals("calculator") || path.equals("age") || path.equals("challenge")) {
					if (queryString.contains("&utm_source=facebook")) {
						model.addObject("ogurl", "https://cetaphil.pentacle.co.kr/body/" + path + "?utm_campaign=body&utm_medium=" + path + "Sharing&utm_source=facebook");
					} else if (queryString.contains("&utm_source=kakaostory")) {
						model.addObject("ogurl", "https://cetaphil.pentacle.co.kr/body/" + path + "?utm_campaign=body&utm_medium=" + path + "Sharing&utm_source=kakaostory");					
					}
				}
			}
		}
		return model;
	}
}
