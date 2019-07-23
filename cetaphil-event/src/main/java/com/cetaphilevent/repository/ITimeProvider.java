package com.cetaphilevent.repository;

import java.util.Date;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.admin.User;

@Repository
public interface ITimeProvider extends JpaRepository<User, Long> {
	@Query(value="SELECT DATE_FORMAT(now(), '%Y%m%d') as now", nativeQuery=true)
	public String now();
	
	@Query(value="SELECT now() as now", nativeQuery=true)
	public Date getDate();
}