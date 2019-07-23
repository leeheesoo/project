package com.babience.nickname.service;

import java.util.List;

import org.springframework.stereotype.Service;

import com.babience.nickname.dao.KabritaNicknameUser;

@Service
public interface KabritaNicknameUserService {
	/* 이벤트 */
	List<KabritaNicknameUser> getUserList();	
}