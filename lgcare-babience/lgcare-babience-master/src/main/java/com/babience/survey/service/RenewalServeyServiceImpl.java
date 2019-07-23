package com.babience.survey.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.babience.survey.dao.RenewalAdvanceApplication;
import com.babience.survey.dao.RenewalExperience;
import com.babience.survey.dao.RenewalReview;
import com.babience.survey.repository.RenewalAdvanceApplicationRepository;
import com.babience.survey.repository.RenewalExperienceRepository;
import com.babience.survey.repository.RenewalReviewRepository;
import com.babience.utils.exception.EventServiceException;

@Service
public class RenewalServeyServiceImpl implements RenewalServeyService{
	@Autowired
	private RenewalAdvanceApplicationRepository advanceApplicationRepo;
	@Autowired
	private RenewalExperienceRepository experienceRepo;
	@Autowired
	private RenewalReviewRepository reviewRepo;
	
	@Override
	public Long createRenewalAdvanceApplication(RenewalAdvanceApplication entity) {
		RenewalAdvanceApplication user = advanceApplicationRepo.save(entity);
		return user.getId();
	}

	@Override
	public Long createRenewalExperience(RenewalExperience entity) {
		RenewalExperience user = experienceRepo.save(entity);
		return user.getId();
	}

	@Override
	public RenewalReview createRenewalReview(RenewalReview entity) {
		RenewalReview user = reviewRepo.save(entity);
		return user;
	}

	@Override
	public RenewalAdvanceApplication updateRenewalAdvanceApplicationSharingCount(Long id, String sharingType) throws Exception {
		RenewalAdvanceApplication user = advanceApplicationRepo.findOne(id);
		if(user == null) {
			throw new EventServiceException("400", "응모정보가 존재하지 않습니다.", id);
		}
		if(sharingType.equals("kakao")) {
			user.setKakaoSharingCount(user.getKakaoSharingCount() + 1);
		}else if(sharingType.equals("url")) {
			user.setUrlSharingCount(user.getUrlSharingCount() + 1);
		}
		
		RenewalAdvanceApplication result = advanceApplicationRepo.save(user);
		return result;
	}

	@Override
	public RenewalExperience updateRenewalExperienceSharingCount(Long id, String sharingType) throws Exception {
		RenewalExperience user = experienceRepo.findOne(id);
		if(user == null) {
			throw new EventServiceException("400", "응모정보가 존재하지 않습니다.", id);
		}
		if(sharingType.equals("kakao")) {
			user.setKakaoSharingCount(user.getKakaoSharingCount() + 1);
		}else {
			user.setUrlSharingCount(user.getUrlSharingCount() + 1);
		}
		
		RenewalExperience result = experienceRepo.save(user);
		return result;
	}

	@Override
	public Boolean checkEventEntry(String mobile, Integer trial) {
		RenewalAdvanceApplication advanceApplicationUser = advanceApplicationRepo.findByMobileAndTrial(mobile, trial);
		RenewalExperience experienceUser = experienceRepo.findByMobileAndTrial(mobile, trial);
		
		if(advanceApplicationUser != null || experienceUser != null) {
			return true;
		}
		return false;
	}
}
