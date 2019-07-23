package com.babience.survey.service;

import org.springframework.stereotype.Service;

import com.babience.survey.dao.RenewalAdvanceApplication;
import com.babience.survey.dao.RenewalExperience;
import com.babience.survey.dao.RenewalReview;

@Service
public interface RenewalServeyService {
	Long createRenewalAdvanceApplication(RenewalAdvanceApplication entity);
	Long createRenewalExperience(RenewalExperience entity);
	RenewalReview createRenewalReview(RenewalReview entity);
	RenewalAdvanceApplication updateRenewalAdvanceApplicationSharingCount(Long id,String sharingType) throws Exception;
	RenewalExperience updateRenewalExperienceSharingCount(Long id,String sharingType) throws Exception;
	Boolean checkEventEntry(String mobile, Integer trial);
}
