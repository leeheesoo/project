package com.cetaphilevent.service.core;

import java.util.List;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSetting;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;

@Service
public interface CoreNaverSearchingPrizeSettingService {
	// 경품 관리 수정
	void update(CoreNaverSearchingPrizeSetting entry) throws Exception;
	
	// 경품 관리 등록
	void insert(CoreNaverSearchingPrizeSetting entry) throws Exception;

	// 경품 목록
	List<CoreNaverSearchingPrizeSetting> get();
}
