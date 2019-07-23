package com.cetaphilevent.service.skinregeneration;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingUser;

@Service
public interface SkinRegenerationNaverSearchingUserService {
	// 즉석당첨 처리 & 결과 저장
	SkinRegenerationNaverSearchingUser save(SkinRegenerationNaverSearchingUser entry, boolean isChecked) throws Exception;

	// 당첨자 가져오기 findById
	SkinRegenerationNaverSearchingUser getWinnerById(Long userId) throws Exception;

	// 당첨시 개인정보 업데이트
	void update(SkinRegenerationNaverSearchingUser entry) throws Exception;
}
