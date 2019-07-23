package com.babience.survey.repository;

import org.springframework.data.jpa.repository.JpaRepository;

import com.babience.survey.dao.RenewalExperience;

public interface RenewalExperienceRepository extends JpaRepository<RenewalExperience, Long> {
	RenewalExperience findByMobileAndTrial(String mobile, Integer trial);
}
