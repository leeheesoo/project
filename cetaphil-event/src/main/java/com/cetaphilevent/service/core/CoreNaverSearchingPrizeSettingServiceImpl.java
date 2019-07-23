package com.cetaphilevent.service.core;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSetting;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;
import com.cetaphilevent.repository.core.CoreNaverSearchingPrizeSettingRepository;
import com.cetaphilevent.util.EventServiceException;

@Service
public class CoreNaverSearchingPrizeSettingServiceImpl implements CoreNaverSearchingPrizeSettingService {

	@Autowired
	private CoreNaverSearchingPrizeSettingRepository repository;
	private Logger logger = LoggerFactory.getLogger(getClass());

	@Override
	public void update(CoreNaverSearchingPrizeSetting entry) throws Exception {
		CoreNaverSearchingPrizeSetting prizeSetting = repository.findOne(entry.getKey());
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
	public List<CoreNaverSearchingPrizeSetting> get() {
		return repository.findAll();
	}

	@Override
	public void insert(CoreNaverSearchingPrizeSetting entry) throws Exception {
		
		CoreNaverSearchingPrizeSetting prize = repository.findByKeyDateAndKeyPrizeType(entry.getKey().getDate(), entry.getKey().getPrizeType());
		if(prize != null) {
			String message = String.format("%s에 등록된 %s이 있습니다.", entry.getKey().getDate().toString(), entry.getKey().getPrizeType().toString());
			logger.debug(message);
			throw new EventServiceException("400", message, null);
		}
		repository.save(entry);
	}

}
