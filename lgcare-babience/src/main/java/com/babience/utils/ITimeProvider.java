package com.babience.utils;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import com.babience.survey.dao.RenewalAdvanceApplication;

@Repository
public interface ITimeProvider extends JpaRepository<RenewalAdvanceApplication, Long> {
	@Query(value="SELECT DATE_FORMAT(now(), '%Y%m%d') as now", nativeQuery=true)
	public String now();
}
