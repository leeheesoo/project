package com.cetaphilevent.service.admin;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.admin.User;

@Service
public interface UserService {
	public User findUserByEmail(String email);
    public void saveUser(User user);
}