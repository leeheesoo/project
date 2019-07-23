package com.cetaphilevent.repository.admin;


import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.admin.Role;

@Repository
public interface RoleRepository extends JpaRepository<Role, Long> {

}