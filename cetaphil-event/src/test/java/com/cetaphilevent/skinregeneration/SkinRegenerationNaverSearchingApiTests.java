package com.cetaphilevent.skinregeneration;

import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.mock.web.MockHttpSession;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationNaverSearchingPrizeSettingRepository;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationNaverSearchingUserRepository;
import com.cetaphilevent.service.skinregeneration.SkinRegenerationNaverSearchingPrizeSettingService;
import com.cetaphilevent.service.skinregeneration.SkinRegenerationNaverSearchingUserService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class SkinRegenerationNaverSearchingApiTests {
	private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private MockMvc mvc;
	
	@Autowired
	private MockHttpSession session;
				
	@MockBean
	@Autowired
	private ITimeProvider timeProvider;
	
	@MockBean
	private SkinRegenerationNaverSearchingUserService service;	
	@MockBean
	private SkinRegenerationNaverSearchingPrizeSettingService prizeService;	
	@MockBean
	private SkinRegenerationNaverSearchingUserRepository repository;	
	@MockBean
	private SkinRegenerationNaverSearchingPrizeSettingRepository prizeRepository;
			
//	@Test
//	/* gsitfatr(스타벅스) 당첨 개인정보 저장
//	 * 성공 테스트*/
//	public void Test_Winner_User_gsitfatr_Success() throws Exception {
//		//given
//		//session.setAttribute("SummerNaverSearchingUserId", 1L);
//		//session.setAttribute("SummerNaverSearchingPrizeType", SummerNaverSearchingPrizeType.StarbucksAmericano);
//		 
//		//when
//		when(timeProvider.now()).thenReturn("20180928");
//		when(session.getAttribute("SummerNaverSearchingUserId")).thenReturn(1L);
//		when(session.getAttribute("SummerNaverSearchingPrizeType")).thenReturn(SkinRegenerationNaverSearchingPrizeType.StarbucksAmericano);
//		
//		//then
//		this.mvc.perform(post("/api/skin-regeneration/instant-lottery/update-winner").session(session)
//				.param("prizeImageName", "gsitfatr")
//				.param("name", "이새라")
//				.param("phone", "01098839828")
//				.param("birth", "900125")
//				.param("email", "lsr@mz.co.kr")
//				.param("agree", "true"))
//		.andDo(print())
//		.andExpect(content().contentType("application/json;charset=utf-8"))
//		.andExpect(content().json("{\"success\":true,\"message\":\"응모 데이터가 정상적으로 저장되었습니다\"}"))
//		.andExpect(status().isOk()) // 상태값 OK
//		;
//	}
	
//	@Test
//	/* gaifadt(레스토라덤) 당첨 개인정보 저장
//	 * 성공 테스트*/
//	public void Test_Winner_User_gaifadt_Success() throws Exception {
//		//given
////		given(session.getAttribute("SummerNaverSearchingUserId")).willReturn(1L);
////		given(session.getAttribute("SummerNaverSearchingPrizeType")).willReturn(SummerNaverSearchingPrizeType.CetaphilAD);
//		
//		//when
//		when(timeProvider.now()).thenReturn("20180928");	
//		
//		//when
//		this.mvc.perform(post("/api/skin-regeneration/instant-lottery/update-winner")
//				.param("prizeImageName", "gaifadt")
//				.param("name", "이새라")
//				.param("phone", "01098839828")
//				.param("zipcode" , "12345")
//				.param("address", "서울 강남구 역삼동")
//				.param("addressDetail", "735-22 GALA빌딩 9층")
//				.param("birth", "900125")
//				.param("email", "lsr@mz.co.kr")
//				.param("agree", "true"))
//		.andDo(print())
//		.andExpect(content().contentType("application/json;charset=utf-8"))
//		.andExpect(content().json("{\"success\":true,\"message\":\"응모 데이터가 정상적으로 저장되었습니다\"}"))
//		.andExpect(status().isOk()) // 상태값 OK
//		;
//	}
	
	@Test
	/* gsitfatr(스타벅스) 당첨 개인정보 저장
	 * Validation 실패 테스트*/
	public void Test_Winner_User_gsitfatr_Validation_Failure() throws Exception {
		//given
		when(timeProvider.now()).thenReturn("20180926");
				
		//when
		this.mvc.perform(post("/api/skin-regeneration/instant-lottery/update-winner")
				.param("prizeImageName", "gsitfatr")
				.param("name", "이새라")
				.param("phone", "01098839828")
				.param("birth", "900125")
				.param("email", "lsr@mz.co.kr")
				.param("agree", "false"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"error\":\"개인정보 수집 방침의 동의해주세요.\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
	
	@Test
	/* gaifadt(레스토라덤) 당첨 개인정보 저장
	 * Validation 실패 테스트*/
	public void Test_Winner_User_gaifadt_Validation_Failure() throws Exception {
		//given
		when(timeProvider.now()).thenReturn("20180926");
		
		//when
		this.mvc.perform(post("/api/skin-regeneration/instant-lottery/update-winner")
				.param("prizeImageName", "gaifadt")
				.param("name", "이새라")
				.param("phone", "01098839828")
				.param("zipcode" , "12345")
				.param("address", "서울 강남구 역삼동")
				.param("addressDetail", "")
				.param("birth", "900125")
				.param("email", "lsr@mz.co.kr")
				.param("agree", "true"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"error\":\"상세주소를 입력해주세요\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
	
	@Test
	/*애칭, 이유, 개인정보 저장
	 * 종료일 실패 테스트*/
	public void Test_Winner_User_Failure_End() throws Exception {
		//given
		when(timeProvider.now()).thenReturn("20180927");
		
		//when
		this.mvc.perform(post("/api/skin-regeneration/instant-lottery/update-winner")
				.param("prizeImageName", "gsitfatr")
				.param("name", "이새라")
				.param("phone", "01098839828")
				.param("birth", "900125")
				.param("email", "lsr@mz.co.kr")
				.param("agree", "true"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"success\":false,\"message\":\"응모 기간이 종료되었습니다\"}"))
		.andExpect(status().isOk()) // 상태값 OK
		;
	}

}
