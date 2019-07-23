package com.cetaphilevent.controller.api.core;

import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.model.core.FandomDto;
import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.core.CoreFandomService;
import com.cetaphilevent.util.CommonProvider;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("api/core/fandom")
@Api(value = "quiz", description = "세타필 코어 리브랜딩 - 팬덤이벤트")
public class CoreFandomApiController {
	@Autowired
	private CoreFandomService service;
	
	@Autowired
	private ITimeProvider timeProvider;

	@Autowired
	private CommonProvider commonProvider;

	private ModelMapper modelMapper = new ModelMapper();
	private int endDate = 20190127; //2019.01.02 - 2019.01.27

	@RequestMapping(value = "", method = RequestMethod.POST)
	@ApiOperation(value = "")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel cteateEntry(@Valid @ModelAttribute FandomDto model, Device device) throws Exception {
		ResponseModel responsModel = new ResponseModel();
		
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			responsModel.setSuccess(false);
			responsModel.setMessage("응모 기간이 종료되었습니다");			
			return responsModel;
		}
		
		if (service.existUser(model.getMobile())) {
			responsModel.setSuccess(false);
			responsModel.setMessage("해당 이벤트는 중복참여가 불가능합니다.");
			return responsModel;
		}
		
		FandomDao entry = modelMapper.map(model, FandomDao.class);
		
		entry.setIpAddress(commonProvider.getIpAddress());
		entry.setChannel(device.isNormal() ? "pc" : "mobile");
		FandomDao userInfo = service.createEntry(entry);
		
		responsModel.setSuccess(true);
		responsModel.setMessage("success");
		
		return responsModel;
	}
}
