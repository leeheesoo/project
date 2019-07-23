package com.cetaphilevent.service.body;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyChallengeUserDao;
import com.cetaphilevent.util.EventServiceException;

@Service
public interface BodyChallengeUserService {
	Long save(BodyChallengeUserDao entry);	
	void update(Long userId, String snsType) throws EventServiceException;
}
