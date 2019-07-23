package com.cetaphilevent.repository.admin;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.admin.User;

@Repository
public interface UserRepository extends JpaRepository<User, Long>{

}