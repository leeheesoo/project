package com.babience.quiz.controller.api;

import javax.servlet.http.HttpSession;
import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.babience.quiz.dao.RenewalQuiz;
import com.babience.quiz.dto.RenewalQuizDTO;
import com.babience.quiz.service.RenewalQuizService;
import com.babience.utils.CommonProvider;
import com.babience.utils.SharingTypeDto;
import com.babience.utils.exception.EventServiceException;

@RestController
@RequestMapping("api/kabrita-renewal/quiz")
public class RenewalQuizApiController {
	@Autowired
	private RenewalQuizService service;
	@Autowired
	private CommonProvider common;
	
	private ModelMapper modelMapper = new ModelMapper();
	
	//4차 이벤트 기간 설정
	private Integer quizEndDate = 20190413;
	
	// 회차 정보
	private int getTrial() {
		return 4;
	}	
	
	@RequestMapping(value = "", method = RequestMethod.POST)
	public void createAdvancApplication(@Valid @ModelAttribute RenewalQuizDTO model,HttpSession session, Device device) throws Exception{
		//종료처리
		if(common.getNow() > quizEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		RenewalQuiz entity = modelMapper.map(model, RenewalQuiz.class);
		entity.setIpAddress(common.getIpAddress());
		entity.setChannel(device.isMobile() || device.isTablet() ? "mobile":"pc");
		entity.setTrial(getTrial());		
		Long userId = service.createRenewalQuiz(entity);
		
		session.setAttribute("renewal-quiz-id", userId);
	}
	@RequestMapping(value = "/sharing", method = RequestMethod.POST)
	public void updateQuizSharingCount(@Valid @ModelAttribute SharingTypeDto model,HttpSession session) throws Exception {
		//종료처리
		if(common.getNow() > quizEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		Long userId = (Long) session.getAttribute("renewal-quiz-id");
	
		if(userId == null || userId <0){
			throw new EventServiceException("400","응모정보가 존재하지 않습니다.새로고침 후 다시 시도해주세요.",null);
		}
		service.updateRenewalQuizSharingCount(userId,model.getSharingType());	
	}
}
