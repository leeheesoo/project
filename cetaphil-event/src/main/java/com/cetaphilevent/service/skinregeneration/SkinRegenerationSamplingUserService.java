package com.cetaphilevent.service.skinregeneration;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationSamplingUser;

@Service
public interface SkinRegenerationSamplingUserService {
	Long save(SkinRegenerationSamplingUser entry);
	
	long totalCount();
	
	boolean existUser(String phone);
	
	void updateBySharingCount(Long userId, String snsType) throws Exception;
}
