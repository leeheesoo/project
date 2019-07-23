package com.cetaphilevent.repository.body;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.body.dao.BodyCalculatorUserDao;

@Repository
public interface BodyCalculatorUserRepository extends JpaRepository<BodyCalculatorUserDao, Long> {

}
