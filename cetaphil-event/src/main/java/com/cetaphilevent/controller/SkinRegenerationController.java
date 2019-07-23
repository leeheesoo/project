package com.cetaphilevent.controller;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mobile.device.Device;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.servlet.ModelAndView;

import com.cetaphilevent.repository.ITimeProvider;

@Controller
@RequestMapping("/return")
public class SkinRegenerationController {
	@Autowired
	private ITimeProvider timeProvider;

	//이벤트 기간 : 2018년 08월 20일 ~ 2018년 09월 26일
	private int endDate = 20180926;
	
	private Logger logger = LoggerFactory.getLogger(getClass());

	@RequestMapping("")
	public ModelAndView index(HttpSession session, Device device) {
		ModelAndView model = new ModelAndView(device.isNormal() ? "skinregeneration/sampling": "skinregeneration/mobile");
		int today = Integer.parseInt(timeProvider.now());

		//즉석당첨 팝업 제어 변수
		String instantLotteryPop = (String) session.getAttribute("ReturnNaverSearchingPop");
		model.addObject("instantLottery", false);
		if (instantLotteryPop != null && instantLotteryPop.equals("SHOW")) {
			model.addObject("instantLottery", true);
		}
		session.removeAttribute("ReturnNaverSearchingPop");
		
		// 구매하러 가기 링크가 날짜별 동적으로 변경되어야 함
		if (today >= 20180820 && today <= 20180826) {
			//위메트 - 마트위크
			model.addObject("buyDailyUrl", "http://www.wemakeprice.com/deal/adeal/3934515");
		} else if (today >= 20180827 && today <= 20180902) {
			//롯데닷컴 - 빅딜
			model.addObject("buyDailyUrl", "#20180902");
		} else if (today >= 20180903 && today <= 20180909) {
			// GS홈쇼핑 - 딜데이
			model.addObject("buyDailyUrl", "#20180909");
		} else if (today >= 20180910 && today <= 20180916) {
			// 11번가 - 쇼킹딜
			model.addObject("buyDailyUrl", "#20180916");
		} else if (today >= 20180917 && today <= 20180923) {
			// 티몬 - 단하루
			model.addObject("buyDailyUrl", "#20180923");
		} else if (today >= 20180924 && today <= 20180930) {
			// 네이버 - 쇼핑우먼
			model.addObject("buyDailyUrl", "#20180930");
		} else {
			model.addObject("buyDailyUrl", "#");
		}
 		
		//pc버전 8/21부터 오픈된다는 알럿노출을 위해 parameter 추가
		if (today >= 20180821) {
			model.addObject("isPcOpen", true);
		} else {
			model.addObject("isPcOpen", false);
		}
		return model;
	}

	
	// [즉석당첨 이벤트] 검색하기 버튼 클릭 (네이버로 랜딩)
	@RequestMapping(value = "naver-landing", method = RequestMethod.GET)
	public void naverLanding(HttpSession session, Device device, HttpServletResponse response) throws Exception {
		session.setAttribute("ReturnNaverSearchingStatus", "START");	
		if (Integer.parseInt(timeProvider.now()) > endDate) {	
			session.setAttribute("ReturnNaverSearchingStatus", "FAIL");	
		}
		
		if (device.isNormal()) {
			response.sendRedirect("http://www.naver.com/");
		} else {
			response.sendRedirect("http://m.naver.com/");
		}
	}

	// [즉석당첨 이벤트] 네이버에서 유입
	@RequestMapping(value = "naver-searching", method = RequestMethod.GET)
	public void naverSearching(HttpSession session, HttpServletResponse response, HttpServletRequest request) throws Exception {
		if (Integer.parseInt(timeProvider.now()) <= endDate) {
			session.setAttribute("ReturnNaverSearchingPop", "SHOW");
		}
		String redirectUrl = String.format("/return?%s", request.getQueryString());
		logger.debug(" redirectUrl: {}", redirectUrl);
		response.sendRedirect(redirectUrl);
	}

}
