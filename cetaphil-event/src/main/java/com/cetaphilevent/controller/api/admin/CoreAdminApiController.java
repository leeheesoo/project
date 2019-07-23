package com.cetaphilevent.controller.api.admin;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.sql.Date;
import java.util.List;

import org.modelmapper.ModelMapper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSetting;
import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSettingDto;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.service.core.CoreNaverSearchingPrizeSettingService;

@RestController
@RequestMapping("manager/api/core")
@PreAuthorize("isAuthenticated()")
public class CoreAdminApiController {
	private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private CoreNaverSearchingPrizeSettingService service;
	private ModelMapper modelMapper = new ModelMapper();
	
	@RequestMapping(value = "prize-setting", method = RequestMethod.GET)
	public List<CoreNaverSearchingPrizeSetting> get() {
		return service.get();
	}
	
	@RequestMapping(value = "prize-setting/update", method = RequestMethod.POST)
	public ResponseModel update(@RequestBody CoreNaverSearchingPrizeSetting entry) throws Exception {
		ResponseModel response = new ResponseModel();
		
		service.update(entry);
		
		response.setSuccess(true);
		response.setMessage("수정이 완료되었습니다");
		return response;
	}
	
	@RequestMapping(value = "prize-setting", method = RequestMethod.POST)
	public ResponseModel insert(@RequestBody CoreNaverSearchingPrizeSettingDto model) throws Exception {
		ResponseModel response = new ResponseModel();
		
		
		CoreNaverSearchingPrizeSetting entry = modelMapper.map(model, CoreNaverSearchingPrizeSetting.class);
		logger.debug("year :: {}", model.getKey().getDate().getYear());
		logger.debug("month :: {}", model.getKey().getDate().getMonth() + 1);
		logger.debug("date :: {}", model.getKey().getDate().getDate());
		logger.debug("key :::: {}",model.getKey());
		
		Date prizeSettingDate = Date.valueOf(LocalDate.of(model.getKey().getDate().getYear() + 1900, model.getKey().getDate().getMonth() + 1, model.getKey().getDate().getDate()).format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));

		entry.getKey().setDate(prizeSettingDate);
		service.insert(entry);
		
		response.setSuccess(true);
		response.setMessage("등록되었습니다.");
		return response;
	}
}
