package com.babience.survey.controller.api;


import javax.servlet.http.HttpSession;
import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.babience.survey.dao.RenewalAdvanceApplication;
import com.babience.survey.dao.RenewalExperience;
import com.babience.survey.dao.RenewalReview;
import com.babience.survey.dto.RenewalAdvanceApplicationDTO;
import com.babience.survey.dto.RenewalExperienceDTO;
import com.babience.survey.dto.RenewalReviewDTO;
import com.babience.survey.service.RenewalServeyService;
import com.babience.utils.CommonProvider;
import com.babience.utils.SharingTypeDto;
import com.babience.utils.exception.EventServiceException;

@RestController
@RequestMapping("api/kabrita-renewal")
public class RenewalServeyApiController {
	private Logger logger = LoggerFactory.getLogger(getClass());
	@Autowired
	private RenewalServeyService service;
	@Autowired
	private CommonProvider common;
	
	private ModelMapper modelMapper = new ModelMapper();
	
	//4차 이벤트 기간 설정
	private Integer surveyEndDate = 20190331;
	private Integer reviewEndDate = 20190414;
	
	// 회차 정보
	private int getTrial() {
		return 4;
	}
	
	@RequestMapping(value = "/advance-application", method = RequestMethod.POST)
	public void createAdvancApplication(@Valid @ModelAttribute RenewalAdvanceApplicationDTO model,HttpSession session, Device device) throws Exception{
		//종료처리
		if(common.getNow() > surveyEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		if(service.checkEventEntry(model.getMobile(), getTrial())) {
			throw new EventServiceException("400","이미 응모하셨습니다.",model.getMobile());
		}
		
		RenewalAdvanceApplication entity = modelMapper.map(model, RenewalAdvanceApplication.class);
		entity.setIpAddress(common.getIpAddress());
		entity.setChannel(device.isMobile() || device.isTablet() ? "mobile":"pc");
		entity.setTrial(getTrial());		
		Long userId = service.createRenewalAdvanceApplication(entity);
		
		session.setAttribute("survey-advance-application-id", userId);
	}
	
	@RequestMapping(value = "/experience", method = RequestMethod.POST)
	public void createExperience(@Valid @ModelAttribute RenewalExperienceDTO model,HttpSession session, Device device) throws Exception {
		//종료처리
		if(common.getNow() > surveyEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		if(service.checkEventEntry(model.getMobile(), getTrial())) {
			throw new EventServiceException("400","이미 응모하셨습니다.",model.getMobile());
		}
		
		RenewalExperience entity = modelMapper.map(model, RenewalExperience.class);
		entity.setIpAddress(common.getIpAddress());
		entity.setChannel(device.isMobile() || device.isTablet() ? "mobile":"pc");
		entity.setTrial(getTrial());
		Long userId = service.createRenewalExperience(entity);
		
		session.setAttribute("renewal-experience-id", userId);
	}
	
	@RequestMapping(value = "/review", method = RequestMethod.POST)
	public void createReview(@Valid @ModelAttribute RenewalReviewDTO model, Device device) throws Exception{	
		//종료처리
		if(common.getNow() > reviewEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		RenewalReview entity = modelMapper.map(model, RenewalReview.class);
		entity.setIpAddress(common.getIpAddress());
		entity.setChannel(device.isMobile() || device.isTablet() ? "mobile":"pc");
		entity.setTrial(getTrial());
		service.createRenewalReview(entity);
	}
	
	@RequestMapping(value = "/advance-application/sharing", method = RequestMethod.POST)
	public void updateAdvanceApplicationSharingCount(@Valid @ModelAttribute SharingTypeDto model,HttpSession session) throws Exception {
		//종료처리
		if(common.getNow() > surveyEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		Long userId = (Long) session.getAttribute("survey-advance-application-id");
	
		if(userId == null || userId <0){
			throw new EventServiceException("400","응모정보가 존재하지 않습니다.새로고침 후 다시 시도해주세요.",null);
		}
		service.updateRenewalAdvanceApplicationSharingCount(userId,model.getSharingType());	
	}
	
	@RequestMapping(value = "/experience/sharing", method = RequestMethod.POST)
	public void updateExperienceSharingCount(@Valid @ModelAttribute SharingTypeDto model,HttpSession session) throws Exception {
		//종료처리
		if(common.getNow() > surveyEndDate) {
			throw new EventServiceException("400","이벤트 기간에 신청해주세요. ^^",null);
		}
		Long userId = (Long) session.getAttribute("renewal-experience-id");
	
		if(userId == null || userId <0){
			throw new EventServiceException("400","응모정보가 존재하지 않습니다.새로고침 후 다시 시도해주세요.",null);
		}
		service.updateRenewalExperienceSharingCount(userId,model.getSharingType());
	}
	
}
