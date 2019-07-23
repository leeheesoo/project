package com.cetaphilevent.service.skinregeneration;

import java.util.List;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;

@Service
public interface SkinRegenerationNaverSearchingPrizeSettingService {
	// 경품 관리 수정
	void update(SkinRegenerationNaverSearchingPrizeSetting entry) throws Exception;

	// 경품 목록
	List<SkinRegenerationNaverSearchingPrizeSetting> get();
}
