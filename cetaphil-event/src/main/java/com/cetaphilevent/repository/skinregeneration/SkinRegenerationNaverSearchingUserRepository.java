package com.cetaphilevent.repository.skinregeneration;

import java.util.Date;
import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeType;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingUser;

@Repository
public interface SkinRegenerationNaverSearchingUserRepository extends JpaRepository<SkinRegenerationNaverSearchingUser, Long> {
	List<SkinRegenerationNaverSearchingUser> findByPhone(String phone);

	List<SkinRegenerationNaverSearchingUser> findByIpAddressAndPrizeTypeNot(String ipAddress, SkinRegenerationNaverSearchingPrizeType prizeType);

	List<SkinRegenerationNaverSearchingUser> findByPrizeTypeAndCreatedDateAfter(SkinRegenerationNaverSearchingPrizeType prizeType, Date nowDate);
}
