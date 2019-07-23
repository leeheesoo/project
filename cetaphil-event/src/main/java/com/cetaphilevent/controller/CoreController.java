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
@RequestMapping("/core")
public class CoreController {
	@Autowired
	private ITimeProvider timeProvider;
	private Logger logger = LoggerFactory.getLogger(getClass());
	private int endDate = 20190203;
	
	@RequestMapping("")
	public ModelAndView index(HttpSession session, Device device, HttpServletRequest request) {
		ModelAndView model = new ModelAndView(device.isNormal() ? "core/index": "core/mobile");

		//ogurl
		model.addObject("ogurl", "https://cetaphil.pentacle.co.kr");
		String queryString = request.getQueryString();
		if (queryString != null && !queryString.isEmpty()) {
			if (request.getQueryString().indexOf("&utm_source=facebook") > -1) {
				model.addObject("ogurl", "https://cetaphil.pentacle.co.kr/core?utm_campaign=core&utm_medium=sharing&utm_source=facebook");
			} else if (request.getQueryString().indexOf("&utm_source=kakaostory") > -1) {
				model.addObject("ogurl", "https://cetaphil.pentacle.co.kr/core?utm_campaign=core&utm_medium=sharing&utm_source=kakaostory");
			}
		}
		
		//즉석당첨 팝업 제어 변수
		String prizePopOpen = (String) session.getAttribute("CoreNaverSearchingPop");
		model.addObject("prizePopOpen", false);
		if (prizePopOpen != null && prizePopOpen.equals("SHOW")) {
			model.addObject("prizePopOpen", true);
		}
		session.removeAttribute("CoreNaverSearchingPop");
		return model;
	}
	
	@RequestMapping(value = "naver-landing", method = RequestMethod.GET)
	public void naverLanding(HttpSession session, Device device, HttpServletResponse response) throws Exception {
		session.setAttribute("CoreNaverSearchingStatus", "START");	
		if (Integer.parseInt(timeProvider.now()) > endDate) {	
			session.setAttribute("CoreNaverSearchingStatus", "FAIL");	
		}
		
		if (device.isNormal()) {
			response.sendRedirect("http://www.naver.com/");
		} else {
			response.sendRedirect("http://m.naver.com/");
		}
	}

	@RequestMapping(value = "naver-searching", method = RequestMethod.GET)
	public void naverSearching(HttpSession session, HttpServletResponse response, HttpServletRequest request) throws Exception {
		if (Integer.parseInt(timeProvider.now()) <= endDate) {
			session.setAttribute("CoreNaverSearchingPop", "SHOW");
		}
		String redirectUrl = String.format("/core");
		response.sendRedirect(redirectUrl);
	}
}
