package com.cetaphilevent.service.skinregeneration;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationSamplingUser;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationSamplingUserRepository;

@Service
public class SkinRegenerationSamplingUserServiceImpl implements SkinRegenerationSamplingUserService {

	@Autowired
	private SkinRegenerationSamplingUserRepository repository;

	@Override
	public Long save(SkinRegenerationSamplingUser entry) {
		SkinRegenerationSamplingUser user = repository.save(entry);
		return user.getId();
	}	

	@Override
	public void updateBySharingCount(Long userId, String snsType) throws Exception {
		SkinRegenerationSamplingUser user = repository.findOne(userId);
		if (user == null) {
			throw new Exception("잘못된 접근입니다.");
		}
		if (snsType.equals("kakaotalk")) {
			user.setKakaoSharingCount(user.getKakaoSharingCount() + 1);
		} else if (snsType.equals("facebook")) {
			user.setFacebookSharingCount(user.getFacebookSharingCount() + 1);
		} else {
			throw new Exception("공유 형태를 다시 확인해주세요");
		}
		repository.save(user);
	}

	@Override
	public long totalCount() {
		return repository.count();
	}

	@Override
	public boolean existUser(String phone) {
		return repository.findByPhone(phone) != null;
	}

}
