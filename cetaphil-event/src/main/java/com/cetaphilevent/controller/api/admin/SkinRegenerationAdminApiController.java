package com.cetaphilevent.controller.api.admin;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;
import com.cetaphilevent.service.skinregeneration.SkinRegenerationNaverSearchingPrizeSettingService;

@RestController
@RequestMapping("manager/api/skin-regeneration")
@PreAuthorize("isAuthenticated()")
public class SkinRegenerationAdminApiController {
	private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private SkinRegenerationNaverSearchingPrizeSettingService service;
	
	@RequestMapping(value = "prize-setting", method = RequestMethod.GET)
	public List<SkinRegenerationNaverSearchingPrizeSetting> get() {
		return service.get();
	}
	
	@RequestMapping(value = "prize-setting", method = RequestMethod.POST)
	public ResponseModel update(@RequestBody SkinRegenerationNaverSearchingPrizeSetting entry) throws Exception {
		ResponseModel response = new ResponseModel();
		
		service.update(entry);
		
		response.setSuccess(true);
		response.setMessage("수정이 완료되었습니다");
		return response;
	}
}
