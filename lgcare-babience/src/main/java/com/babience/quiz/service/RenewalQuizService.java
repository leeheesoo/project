package com.babience.quiz.service;

import org.springframework.stereotype.Service;

import com.babience.quiz.dao.RenewalQuiz;

@Service
public interface RenewalQuizService {
	Long createRenewalQuiz(RenewalQuiz entity);
	RenewalQuiz updateRenewalQuizSharingCount(Long id,String sharingType) throws Exception;
}
