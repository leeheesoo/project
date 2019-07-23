package com.babience.nickname.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.babience.nickname.dao.KabritaNicknameUser;
import com.babience.nickname.repository.KabritaNicknameUserRepository;

@Service
public class KabritaNicknameUserServiceImpl implements KabritaNicknameUserService {
		
	@Autowired
	private KabritaNicknameUserRepository repository;
		
	@Override
	public List<KabritaNicknameUser> getUserList() {
		return repository.findTop60ByIsShowTrueOrderByCreatedDateDesc();
	}
}
