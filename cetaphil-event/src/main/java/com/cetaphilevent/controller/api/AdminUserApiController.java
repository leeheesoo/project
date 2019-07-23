package com.cetaphilevent.controller.api;

import javax.validation.Valid;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.cetaphilevent.model.admin.User;
import com.cetaphilevent.service.admin.UserService;

@RestController
@RequestMapping("api/user")
@PreAuthorize("hasAnyAuthority('Administrator')")
public class AdminUserApiController {
	@Autowired
    private UserService userService;
	
	@RequestMapping(value = "post", method = RequestMethod.POST)
	public void Post(@Valid @ModelAttribute User model){
		userService.saveUser(model);
	}
}