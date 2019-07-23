package com.cetaphilevent.service.skinregeneration;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Random;

import javax.transaction.Transactional;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeSetting;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingPrizeType;
import com.cetaphilevent.model.skinregeneration.SkinRegenerationNaverSearchingUser;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationNaverSearchingPrizeSettingRepository;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationNaverSearchingUserRepository;
import com.cetaphilevent.util.CommonProvider;
import com.cetaphilevent.util.EventServiceException;

@Service
public class SkinRegenerationNaverSearchingUserServiceImpl implements SkinRegenerationNaverSearchingUserService {

	private Logger logger = LoggerFactory.getLogger(getClass());

	@Autowired
	private SkinRegenerationNaverSearchingUserRepository userRepository;
	@Autowired
	private SkinRegenerationNaverSearchingPrizeSettingRepository prizeSettingRepository;
	@Autowired
	private ITimeProvider timeProvider;
	@Autowired
	private CommonProvider commonProvider;

	@Override
	@Transactional
	public SkinRegenerationNaverSearchingUser save(SkinRegenerationNaverSearchingUser entry, boolean isChecked) throws Exception {

		// IP로 당첨된 적이 있는지 확인
		boolean isWin = userRepository.findByIpAddressAndPrizeTypeNot(commonProvider.getIpAddress(), SkinRegenerationNaverSearchingPrizeType.LosingTicket).size() > 0;

		entry.setPrizeType(SkinRegenerationNaverSearchingPrizeType.LosingTicket);		 
		if (isChecked && !isWin) {
			// 경품목록 확인
			Date nowDate = new SimpleDateFormat("yyyyMMdd").parse(timeProvider.now());
			int nowHour = timeProvider.getDate().getHours();
			List<SkinRegenerationNaverSearchingPrizeSetting> prizes = prizeSettingRepository.findSelectorPrizeSetting(nowDate, nowHour);

			// 경품 내역 가져오기<시작시간, 확률 확인>
			float totalSeed = 0.0f;
			float seed = (float) new Random().nextDouble();
			for (SkinRegenerationNaverSearchingPrizeSetting prize : prizes) {
				totalSeed += prize.getRate();
				if (totalSeed > seed) {
					// 당첨
					entry.setPrizeType(prize.getKey().getPrizeType());
					break;
				}
			}

			if (entry.getPrizeType() != SkinRegenerationNaverSearchingPrizeType.LosingTicket) {
				// 경품 테이블 winnerCount + 1 업데이트
				SkinRegenerationNaverSearchingPrizeSetting prize = prizeSettingRepository.findByKeyDateAndKeyPrizeType(nowDate, entry.getPrizeType());
				if (prize == null) {
					entry.setPrizeType(SkinRegenerationNaverSearchingPrizeType.LosingTicket);
				} else {
					int prizeWinnerCount = userRepository.findByPrizeTypeAndCreatedDateAfter(prize.getKey().getPrizeType(), nowDate).size() + 1;
					prize.setWinnerCount(prizeWinnerCount);
					prizeSettingRepository.save(prize);
				}
			}
		}

		SkinRegenerationNaverSearchingUser user = userRepository.save(entry);
		return user;
	}

	@Override
	public SkinRegenerationNaverSearchingUser getWinnerById(Long userId) throws Exception {
		SkinRegenerationNaverSearchingUser user = userRepository.findOne(userId);
		if (user == null) {
			throw new EventServiceException("400", "잘못된 접근입니다", null);
		}
		if (user.getPrizeType() == SkinRegenerationNaverSearchingPrizeType.LosingTicket) {
			throw new EventServiceException("400", "꽝! 처리된 참여자입니다", null);
		}
		if (user.getUpdatedDate() != null) {
			throw new EventServiceException("400", "이미 개인정보가 저장되었습니다", null);
		}
		return user;
	}

	@Override
	public void update(SkinRegenerationNaverSearchingUser entry) throws Exception {
		if (userRepository.findByPhone(entry.getPhone()).size() > 0) {
			throw new EventServiceException("400", "이미 당첨이 되셨네요, 즉석이벤트는 한 번만 참여가 가능합니다", null);
		}
		entry.setUpdatedDate(timeProvider.getDate());
		userRepository.save(entry);
	}

}
