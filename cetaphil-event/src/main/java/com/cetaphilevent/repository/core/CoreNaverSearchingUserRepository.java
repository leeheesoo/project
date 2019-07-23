package com.cetaphilevent.repository.core;

import java.util.Date;
import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeType;
import com.cetaphilevent.model.core.CoreNaverSearchingUser;

@Repository
public interface CoreNaverSearchingUserRepository extends JpaRepository<CoreNaverSearchingUser, Long> {
	List<CoreNaverSearchingUser> findByPhone(String phone);

	List<CoreNaverSearchingUser> findByIpAddressAndPrizeTypeNot(String ipAddress, CoreNaverSearchingPrizeType prizeType);

	List<CoreNaverSearchingUser> findByPrizeTypeAndCreatedDateAfter(CoreNaverSearchingPrizeType prizeType, Date nowDate);
}
