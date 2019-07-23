package com.babience.quiz.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.babience.quiz.dao.RenewalQuiz;
import com.babience.quiz.repository.RenewalQuizRepository;
import com.babience.utils.exception.EventServiceException;

@Service
public class RenewalQuizServiceImpl implements RenewalQuizService{
	@Autowired
	private RenewalQuizRepository quizRepo;
	
	@Override
	public Long createRenewalQuiz(RenewalQuiz entity) {
		RenewalQuiz result = quizRepo.save(entity);
		return result.getId();
	}

	@Override
	public RenewalQuiz updateRenewalQuizSharingCount(Long id, String sharingType) throws Exception {
		RenewalQuiz user = quizRepo.findOne(id);
		if(user == null) {
			throw new EventServiceException("400", "응모정보가 존재하지 않습니다.", id);
		}
		if(sharingType.equals("kakao")) {
			user.setKakaoSharingCount(user.getKakaoSharingCount() + 1);
		}else if(sharingType.equals("url")) {
			user.setUrlSharingCount(user.getUrlSharingCount() + 1);
		}
		
		RenewalQuiz result = quizRepo.save(user);
		return result;
	}
}
