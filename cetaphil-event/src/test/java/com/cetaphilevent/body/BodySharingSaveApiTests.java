package com.cetaphilevent.body;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.mock.web.MockHttpServletRequest;
import org.springframework.mock.web.MockHttpSession;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import org.springframework.web.context.WebApplicationContext;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

import com.cetaphilevent.service.body.BodyChallengeUserService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class BodySharingSaveApiTests {
	@Autowired
    private WebApplicationContext wac;
		
	private MockMvc mvc;
	public MockHttpSession session;
	public MockHttpServletRequest request;
	
	@MockBean
	private BodyChallengeUserService challengeService;
	
	@Before
    public void setUp() throws Exception {
		this.mvc = MockMvcBuilders.webAppContextSetup(this.wac).build();
    }
	
	@Test
	/* 데이터 응모 완료
	 * 성공 테스트*/
	public void Test_Success() throws Exception {
		//when
		session = new MockHttpSession();
		session.setAttribute("body-challenge-userId", 1L);
		
		request = new MockHttpServletRequest();
        request.setSession(session);
        RequestContextHolder.setRequestAttributes(new ServletRequestAttributes(request));
        
		//then
		this.mvc.perform(post("/api/body/sharing-save").session(session)
				.param("eventType", "challenge")
				.param("snsType", "facebook"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"success\":true,\"message\":\"SNS 공유 데이터가 저장되었습니다\"}"))
		.andExpect(status().isOk()) // 상태값 OK
		;
	}
	
	@Test
	/* 세션이 NULL일 경우
	 * 실패 테스트*/
	public void Test_Session_Failure() throws Exception {
		//when
		session = new MockHttpSession();
		
		request = new MockHttpServletRequest();
        request.setSession(session);
        RequestContextHolder.setRequestAttributes(new ServletRequestAttributes(request));
        
        //then
		this.mvc.perform(post("/api/body/sharing-save").session(session)
				.param("eventType", "challenge")
				.param("snsType", "facebook"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"error\":\"응모된 이벤트 정보를 찾을 수 없습니다.\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
	
	@Test
	/* 세션은 있지만 이벤트명이 다를 경우
	 * 실패 테스트*/
	public void Test_EventType_Failure() throws Exception {
		//when
		session = new MockHttpSession();
		session.setAttribute("body-test-userId", 1L);
		
		request = new MockHttpServletRequest();
        request.setSession(session);
        RequestContextHolder.setRequestAttributes(new ServletRequestAttributes(request));
        
        //then
    	this.mvc.perform(post("/api/body/sharing-save").session(session)
				.param("eventType", "test")
				.param("snsType", "facebook"))
    	.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"error\":\"이벤트명을 확인해주세요\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
}
