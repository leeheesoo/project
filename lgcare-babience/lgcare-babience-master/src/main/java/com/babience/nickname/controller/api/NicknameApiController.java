package com.babience.nickname.controller.api;

import java.lang.reflect.Type;
import java.util.List;

import org.modelmapper.ModelMapper;
import org.modelmapper.TypeToken;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.babience.nickname.service.KabritaNicknameUserService;
import com.babience.nickname.vo.KabritaNicknameUserListDto;

@RestController
@RequestMapping("api/kabrita/nickname")
public class NicknameApiController {
	
	private Logger logger = LoggerFactory.getLogger(getClass());

	@Autowired
	private KabritaNicknameUserService service;
	
	private ModelMapper modelMapper = new ModelMapper();
		
	@RequestMapping(value = "list", method = RequestMethod.GET)
	public List<KabritaNicknameUserListDto> get() {
		Type listType = new TypeToken<List<KabritaNicknameUserListDto>>() {}.getType();
		return modelMapper.map(service.getUserList(), listType);
	}
}
