//package com.babience.kabritarenewal;
//
//import static org.assertj.core.api.Assertions.assertThat;
//import static org.mockito.Mockito.when;
//
//import org.junit.Test;
//import org.junit.runner.RunWith;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.boot.test.context.SpringBootTest;
//import org.springframework.boot.test.mock.mockito.MockBean;
//import org.springframework.test.context.junit4.SpringRunner;
//
//import com.babience.survey.dao.RenewalAdvanceApplication;
//import com.babience.survey.dao.RenewalExperience;
//import com.babience.survey.dao.RenewalReview;
//import com.babience.survey.repository.RenewalAdvanceApplicationRepository;
//import com.babience.survey.repository.RenewalExperienceRepository;
//import com.babience.survey.repository.RenewalReviewRepository;
//import com.babience.survey.service.RenewalServeyService;
//
//@RunWith(SpringRunner.class)
//@SpringBootTest
//public class RenewalServeyServiceTests {
//	@MockBean
//	private RenewalAdvanceApplicationRepository advanceApplicationRepo;
//	@MockBean
//	private RenewalExperienceRepository experienceRepo;
//	@MockBean
//	private RenewalReviewRepository reviewRepo;
//	@Autowired
//	private RenewalServeyService service;
//	
//	@Test
//	public void createRenewalAdvanceApplictionTest() {
//		RenewalAdvanceApplication user = new RenewalAdvanceApplication();
//		user.setId(1L);
//		user.setName("테스트");
//		user.setMobile("01012345678");
//		user.setZipCode("12345");
//		user.setAddress("서울시 중랑구 중화동 280-10호");
//		user.setAddressDetail("2층");
//		user.setBabyAge("임산부");
//		user.setBabyBirthMonth(2);
//		user.setBabyBirthYear(2019);
//		user.setChannel("pc");
//		user.setChildcareWorry("테스트");
//		user.setFeedingType("모유수유");
//		user.setUsedProduct("임페리얼");
//		user.setAgree(true);
//		
//		when(advanceApplicationRepo.save(user)).thenReturn(user);
//		
//		Long userId = service.createRenewalAdvanceApplication(user);
//		
//		assertThat(userId).isNotNull();
//		assertThat(userId).isEqualTo(1L);
//	}
//	
//	@Test
//	public void createRenewalExperienceTest() {
//		RenewalExperience user = new RenewalExperience();
//		user.setId(1L);
//		user.setName("테스트");
//		user.setMobile("01012345678");
//		user.setZipCode("12345");
//		user.setAddress("서울시 중랑구 중화동 280-10호");
//		user.setAddressDetail("2층");
//		user.setProudctSteps("step3 (12~24개월)");
//		user.setSelectPoint("");
//		user.setBabyBirthMonth(2);
//		user.setBabyBirthYear(2019);
//		user.setChannel("pc");
//		user.setChildcareWorry("테스트");
//		user.setFeedingType("모유수유");
//		user.setUsedProduct("임페리얼");
//		user.setAgree(true);
//		
//		when(experienceRepo.save(user)).thenReturn(user);
//		
//		Long userId = service.createRenewalExperience(user);
//		
//		assertThat(userId).isNotNull();
//		assertThat(userId).isEqualTo(1L);
//	}
//	
//	@Test
//	public void createRenewalReviewTest() {
//		RenewalReview user = new RenewalReview();
//		user.setId(1L);
//		user.setName("테스트");
//		user.setMobile("01012345678");
//		user.setZipCode("12345");
//		user.setAddress("서울시 중랑구 중화동 280-10호");
//		user.setAddressDetail("2층");
//		user.setChannel("pc");
//		user.setReviewUrl("https://kabrita.kr");
//		user.setAgree(true);
//		
//		when(reviewRepo.save(user)).thenReturn(user);
//		
//		RenewalReview result = service.createRenewalReview(user);
//		
//		assertThat(result).isNotNull();
//		assertThat(result.getId()).isEqualTo(1L);
//	}
//	
//	@Test
//	public void updateRenewalAdvanceApplictionSharingCountTest() throws Exception{
//		RenewalAdvanceApplication user = new RenewalAdvanceApplication();
//		user.setId(1L);
//		user.setName("테스트");
//		user.setMobile("01012345678");
//		user.setZipCode("12345");
//		user.setAddress("서울시 중랑구 중화동 280-10호");
//		user.setAddressDetail("2층");
//		user.setBabyAge("임산부");
//		user.setBabyBirthMonth(2);
//		user.setBabyBirthYear(2019);
//		user.setChannel("pc");
//		user.setChildcareWorry("테스트");
//		user.setFeedingType("모유수유");
//		user.setUsedProduct("임페리얼");
//		user.setAgree(true);
//		
//		when(advanceApplicationRepo.findOne(1L)).thenReturn(user);
//		when(advanceApplicationRepo.save(user)).thenReturn(user);	
//		
//		RenewalAdvanceApplication result = service.updateRenewalAdvanceApplicationSharingCount(1L, "kakao");
//				
//		assertThat(result).isNotNull();
//		assertThat(result.getKakaoSharingCount()).isEqualTo(1);
//	}
//	
//	@Test
//	public void updateRenewalExprienceSharingCountTest() throws Exception{
//		RenewalExperience user = new RenewalExperience();
//		user.setId(1L);
//		user.setName("테스트");
//		user.setMobile("01012345678");
//		user.setZipCode("12345");
//		user.setAddress("서울시 중랑구 중화동 280-10호");
//		user.setAddressDetail("2층");
//		user.setProudctSteps("step3 (12~24개월)");
//		user.setSelectPoint("");
//		user.setBabyBirthMonth(2);
//		user.setBabyBirthYear(2019);
//		user.setChannel("pc");
//		user.setChildcareWorry("테스트");
//		user.setFeedingType("모유수유");
//		user.setUsedProduct("임페리얼");
//		user.setAgree(true);
//		
//		when(experienceRepo.findOne(1L)).thenReturn(user);
//		when(experienceRepo.save(user)).thenReturn(user);	
//		
//		RenewalExperience result = service.updateRenewalExperienceSharingCount(1L, "kakao");
//				
//		assertThat(result).isNotNull();
//		assertThat(result.getKakaoSharingCount()).isEqualTo(1);
//	}
//}
