package com.cetaphilevent.service.skinregeneration;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationNaverSearchingPrizeSettingRepository;
import com.cetaphilevent.util.EventServiceException;

@Service
public class SkinRegenerationNaverSearchingPrizeSettingServiceImpl implements SkinRegenerationNaverSearchingPrizeSettingService {

	@Autowired
	private SkinRegenerationNaverSearchingPrizeSettingRepository repository;

	@Override
	public void update(SkinRegenerationNaverSearchingPrizeSetting entry) throws Exception {
		SkinRegenerationNaverSearchingPrizeSetting prizeSetting = repository.findOne(entry.getKey());
		if (prizeSetting == null) {
			throw new EventServiceException("400", "수정하실 경품을 찾을 수 없습니다 새로고침 후 다시 확인해주세요", null);
		}
		if (prizeSetting.getWinnerCount() > entry.getTotalCount()) {
			throw new EventServiceException("400", "이미 당첨된 수량보다 총 경품수량이 적게 설정되었습니다 다시 확인해주세요", null);
		}
		prizeSetting.setTotalCount(entry.getTotalCount());
		prizeSetting.setStartTime(entry.getStartTime());
		prizeSetting.setRate(entry.getRate());
		repository.save(prizeSetting);
	}

	@Override
	public List<SkinRegenerationNaverSearchingPrizeSetting> get() {
		return repository.findAll();
	}

}
