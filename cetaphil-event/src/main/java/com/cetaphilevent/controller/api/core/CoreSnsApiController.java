package com.cetaphilevent.controller.api.core;

import javax.servlet.http.HttpSession;
import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.core.CoreSharginDto;
import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.model.core.CoreSnsDto;
import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.core.CoreSnsService;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("api/core/sns")
@Api(value = "quiz", description = "세타필 코어 리브랜딩 - 네이버 검색 즉석당첨")
public class CoreSnsApiController {
	@Autowired
	private CoreSnsService service;
	
	@Autowired
	private ITimeProvider timeProvider;

	@Autowired
	private CommonProvider commonProvider;

	private ModelMapper modelMapper = new ModelMapper();
	
	private int endDate = 20190108; //2019.01.02 - 2019.01.08

	
	@RequestMapping(value = "", method = RequestMethod.POST)
	@ApiOperation(value = "")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel cteateFandomSnsEntry(@Valid @ModelAttribute CoreSnsDto model, Device device, HttpSession session) {
		ResponseModel responsModel = new ResponseModel();
		
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			responsModel.setSuccess(false);
			responsModel.setMessage("응모 기간이 종료되었습니다");			
			return responsModel;
		}
		
//		if (service.existUser(model.getMobile())) {
//			responsModel.setSuccess(false);
//			responsModel.setMessage("해당 이벤트는 중복참여가 불가능합니다.");
//			return responsModel;
//		}
		
		CoreSnsDao entry = modelMapper.map(model, CoreSnsDao.class);
		entry.setChannel(device.isNormal() ? "pc" : "mobile");
		entry.setIpAddress(commonProvider.getIpAddress());
		Long userId = service.fandomSnsUserSave(entry);		
		session.setAttribute("fandom-sns-user", userId);
		
		responsModel.setSuccess(true);
		responsModel.setMessage("success");
		
		return responsModel;
	}
	
	@RequestMapping(value = "/sharing", method = RequestMethod.POST)
	@ApiOperation(value = "")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel updateSnsCounter(@Valid @ModelAttribute CoreSharginDto model, Device device, HttpSession session) throws EventServiceException {
		ResponseModel response = new ResponseModel();
		
		Long userId = (Long)session.getAttribute("fandom-sns-user");
		if (userId == null) {
			throw new EventServiceException("400", "응모된 이벤트 정보를 찾을 수 없습니다.", null);
		}
		
		service.snsCounterUpdate(userId, model.getSnsType());
		
		response.setSuccess(true);
		response.setMessage("SNS 공유이벤트 참여가 완료되었습니다.");
		
		return response;
	}
}
