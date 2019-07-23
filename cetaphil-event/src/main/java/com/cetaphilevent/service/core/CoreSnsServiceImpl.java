package com.cetaphilevent.service.core;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyAgeUserDao;
import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.repository.caribbean.CaribbeanEntryRespository;
import com.cetaphilevent.repository.core.FandomRepository;
import com.cetaphilevent.repository.core.CoreSnsRepository;
import com.cetaphilevent.util.EventServiceException;

@Service
public class CoreSnsServiceImpl implements CoreSnsService{
private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private CoreSnsRepository repository;

	@Override
	public Long fandomSnsUserSave(CoreSnsDao entry) {
		// TODO Auto-generated method stub
		return repository.save(entry).getId();
	}
	
	@Override
	public void snsCounterUpdate(Long userId, String snsType) throws EventServiceException {
		// TODO Auto-generated method stub
		CoreSnsDao user = repository.findOne(userId);
		if (user == null) {
			throw new EventServiceException("400", "응모 데이터를 찾을 수 없습니다", "");
		}
		switch(snsType) {
			case "facebook":
				user.setFacebookSharingCount(user.getFacebookSharingCount() + 1);
				break;
			case "kakaotalk":
				user.setKakaotalkSharingCount(user.getKakaotalkSharingCount() + 1);
				break;
			case "kakaostory":
				user.setKakaostorySharingCount(user.getKakaostorySharingCount() + 1);
				break;
			default:
				throw new EventServiceException("400", "SNS 공유 정보가 잘못되었습니다", "");
		}
		repository.save(user);
	}
	
	public List<CoreSnsDao> getSnsEntryList() {
		return repository.findAll(sortByIdDesc());
	}
	
	private Sort sortByIdDesc() {
        return new Sort(Sort.Direction.DESC, "id");
	}

}
