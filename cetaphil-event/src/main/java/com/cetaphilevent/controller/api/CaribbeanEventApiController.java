package com.cetaphilevent.controller.api;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

import javax.servlet.http.HttpSession;
import javax.validation.Valid;

import org.modelmapper.ModelMapper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.caribbean.CaribbeanEntry;
import com.cetaphilevent.model.caribbean.CaribbeanEntryDto;
import com.cetaphilevent.model.caribbean.CaribbeanListDto;
import com.cetaphilevent.model.response.ResponseExceptionModel;
import com.cetaphilevent.model.response.ResponseModel;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.caribbean.CaribbeanService;
import com.cetaphilevent.util.CommonProvider;

import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import io.swagger.annotations.ApiResponse;
import io.swagger.annotations.ApiResponses;

@RestController
@RequestMapping("api/caribbean")
@Api(value = "caribbean", description = "캐리비안베이 이벤트")
public class CaribbeanEventApiController {
	
	private Logger logger = LoggerFactory.getLogger(getClass());

	@Autowired
	private CaribbeanService service;
	@Autowired
	private CommonProvider common;
	@Autowired
	private ITimeProvider timeProvider;
	
	private ModelMapper modelMapper = new ModelMapper();

	@RequestMapping(value="", method=RequestMethod.POST)
	@ApiOperation(value="post, 기대평작성")
	@ApiResponses(value = {
			@ApiResponse(code = 200, message = "OK", response = ResponseModel.class),
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
            @ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class)})
	public ResponseModel cteateEntry(@Valid @ModelAttribute CaribbeanEntryDto model, HttpSession session) {
		ResponseModel responsModel = new ResponseModel();
		
		if(Integer.parseInt(timeProvider.now()) > 20180726) {
			responsModel.setSuccess(false);
			responsModel.setMessage("응모 기간이 종료되었습니다");			
			return responsModel;
		}
		
		CaribbeanEntry entity = modelMapper.map(model, CaribbeanEntry.class);
		entity.setIpAddress(common.getIpAddress());
		
		CaribbeanEntry userInfo = service.createEntry(entity);
		
		session.setAttribute("userId", userInfo.getId());
		responsModel.setSuccess(true);
		responsModel.setMessage("success");
		
		return responsModel;
	}
	
	@RequestMapping(value="/sharing", method=RequestMethod.POST)
	@ApiOperation(value="post, sns공유 카운트 업데이트")
	@ApiResponses(value = {
			@ApiResponse(code = 200, message = "OK", response = ResponseModel.class),
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
            @ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class)})
	public ResponseModel updateEntry(HttpSession session, @ModelAttribute CaribbeanEntryDto model) {
		ResponseModel responsModel = new ResponseModel();
		
		if(Integer.parseInt(timeProvider.now()) > 20180726) {
			responsModel.setSuccess(false);
			responsModel.setMessage("응모 기간이 종료되었습니다");			
			return responsModel;
		}
		
		Long userId = (Long) session.getAttribute("userId");
		
		CaribbeanEntry user  = service.getEntryById(userId);
		if(model.getSnsType().equals("facebook")) {
			user.setShareFacebookCount(user.getShareFacebookCount() + 1);
		} else {
			user.setShareKaKaoCount(user.getShareKaKaoCount() + 1);
		}
		
		service.createEntry(user);
		
		responsModel.setSuccess(true);
		responsModel.setMessage("success");
		
		return responsModel;
	}
	
	@RequestMapping(value="", method=RequestMethod.GET)
	@ApiOperation(value="get, 기대평 이벤트 참여목록")
	@ApiResponses(value = {
			@ApiResponse(code = 200, message = "OK", response = CaribbeanEntry.class, responseContainer = "List" ),
			@ApiResponse(code = 400, message = "BAD_REQUEST", response = ResponseExceptionModel.class),
            @ApiResponse(code = 500, message = "오류가 발생했습니다. 잠시 후 다시 시도해주세요.", response = ResponseExceptionModel.class)})
	public List<CaribbeanListDto> getEventList() {
		List<CaribbeanEntry> list = service.getEventList();
		
		List<CaribbeanListDto> returnList = new ArrayList<CaribbeanListDto>();
		for(int i=0; i< list.size(); i++) {
			CaribbeanListDto model = new CaribbeanListDto();
			model.setName(maskingName(list.get(i).getName()));
			model.setContent(list.get(i).getContent());
			returnList.add(model);
		}
		
		return returnList;
	}
	
	public String maskingName(String name) {
        String mName = "";        
        
        boolean isKorean = Pattern.matches("^[ㄱ-ㅎ가-힣]*$", name);
        if(isKorean) {    //한글 이름 (성 뒤 이름의 첫자리 마스킹)
            mName = name.replace(name.toCharArray()[1], '*');
        } else {    //영문 이름 (앞 뒤 2자리 제외 마스킹)
            if(name.length() == 0 || name == null) {
                mName = "";
            }else if(name.length() == 1) {
                mName = "*";
            }else{
                //replace를 할 경우 같은글자가 모두 * 마스킹되는 현상이 있다.
                char[] cName = name.toCharArray();
                cName[1] = '*';
                mName = String.copyValueOf(cName);
            }
        }
        return mName;
    }
}
