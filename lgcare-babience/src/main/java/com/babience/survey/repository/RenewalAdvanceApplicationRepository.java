package com.babience.survey.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.babience.survey.dao.RenewalAdvanceApplication;

public interface RenewalAdvanceApplicationRepository extends JpaRepository<RenewalAdvanceApplication, Long>{
	RenewalAdvanceApplication findByMobileAndTrial(String mobile, Integer trial);
}
