package com.babience.utils;

import javax.servlet.http.HttpServletRequest;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

@Component
public class CommonProvider {
	private Logger logger = LoggerFactory.getLogger(getClass());
	@Autowired
	private ITimeProvider timeProvider;
	
	public String getIpAddress() {
		try {
			HttpServletRequest req = ((ServletRequestAttributes)RequestContextHolder.currentRequestAttributes()).getRequest();

	        String ip = req.getHeader("X-FORWARDED-FOR");
	        if (ip == null) {
	        	ip = req.getRemoteAddr();
	        }
	    	return ip;	
		}catch(Exception e) {			
			logger.debug(">>>>>>>>>>> ipAddress Exception: {}", e.getMessage());
			return "";
		}
	}
	
	public Integer getNow() {
		return Integer.parseInt(timeProvider.now());
	}
}
