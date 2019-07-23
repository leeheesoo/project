package com.cetaphilevent.repository.skinregeneration;

import java.util.Date;
import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSettingKey;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeType;

@Repository
public interface SkinRegenerationNaverSearchingPrizeSettingRepository extends JpaRepository<SkinRegenerationNaverSearchingPrizeSetting, SkinRegenerationNaverSearchingPrizeSettingKey<SkinRegenerationNaverSearchingPrizeType>> {

	@Query("select e from #{#entityName} e where e.key.date = ?1 and e.startTime <= ?2 and e.totalCount > e.winnerCount and e.rate > 0.0f Order By rate Asc")
	List<SkinRegenerationNaverSearchingPrizeSetting> findSelectorPrizeSetting(Date nowDate, int nowHour);

	SkinRegenerationNaverSearchingPrizeSetting findByKeyDateAndKeyPrizeType(Date date, SkinRegenerationNaverSearchingPrizeType prizeType);
}
