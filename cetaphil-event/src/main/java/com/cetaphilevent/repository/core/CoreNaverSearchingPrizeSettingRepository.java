package com.cetaphilevent.repository.core;

import java.util.Date;
import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSetting;
import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSettingKey;
import com.cetaphilevent.model.core.CoreNaverSearchingPrizeType;

@Repository
public interface CoreNaverSearchingPrizeSettingRepository extends JpaRepository<CoreNaverSearchingPrizeSetting, CoreNaverSearchingPrizeSettingKey<CoreNaverSearchingPrizeType>> {

	@Query("select e from #{#entityName} e where e.key.date = ?1 and e.startTime <= ?2 and e.totalCount > e.winnerCount and e.rate > 0.0f Order By rate Asc")
	List<CoreNaverSearchingPrizeSetting> findSelectorPrizeSetting(Date nowDate, int nowHour);

	CoreNaverSearchingPrizeSetting findByKeyDateAndKeyPrizeType(Date date, CoreNaverSearchingPrizeType prizeType);
}