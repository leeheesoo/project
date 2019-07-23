package com.cetaphilevent.controller.api.skinregeneration;

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

import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeDto;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeType;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingUser;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingUserDto;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.skinregeneration.SkinRegenerationNaverSearchingUserService;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("api/skin-regeneration/instant-lottery")
@Api(value = "instant-lottery", description = "#피부리턴 이벤트 : 네이버 검색 즉석당첨 이벤트")
public class SkinRegenerationNaverSearchingApiController {
	private Logger logger = LoggerFactory.getLogger(getClass());

	@Autowired
	private SkinRegenerationNaverSearchingUserService userService;

	@Autowired
	private ITimeProvider timeProvider;

	@Autowired
	private CommonProvider commonProvider;

	private ModelMapper modelMapper = new ModelMapper();

	//이벤트 기간 : 2018년 08월 20일 ~ 2018년 09월 26일
	private int endDate = 20180926;

	@RequestMapping(value = "", method = RequestMethod.POST)
	@ApiOperation(value = "[즉석 당첨 경품 정보 추츨] View에서 instant-lottery Model Object 값이 true 일 경우 해당 API를 호출해 주세요")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = SkinRegenerationNaverSearchingPrizeDto.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public SkinRegenerationNaverSearchingPrizeDto instantLottery(HttpSession session, Device device) throws Exception {
		SkinRegenerationNaverSearchingPrizeDto response = new SkinRegenerationNaverSearchingPrizeDto();

		if (Integer.parseInt(timeProvider.now()) > endDate) {
			// 이벤트 종료
			response.setSuccess(false);
			response.setMessage("응모 기간이 종료되었습니다");
		} else {
			// [즉석당첨 이벤트] 경품 로직	
			String status = (String) session.getAttribute("ReturnNaverSearchingStatus");	
			boolean isChecked = false;	
 			if (status != null && status.equals("START")) {	
				isChecked = true;	
			}	
			session.removeAttribute("ReturnNaverSearchingStatus");
						
			// DB 저장
			SkinRegenerationNaverSearchingUser entry = new SkinRegenerationNaverSearchingUser();
			entry.setChannel(device.isNormal() ? "pc" : "mobile");
			entry.setPrizeType(SkinRegenerationNaverSearchingPrizeType.LosingTicket);
			entry.setIpAddress(commonProvider.getIpAddress());
			SkinRegenerationNaverSearchingUser user = userService.save(entry, isChecked);

			// 당첨자 일경우 경품 정보 세션 저장
			if (user.getPrizeType() != SkinRegenerationNaverSearchingPrizeType.LosingTicket) {
				session.setAttribute("ReturnNaverSearchingUserId", user.getId());
			}

			response.setPrizeType(user.getPrizeType());
			response.setSuccess(true);
			response.setMessage("즉석당첨 결과를 노출해주세요~");
		}
		return response;
	}

	@RequestMapping(value = "update-winner", method = RequestMethod.POST)
	@ApiOperation(value = "[즉석당첨 이벤트] 개인정보 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel updateWinner(@Valid @ModelAttribute SkinRegenerationNaverSearchingUserDto model, HttpSession session) throws Exception {
		ResponseModel response = new ResponseModel();

		if (Integer.parseInt(timeProvider.now()) > endDate) {
			// 이벤트 종료
			response.setSuccess(false);
			response.setMessage("응모 기간이 종료되었습니다");
		} else {
			
			Long userId = (Long) session.getAttribute("ReturnNaverSearchingUserId");
			if (userId == null) {
				throw new EventServiceException("400", "당첨자가 아닙니다", null);
			}
			SkinRegenerationNaverSearchingUser user = userService.getWinnerById(userId);
			if (user.getPrizeType() != model.getPrizeType()) {
				throw new EventServiceException("400", "해당 경품 당첨자가 아닙니다", null);
			}
			modelMapper.map(model, user);
			userService.update(user);

			// 세션 정리
			session.removeAttribute("ReturnNaverSearchingUserId");

			response.setSuccess(true);
			response.setMessage("개인정보가 저장되었습니다");
		}
		return response;
	}
}
