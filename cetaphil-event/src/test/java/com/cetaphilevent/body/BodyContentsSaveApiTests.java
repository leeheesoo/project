package com.cetaphilevent.body;

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
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.body.BodyContentsUserService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class BodyContentsSaveApiTests {
	@Autowired
	private MockMvc mvc;
				
	@MockBean
	private ITimeProvider timeProvider;
	
	@MockBean
	private BodyContentsUserService service;
		
	@Test
	/* 데이터 응모 완료
	 * 성공 테스트*/
	public void Test_Success() throws Exception {
		//when
		when(timeProvider.now()).thenReturn("20181130");
		
		//then
		this.mvc.perform(post("/api/body/contents-save")
				.param("name", "이새라")
				.param("phone", "01098839828")
				.param("birth", "900125")
				.param("agree", "true"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"success\":true,\"message\":\"동안바디 공유 이벤트 응모 데이터가 저장되었습니다\"}"))
		.andExpect(status().isOk()) // 상태값 OK
		;
	}
	
	@Test
	/* 유효성 검사
	 * 실패 테스트*/
	public void Test_Failure() throws Exception {
		//when
		when(timeProvider.now()).thenReturn("20181130");
		
		//then
		this.mvc.perform(post("/api/body/contents-save")
				.param("name", "이새라")
				.param("phone", "01098839828"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"error\":\"생년월일을 입력해주세요\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
	
	@Test
	/* 데이터 응모 완료
	 * 이벤트 기간 종료 테스트*/
	public void Test_Failure_End() throws Exception {
		//when
		when(timeProvider.now()).thenReturn("20181201");
		
		//then
		this.mvc.perform(post("/api/body/contents-save")
				.param("name", "이새라")
				.param("phone", "01098839828")
				.param("birth", "900125")
				.param("agree", "true"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"success\":false,\"message\":\"이벤트가 종료되었습니다.\"}"))
		.andExpect(status().isOk()) // 상태값 OK
		;
	}
}
