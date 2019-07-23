package com.cetaphilevent.controller.api.body;

import javax.servlet.http.HttpSession;
import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.body.BodyAgeUserDto;
import com.cetaphilevent.model.body.BodyCalculatorUserDto;
import com.cetaphilevent.model.body.BodyChallengeUserDto;
import com.cetaphilevent.model.body.BodySharingDto;
import com.cetaphilevent.model.body.BodyValidUserDto;
import com.cetaphilevent.model.body.dao.BodyAgeUserDao;
import com.cetaphilevent.model.body.dao.BodyCalculatorUserDao;
import com.cetaphilevent.model.body.dao.BodyChallengeUserDao;
import com.cetaphilevent.model.body.dao.BodyContentsUserDao;
import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.body.BodyAgeUserService;
import com.cetaphilevent.service.body.BodyCalculatorUserService;
import com.cetaphilevent.service.body.BodyChallengeUserService;
import com.cetaphilevent.service.body.BodyContentsUserService;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("/api/body")
@Api(value = "body", description = "#동안바디 이벤트")
public class BodyApiController {
	
	@Autowired
	private BodyCalculatorUserService calculatorService;	
	@Autowired
	private BodyAgeUserService ageService;
	@Autowired
	private BodyChallengeUserService challengeService;
	@Autowired
	private BodyContentsUserService contentsService;
	@Autowired
	private CommonProvider commonProvider;
	@Autowired
	private ITimeProvider timeProvider;
	
	private ModelMapper modelMapper = new ModelMapper();
	
	private String getChannel(Device device) {
		return device.isNormal() ? "pc" : "mobile";
	}
	
	private int endDate = 20181130;

	@RequestMapping(value = "/valid-user", method = RequestMethod.POST)
	@ApiOperation(value = "[#동안바디 이벤트] 개인정보 유효성 검사")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel ValidUser(@Valid @ModelAttribute BodyValidUserDto model) {
		ResponseModel response = new ResponseModel();
		//이벤트 종료 처리
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("이벤트가 종료되었습니다.");
			return response;
		}
		
		response.setSuccess(true);
		response.setMessage("개인정보 유효성 검사가 완료되었습니다.");

		return response;
	}

	@RequestMapping(value = "/calculator-save", method = RequestMethod.POST)
	@ApiOperation(value = "[#동안바디 이벤트] 동안바디 계산기 응모 데이터 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel CalculatorSave(@Valid @ModelAttribute BodyCalculatorUserDto model, Device device, HttpSession session) {
		ResponseModel response = new ResponseModel();
		//이벤트 종료 처리
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("이벤트가 종료되었습니다.");
			return response;
		}
		
		BodyCalculatorUserDao entry = modelMapper.map(model, BodyCalculatorUserDao.class);
		entry.setChannel(getChannel(device));
		entry.setIpAddress(commonProvider.getIpAddress());
		Long userId = calculatorService.save(entry);		
		session.setAttribute("body-calculator-userId", userId);
		
		response.setSuccess(true);
		response.setMessage("동안바디 계산기 응모 데이터가 저장되었습니다");

		return response;
	}

	@RequestMapping(value = "/age-save", method = RequestMethod.POST)
	@ApiOperation(value = "[#동안바디 이벤트] 동안바디 측정기 응모 데이터 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel AgeSave(@Valid @ModelAttribute BodyAgeUserDto model, Device device, HttpSession session) {
		ResponseModel response = new ResponseModel();
		//이벤트 종료 처리
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("이벤트가 종료되었습니다.");
			return response;
		}
		
		BodyAgeUserDao entry = modelMapper.map(model, BodyAgeUserDao.class);
		entry.setChannel(getChannel(device));
		entry.setIpAddress(commonProvider.getIpAddress());
		Long userId = ageService.save(entry);
		session.setAttribute("body-age-userId", userId);
		
		response.setSuccess(true);
		response.setMessage("동안바디 측정기 응모 데이터가 저장되었습니다");

		return response;
	}

	@RequestMapping(value = "/challenge-save", method = RequestMethod.POST)
	@ApiOperation(value = "[#동안바디 이벤트] 에브리바디 동안바디 챌린지 응모 데이터 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel ChallengeSave(@Valid @ModelAttribute BodyChallengeUserDto model, Device device, HttpSession session) {
		ResponseModel response = new ResponseModel();
		//이벤트 종료 처리
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("이벤트가 종료되었습니다.");
			return response;
		}
		
		BodyChallengeUserDao entry = modelMapper.map(model, BodyChallengeUserDao.class);
		entry.setChannel(getChannel(device));
		entry.setIpAddress(commonProvider.getIpAddress());
		Long userId = challengeService.save(entry);
		session.setAttribute("body-challenge-userId", userId);
		
		response.setSuccess(true);
		response.setMessage("에브리바디 동안바디 챌린지 응모 데이터가 저장되었습니다");

		return response;
	}
	
	@RequestMapping(value = "/contents-save", method = RequestMethod.POST)
	@ApiOperation(value = "[#동안바디 이벤트] 동안바디 공유이벤트 응모 데이터 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel ContentsSave(@Valid @ModelAttribute BodyValidUserDto model, Device device, HttpSession session) {
		ResponseModel response = new ResponseModel();
		//이벤트 종료 처리
		if(Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("이벤트가 종료되었습니다.");
			return response;
		}
		
		BodyContentsUserDao entry = modelMapper.map(model, BodyContentsUserDao.class);
		entry.setChannel(getChannel(device));
		entry.setIpAddress(commonProvider.getIpAddress());
		Long userId = contentsService.save(entry);
		session.setAttribute("body-contents-userId", userId);
		
		response.setSuccess(true);
		response.setMessage("동안바디 공유 이벤트 응모 데이터가 저장되었습니다");
		
		return response;
	}
	
	@RequestMapping(value = "/sharing-save", method = RequestMethod.POST)
	@ApiOperation(value = "[#동안바디 이벤트] 동안바디 SNS 공유 데이터 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel SharingSave(@Valid @ModelAttribute BodySharingDto model, HttpSession session) throws Exception {
		ResponseModel response = new ResponseModel();
		
		Long userId = (Long)session.getAttribute("body-" + model.getEventType() + "-userId");
		if (userId == null) {
			throw new EventServiceException("400", "응모된 이벤트 정보를 찾을 수 없습니다.", null);
		}
		switch(model.getEventType()) {
			case "calculator":
				calculatorService.update(userId, model.getSnsType());
				break;
			case "age":
				ageService.update(userId, model.getSnsType());
				break;
			case "challenge":
				challengeService.update(userId, model.getSnsType());
				break;
			case "contents":
				contentsService.update(userId, model.getSnsType());
				break;
			default:
				throw new EventServiceException("400", "이벤트명을 확인해주세요", null);
		}
		response.setSuccess(true);
		response.setMessage("SNS 공유 데이터가 저장되었습니다");
		
		return response;
	}
}
