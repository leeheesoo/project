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

import com.cetaphilevent.model.caribbean.CaribbeanEntry;
import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.caribbean.CaribbeanService;
import com.cetaphilevent.service.core.CoreFandomService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class coreFandomEventTest {
	@Autowired
	private MockMvc mvc;

	 @Autowired
	 private  MockHttpSession session;
	 @MockBean
	 @Autowired
	 private ITimeProvider timeProvider;
	
	@MockBean
	@Autowired
	private CoreFandomService service;
	
	@Test
	public void personalInfoSuccess() throws Exception  {
		//when
		when(timeProvider.now()).thenReturn("20181225");
		
		FandomDao userInfo = new FandomDao();
		userInfo.setId(1l);		
		when(service.createEntry(any(FandomDao.class))).thenReturn(userInfo);
		
		this.mvc.perform(post("/api/core/fandom")
				.param("isUse", "Y")
				.param("isChildren", "Y")
				.param("childrenAge", "false")
				.param("brith","890912")
				.param("comment", "가나다라마바사아차카타파하")
				.param("name", "이수영")
				.param("mobile", "01046282987")
				.param("zipcode", "11111")
				.param("address", "주소")
				.param("addressDetail", "상세주소")
				.param("birth", "890912")
				.param("snsAddress", "기대평")
				.param("agree", "true"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(content().contentType("application/json;charset=UTF-8"))
		.andExpect(content().json("{\"success\":true,\"message\":\"success\"}"))
		.andExpect(status().isOk());		
	}
	
	@Test
	public void personalInfoFail() throws Exception  {
		this.mvc.perform(post("/api/core/fandom")
				.param("isUse", "Y")
				.param("isChildren", "Y")
				.param("childrenAge", "false")
				.param("brith","")
				.param("comment", "가나다라마바사아차카타파하")
				.param("name", "이수영")
				.param("mobile", "01046282987")
				.param("zipcode", "11111")
				.param("address", "주소")
				.param("addressDetail", "상세주소")
				.param("snsAddress", "기대평")
				.param("agree","true"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(content().contentType("application/json;charset=UTF-8"))
		.andExpect(content().json("{\"error\":\"생년월일을 입력해주세요.\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest());		
	}
}
