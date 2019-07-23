package com.cetaphilevent.service.body;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyAgeUserDao;
import com.cetaphilevent.repository.body.BodyAgeUserRepository;
import com.cetaphilevent.util.EventServiceException;

@Service
public class BodyAgeUserServiceImpl implements BodyAgeUserService {
	
	@Autowired
	private BodyAgeUserRepository repository;

	@Override
	public Long save(BodyAgeUserDao entry) {
		// TODO Auto-generated method stub
		BodyAgeUserDao user = repository.save(entry);
		return user.getId();
	}

	@Override
	public void update(Long userId, String snsType) throws EventServiceException {
		// TODO Auto-generated method stub
		BodyAgeUserDao user = repository.findOne(userId);
		if (user == null) {
			throw new EventServiceException("400", "응모 데이터를 찾을 수 없습니다", "");
		}
		switch(snsType) {
			case "facebook":
				user.setSharingFacebook(user.getSharingFacebook() + 1);
				break;
			case "kakaotalk":
				user.setSharingKakaotalk(user.getSharingKakaotalk() + 1);
				break;
			case "kakaostory":
				user.setSharingKakaostory(user.getSharingKakaostory() + 1);
				break;
			default:
				throw new EventServiceException("400", "SNS 공유 정보가 잘못되었습니다", "");
		}
		repository.save(user);
	}

}
