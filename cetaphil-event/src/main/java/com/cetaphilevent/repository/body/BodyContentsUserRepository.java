package com.cetaphilevent.repository.body;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.body.dao.BodyContentsUserDao;

@Repository
public interface BodyContentsUserRepository extends JpaRepository<BodyContentsUserDao, Long> {

}
