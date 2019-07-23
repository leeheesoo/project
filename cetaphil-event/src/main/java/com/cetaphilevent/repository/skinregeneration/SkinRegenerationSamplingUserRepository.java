package com.cetaphilevent.repository.skinregeneration;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationSamplingUser;

@Repository
public interface SkinRegenerationSamplingUserRepository extends JpaRepository<SkinRegenerationSamplingUser, Long> {
	SkinRegenerationSamplingUser findByPhone(String phone);
}
