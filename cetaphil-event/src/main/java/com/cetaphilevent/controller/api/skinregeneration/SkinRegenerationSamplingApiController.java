package com.cetaphilevent.controller.api.skinregeneration;

import javax.servlet.http.HttpSession;
import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationSamplingSharingDto;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationSamplingUser;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationSamplingUserDto;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.skinregeneration.SkinRegenerationSamplingUserService;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("api/skin-regeneration/sampling")
@Api(value = "quiz", description = "#피부리턴 이벤트 : 샘플링 이벤트")
public class SkinRegenerationSamplingApiController {

	@Autowired
	private SkinRegenerationSamplingUserService service;

	@Autowired
	private ITimeProvider timeProvider;

	@Autowired
	private CommonProvider commonProvider;

	private ModelMapper modelMapper = new ModelMapper();

	// 2탄 이벤트 기간 : 2018년 09월 05일 ~ 2018년 09월 26일
	private int endDate = 20180926;

	@RequestMapping(value = "save-user", method = RequestMethod.POST)
	@ApiOperation(value = "[샘플링 이벤트] 개인정보 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel updateWinner(@Valid @ModelAttribute SkinRegenerationSamplingUserDto model, HttpSession session, Device device) throws Exception {
		ResponseModel response = new ResponseModel();

		if (Integer.parseInt(timeProvider.now()) > endDate) {
			// 이벤트 종료
			response.setSuccess(false);
			response.setMessage("응모 기간이 종료되었습니다");
		} else {
			/* 1탄 종료 후 2탄에서는 선착순 참여 없고 중복참여 허용되도록 설정 */
//			// 선착순 참여 - 4950명
//			if (service.totalCount() >= 4950) {
//				response.setSuccess(false);
//				response.setMessage("아쉽게도 피부리턴 키트가 전부 소진되어 이벤트가 조기 종료 되었습니다\r\n아래 다양한 이벤트가 준비되어 있으니 많은 참여 부탁 드립니다^^");
//				return response;
//			}			
//			// 중복참여 X - 연락처
//			if (service.existUser(model.getPhone())) {
//				response.setSuccess(false);
//				response.setMessage("피부 리턴 키트 100% 증정 이벤트는 한 번만 참여 가능합니다");
//				return response;
//			}
			
			SkinRegenerationSamplingUser entry = modelMapper.map(model, SkinRegenerationSamplingUser.class);
			entry.setChannel(device.isNormal() ? "pc" : "mobile");
			entry.setIpAddress(commonProvider.getIpAddress());
			Long userId = service.save(entry);
			session.setAttribute("ReturnQuizUserId", userId);

			response.setSuccess(true);
			response.setMessage("개인정보가 저장되었습니다");
		}
		return response;
	}

	@ApiOperation(value = "[샘플링 이벤트] SNS 공유")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	@RequestMapping(value = "sharing", method = RequestMethod.POST)
	public ResponseModel sharing(@ModelAttribute @Valid SkinRegenerationSamplingSharingDto model, HttpSession session) throws Exception {
		ResponseModel response = new ResponseModel();

		if (Integer.parseInt(timeProvider.now()) > endDate) {
			// 이벤트 종료
			response.setSuccess(false);
			response.setMessage("응모 기간이 종료되었습니다");
		} else {
			Long userId = (Long) session.getAttribute("ReturnQuizUserId");
			if (userId == null) {
				throw new EventServiceException("400", "응모된 데이터를 찾을 수 없습니다. 새로고침 후 다시 참여해주세요", null);
			}
			service.updateBySharingCount(userId, model.getSnsType());

			response.setSuccess(true);
			response.setMessage("공유한 데이터가 정상적으로 저장되었습니다");
		}
		return response;
	}
}
