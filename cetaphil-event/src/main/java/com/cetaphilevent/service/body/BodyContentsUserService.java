package com.cetaphilevent.service.body;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyContentsUserDao;
import com.cetaphilevent.util.EventServiceException;

@Service
public interface BodyContentsUserService {
	Long save(BodyContentsUserDao entry);	
	void update(Long userId, String snsType) throws EventServiceException;
}
