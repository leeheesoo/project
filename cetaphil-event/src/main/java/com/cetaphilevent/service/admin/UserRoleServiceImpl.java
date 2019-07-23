package com.cetaphilevent.service.admin;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.admin.Role;
import com.cetaphilevent.repository.admin.RoleRepository;

@Service
public class UserRoleServiceImpl implements UserRoleService{
	@Autowired
	private RoleRepository roleRepo;
	
	@Override
	public List<Role> getRole() {
		return roleRepo.findAll();
	}
}
