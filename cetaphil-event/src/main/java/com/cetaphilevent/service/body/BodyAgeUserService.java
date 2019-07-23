package com.cetaphilevent.service.body;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyAgeUserDao;
import com.cetaphilevent.util.EventServiceException;

@Service
public interface BodyAgeUserService {
	Long save(BodyAgeUserDao entry);	
	void update(Long userId, String snsType) throws EventServiceException;
}
