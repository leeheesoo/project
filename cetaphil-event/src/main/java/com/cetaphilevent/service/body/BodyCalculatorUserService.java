package com.cetaphilevent.service.body;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyCalculatorUserDao;
import com.cetaphilevent.util.EventServiceException;

@Service
public interface BodyCalculatorUserService {
	Long save(BodyCalculatorUserDao entry);
	void update(Long userId, String snsType) throws EventServiceException;
}
