package com.cetaphilevent.service.core;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.core.CoreNaverSearchingUser;
import com.cetaphilevent.model.core.CoreNaverSearchingUserDto;

@Service
public interface CoreNaverSearchingUserService {
	// 즉석당첨 처리 & 결과 저장
	CoreNaverSearchingUser save(CoreNaverSearchingUser entry, boolean isChecked) throws Exception;

	// 당첨자 가져오기 findById
	CoreNaverSearchingUser getWinnerById(Long userId) throws Exception;

	// 당첨시 개인정보 업데이트
	//void update(CoreNaverSearchingUserDto entry) throws Exception;

	void update(CoreNaverSearchingUserDto entry, CoreNaverSearchingUser user) throws Exception;
}
