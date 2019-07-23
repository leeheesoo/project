package com.cetaphilevent.core;

import static org.mockito.Matchers.any;
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

import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.core.CoreSnsService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class coreSnsShareEventTest {
	@Autowired
	private MockMvc mvc;

	 @Autowired
	 private  MockHttpSession session;
	 @MockBean
	 @Autowired
	 private ITimeProvider timeProvider;
	
	@MockBean
	@Autowired
	private CoreSnsService service;
	
	@Test
	public void snsInfoSuccess() throws Exception  {
		//when
		when(timeProvider.now()).thenReturn("20181225");
		
		CoreSnsDao userInfo = new CoreSnsDao();
		userInfo.setId(1l);		
		when(service.fandomSnsUserSave(any(CoreSnsDao.class))).thenReturn(userInfo.getId());
		
		this.mvc.perform(post("/api/core/sns")
				.param("name", "이수영")
				.param("mobile", "01046282987")
				.param("agree", "true"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(content().contentType("application/json;charset=UTF-8"))
		.andExpect(content().json("{\"success\":true,\"message\":\"success\"}"))
		.andExpect(status().isOk());		
	}
	
	@Test
	public void snsInfoFail() throws Exception  {
		this.mvc.perform(post("/api/core/fandom")
				.param("name", "이수영")
				.param("mobile", "010462829873")
				.param("agree", "true"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(content().contentType("application/json;charset=UTF-8"))
		.andExpect(content().json("{\"error\":\"휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest());		
	}
}
