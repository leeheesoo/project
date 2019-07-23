package com.cetaphilevent.skinregeneration;

import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.mock.web.MockHttpSession;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.repository.skinregeneration.SkinRegenerationSamplingUserRepository;
import com.cetaphilevent.service.skinregeneration.SkinRegenerationSamplingUserService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class SkinRegenerationSamplingApiTests {	
	@Autowired
	private MockMvc mvc;
		
	@Autowired
	private MockHttpSession session;
				
	@MockBean
	@Autowired
	private ITimeProvider timeProvider;
	
	@MockBean
	private SkinRegenerationSamplingUserService service;	
	@MockBean
	private SkinRegenerationSamplingUserRepository repository;
			
	@Test
	/* 개인정보 저장
	 * 성공 테스트*/
	public void Test_User_Success() throws Exception {
		//when
		when(timeProvider.now()).thenReturn("20180926");

		//then
		this.mvc.perform(post("/api/skin-regeneration/sampling/save-user").session(session)
				.param("name", "이새라")
				.param("phone", "01012345678")
				.param("zipcode" , "12345")
				.param("address", "서울 강남구 역삼동")
				.param("addressDetail", "735-22 GALA빌딩 9층")
				.param("birth", "900125")
				.param("email", "lsr@mz.co.kr")
				.param("agree", "true"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"success\":true,\"message\":\"개인정보가 저장되었습니다\"}"))
		.andExpect(status().isOk()) // 상태값 OK
		;
	}
	
	@Test
	/* 개인정보 저장
	 * Validation 실패 테스트*/
	public void Test_User_Validation_Failure() throws Exception {
		//when
		when(timeProvider.now()).thenReturn("20180926");

		//then
		this.mvc.perform(post("/api/skin-regeneration/sampling/save-user").session(session)
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
		.andExpect(content().json("{\"error\":\"상세주소를 입력해주세요.\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
	
	@Test
	/* 개인정보 저장
	 * 종료 실패 테스트*/
	public void Test_User_EndDate_Failure() throws Exception {
		//when
		when(timeProvider.now()).thenReturn("20180927");

		//then
		this.mvc.perform(post("/api/skin-regeneration/sampling/save-user").session(session)
				.param("name", "이새라")
				.param("phone", "01098839828")
				.param("zipcode" , "12345")
				.param("address", "서울 강남구 역삼동")
				.param("addressDetail", "735-22 GALA빌딩 9층")
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
