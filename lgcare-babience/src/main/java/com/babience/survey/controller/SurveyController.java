package com.babience.survey.controller;

import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

import com.babience.utils.CommonProvider;

@Controller
@RequestMapping("kabrita_try")
public class SurveyController {
	
	@Autowired
	private CommonProvider common;
	
	//4차 이벤트 기간 설정
	private Integer experienceEndDate = 20190331;
	private Integer reviewEndDate = 20190414;
	
	@RequestMapping("")
	public ModelAndView survey(Device device, HttpSession session){
		session.removeAttribute("survey-advance-application-id");
		session.removeAttribute("survey-experience-id");	
		
		Integer now = common.getNow();
		ModelAndView model = new ModelAndView(device.isNormal() ? "fourth/survey-event" : "fourth/survey-event-mobile");		
		
		//체험신청이벤트 - 이벤트 기간인지 확인
		boolean isProceedingExperience = false;
		if (now <= experienceEndDate) {
			isProceedingExperience = true;
		}
		model.addObject("isProceedingExperience", isProceedingExperience);
		
		//후기이벤트 - 이벤트 기간인지 확인
		boolean isProceedingReview = false;
		if (now <= reviewEndDate) {
			isProceedingReview = true;
		}
		model.addObject("isProceedingReview", isProceedingReview);
		
		return model;
	}
	
	/* 1차 이벤트 버전 URL */	
	@RequestMapping("v1")
	public ModelAndView survey_v1(Device device){
		ModelAndView model = new ModelAndView(device.isNormal() ? "first/survey-event" : "first/survey-event-mobile");
		return model;
	}

	/* 2-3차이벤트 버전 URL */	
	@RequestMapping("v2")
	public ModelAndView survey_v2(Device device){
		ModelAndView model = new ModelAndView(device.isNormal() ? "renewal/survey-event" : "renewal/survey-event-mobile");
		model.addObject("isProceedingReview", false);		
		return model;
	}
}
