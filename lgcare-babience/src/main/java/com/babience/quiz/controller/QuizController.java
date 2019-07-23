package com.babience.quiz.controller;

import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;

import com.babience.utils.CommonProvider;

@Controller
@RequestMapping("kabrita_quiz")
public class QuizController {
	
	@Autowired
	private CommonProvider common;
	
	//4차 이벤트 기간 설정
	private Integer quizEndDate = 20190413;
	
	@RequestMapping("")
	public ModelAndView quiz(Device device, HttpSession session){
		session.removeAttribute("renewal-quiz-id");
		
		Integer now = common.getNow();
		ModelAndView model = new ModelAndView(device.isNormal() ? "fourth/quiz-event" : "fourth/quiz-event-mobile");
	
		//이벤트 기간인지 확인
		boolean isProceedingQuiz = false;
		if (now <= quizEndDate) {
			isProceedingQuiz = true;
		}
		model.addObject("isProceedingQuiz", isProceedingQuiz);
		
		return model;
	}
	
	/* 2-3차이벤트 버전 URL */
	@RequestMapping("v2")
	public ModelAndView quiz_v2(Device device) {
		ModelAndView model = new ModelAndView(device.isNormal() ? "renewal/quiz-event" : "renewal/quiz-event-mobile");
		return model;
	}
}
