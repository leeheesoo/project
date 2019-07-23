package com.cetaphilevent.controller.api.core;

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

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeDto;
import com.cetaphilevent.model.core.CoreNaverSearchingPrizeType;
import com.cetaphilevent.model.core.CoreNaverSearchingUser;
import com.cetaphilevent.model.core.CoreNaverSearchingUserDto;
import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeDto;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.core.CoreNaverSearchingUserService;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("api/core/instant-lottery")
@Api(value = "quiz", description = "세타필 코어 리브랜딩 - sns공유 이벤트")
public class CoreNaverSearchingApiController {
	@Autowired
	private CoreNaverSearchingUserService service; 
	
	@Autowired
	private ITimeProvider timeProvider;

	@Autowired
	private CommonProvider commonProvider;

	private Logger logger = LoggerFactory.getLogger(getClass());
	private ModelMapper modelMapper = new ModelMapper();
	private int endDate = 20190203; //2019.01.09 - 2019.02.03
	
	@RequestMapping(value = "", method = RequestMethod.POST)
	@ApiOperation(value = "[즉석 당첨 경품 정보 추츨] View에서 instant-lottery Model Object 값이 true 일 경우 해당 API를 호출해 주세요")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = SkinRegenerationNaverSearchingPrizeDto.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public CoreNaverSearchingPrizeDto instantLottery(HttpSession session, Device device) throws Exception {
		CoreNaverSearchingPrizeDto response = new CoreNaverSearchingPrizeDto();

		if (Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("응모 기간이 종료되었습니다");
		} else {
			// 경품 로직	
			String status = (String) session.getAttribute("CoreNaverSearchingStatus");	
			logger.debug("session :: {}", status);
			boolean isChecked = false;	 //세션유무 확인
 			 if(status != null && status.equals("START")) {	
				isChecked = true;
			}	
			session.removeAttribute("CoreNaverSearchingStatus");

		
			CoreNaverSearchingUser entry = new CoreNaverSearchingUser();
			entry.setChannel(device.isNormal() ? "pc" : "mobile");
			entry.setPrizeType(CoreNaverSearchingPrizeType.LosingTicket);
			entry.setIpAddress(commonProvider.getIpAddress());
			CoreNaverSearchingUser user = service.save(entry, isChecked);

			// 당첨자 일경우 세션에 당첨 id 저장
			if (user.getPrizeType() != CoreNaverSearchingPrizeType.LosingTicket) {
				session.setAttribute("CoreNaverSearchingUserId", user.getId());
			}

			response.setPrizeType(user.getPrizeType());
			response.setSuccess(true);
			response.setMessage("즉석당첨 결과를 노출해주세요~");
		} 
		return response;
	}
	
	@RequestMapping(value = "update-winner", method = RequestMethod.POST)
	@ApiOperation(value = "즉석당첨 이벤트 개인정보 저장")
	@ApiResponses(value = { @ApiResponse(code = 200, message = "OK", response = ResponseModel.class), 
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
			@ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class) })
	public ResponseModel updateWinner(@Valid @ModelAttribute CoreNaverSearchingUserDto model, HttpSession session) throws Exception {
		ResponseModel response = new ResponseModel();

		// 이벤트 종료
		if (Integer.parseInt(timeProvider.now()) > endDate) {
			response.setSuccess(false);
			response.setMessage("응모 기간이 종료되었습니다");
		} else {
			
			Long userId = (Long) session.getAttribute("CoreNaverSearchingUserId");
			if (userId == null) {
				throw new EventServiceException("400", "당첨자가 아닙니다", null);
			}
			CoreNaverSearchingUser user = service.getWinnerById(userId);
			if (user.getPrizeType() != model.getPrizeType()) {
				throw new EventServiceException("400", "해당 경품 당첨자가 아닙니다", null);
			}
			//modelMapper.map(model, user);
			service.update(model, user);

			session.removeAttribute("CoreNaverSearchingUserId");

			response.setSuccess(true);
			response.setMessage("개인정보가 저장되었습니다");
		}
		return response;
	}
}
