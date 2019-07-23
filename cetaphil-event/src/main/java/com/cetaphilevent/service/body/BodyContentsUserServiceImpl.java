package com.cetaphilevent.service.body;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyContentsUserDao;
import com.cetaphilevent.repository.body.BodyContentsUserRepository;
import com.cetaphilevent.util.EventServiceException;

@Service
public class BodyContentsUserServiceImpl implements BodyContentsUserService {
	
	@Autowired
	private BodyContentsUserRepository repository;

	@Override
	public Long save(BodyContentsUserDao entry) {
		// TODO Auto-generated method stub
		BodyContentsUserDao user = repository.save(entry);
		return user.getId();
	}

	@Override
	public void update(Long userId, String snsType) throws EventServiceException {
		// TODO Auto-generated method stub
		BodyContentsUserDao user = repository.findOne(userId);
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
