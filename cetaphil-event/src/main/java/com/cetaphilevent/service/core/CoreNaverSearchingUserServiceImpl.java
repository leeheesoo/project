package com.cetaphilevent.service.core;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Random;

import javax.transaction.Transactional;

import org.modelmapper.ModelMapper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.core.CoreNaverSearchingPrizeSetting;
import com.cetaphilevent.model.core.CoreNaverSearchingPrizeType;
import com.cetaphilevent.model.core.CoreNaverSearchingUser;
import com.cetaphilevent.model.core.CoreNaverSearchingUserDto;
import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.repository.core.CoreNaverSearchingPrizeSettingRepository;
import com.cetaphilevent.repository.core.CoreNaverSearchingUserRepository;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

@Service
public class CoreNaverSearchingUserServiceImpl implements CoreNaverSearchingUserService {

	private Logger logger = LoggerFactory.getLogger(getClass());
	private ModelMapper modelMapper = new ModelMapper();

	@Autowired
	private CoreNaverSearchingUserRepository userRepository;
	@Autowired
	private CoreNaverSearchingPrizeSettingRepository prizeSettingRepository;
	@Autowired
	private ITimeProvider timeProvider;
	@Autowired
	private CommonProvider commonProvider;

	@Override
	@Transactional
	public CoreNaverSearchingUser save(CoreNaverSearchingUser entry, boolean isChecked) throws Exception {
		// IP로 당첨된 적이 있는지 확인
		boolean isWin = userRepository.findByIpAddressAndPrizeTypeNot(commonProvider.getIpAddress(), CoreNaverSearchingPrizeType.LosingTicket).size() > 0;
		logger.debug("isWin ::: {}", isWin);

		entry.setPrizeType(CoreNaverSearchingPrizeType.LosingTicket);		 
		if (isChecked && !isWin) {
			// 경품목록 확인
			Date nowDate = new SimpleDateFormat("yyyyMMdd").parse(timeProvider.now());
			int nowHour = timeProvider.getDate().getHours();
			List<CoreNaverSearchingPrizeSetting> prizes = prizeSettingRepository.findSelectorPrizeSetting(nowDate, nowHour);

			// 경품 내역 가져오기<시작시간, 확률 확인>
			float totalSeed = 0.0f;
			float seed = (float) new Random().nextDouble();
			for (CoreNaverSearchingPrizeSetting prize : prizes) {
				totalSeed += prize.getRate();
				if (totalSeed > seed) {
					// 당첨
					entry.setPrizeType(prize.getKey().getPrizeType());
					break;
				}
			}

			if (entry.getPrizeType() != CoreNaverSearchingPrizeType.LosingTicket) {
				// 경품 테이블 winnerCount + 1 업데이트
				CoreNaverSearchingPrizeSetting prize = prizeSettingRepository.findByKeyDateAndKeyPrizeType(nowDate, entry.getPrizeType());
				if (prize == null) {
					entry.setPrizeType(CoreNaverSearchingPrizeType.LosingTicket);
				} else {
					int prizeWinnerCount = userRepository.findByPrizeTypeAndCreatedDateAfter(prize.getKey().getPrizeType(), nowDate).size() + 1;
					prize.setWinnerCount(prizeWinnerCount);
					prizeSettingRepository.save(prize);
				}
			}
		}

		CoreNaverSearchingUser user = userRepository.save(entry);
		return user;
	}

	@Override
	public CoreNaverSearchingUser getWinnerById(Long userId) throws Exception {
		CoreNaverSearchingUser user = userRepository.findOne(userId);
		if (user == null) {
			throw new EventServiceException("400", "잘못된 접근입니다", null);
		}
		if (user.getPrizeType() == CoreNaverSearchingPrizeType.LosingTicket) {
			throw new EventServiceException("400", "꽝! 처리된 참여자입니다", null);
		}
		if (user.getUpdatedDate() != null) {
			throw new EventServiceException("400", "참여정보 이력이 있습니다.", null);
		}
		return user;
	}

	@Override
	public void update(CoreNaverSearchingUserDto entry, CoreNaverSearchingUser user) throws Exception {
		if (userRepository.findByPhone(entry.getPhone()).size() > 0) {
			throw new EventServiceException("400", "즉석당첨 이벤트는 한 번만 참여가 가능합니다", null);
		} else {
			modelMapper.map(entry, user);
			user.setUpdatedDate(timeProvider.getDate());
			userRepository.save(user);
		}
	}

}
