package com.cetaphilevent.util;

import javax.servlet.http.HttpServletRequest;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

@Component("CommonProvider")
public class CommonProviderImpl implements CommonProvider {
	private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Override
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
	
}