package com.cetaphilevent.caribbean;

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
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.service.caribbean.CaribbeanService;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class caribbeanEventTest {
	@Autowired
	private MockMvc mvc;

	 @Autowired
	 private  MockHttpSession session;
	 @MockBean
	 @Autowired
	 private ITimeProvider timeProvider;
	
	@MockBean
	@Autowired
	private CaribbeanService service;
	
	@Test
	public void Test_PersonalInfo_Success() throws Exception  {
		//when
		when(timeProvider.now()).thenReturn("20180725");
		
		CaribbeanEntry userInfo = new CaribbeanEntry();
		userInfo.setId(1l);		
		when(service.createEntry(any(CaribbeanEntry.class))).thenReturn(userInfo);
		
		//
		this.mvc.perform(post("/api/caribbean")
				.param("snsType", "kakao")
				.param("name", "이수영")
				.param("mobile", "01046282987")
				.param("zipcode", "11111")
				.param("address", "주소")
				.param("addressDetail", "상세주소")
				.param("birth", "890912")
				.param("content", "기대평")
				.param("email", "syl@mz.co.kr")
				.param("agree", "true"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(content().contentType("application/json;charset=UTF-8"))
		.andExpect(content().json("{\"success\":true,\"message\":\"success\"}"))
		.andExpect(status().isOk());		
	}
	
	@Test
	public void Test_End_Date_Check() throws Exception  {
		//when	
		when(timeProvider.now()).thenReturn("20180727");
		
		//
		this.mvc.perform(post("/api/caribbean")
				.param("snsType", "kakao")
				.param("name", "이수영")
				.param("mobile", "01046282987")
				.param("zipcode", "11111")
				.param("address", "주소")
				.param("addressDetail", "상세주소")
				.param("birth", "890912")
				.param("content", "기대평")
				.param("email", "syl@mz.co.kr")
				.param("agree", "true"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(content().contentType("application/json;charset=UTF-8"))
		.andExpect(content().json("{\"success\":false,\"message\":\"응모 기간이 종료되었습니다\"}"))
		.andExpect(status().isOk());		
	}
	
	@Test
	public void Test_PersonalInfo_Fail() throws Exception  {
		//when
		when(timeProvider.now()).thenReturn("20180725");
		
		CaribbeanEntry userInfo = new CaribbeanEntry();
		userInfo.setId(1l);		
		when(service.createEntry(any(CaribbeanEntry.class))).thenReturn(userInfo);
		
		//
		this.mvc.perform(post("/api/caribbean")
				.param("snsType", "kakao")
				.param("name", "이수영")
				.param("mobile", "01046282987")
				.param("zipcode", "11111")
				.param("address", "주소")
				.param("addressDetail", "상세주소")
				.param("birth", "890912")
				.param("content", "기대평")
				.param("email", "syl@mz.co.kr"))
		.andDo(print()) // 처리 내용 출력
		.andExpect(status().isBadRequest());		
	}
	
//	@Test
//	public void Test_PersonalInfoUpdate_Success() throws Exception  {
//		//when
//				CaribbeanEntry userInfo = new CaribbeanEntry();
//				userInfo.setId(1l);
//				when(service.getEntryById(1l)).thenReturn(userInfo);
//				
//				//
//				this.mvc.perform(post("/api/caribbean/sharing")
//						.sessionAttr("id", 1)
//						.param("snsType", "facebook"))
//				.andDo(print()) // 처리 내용 출력
//				.andExpect(status().isOk());		
//	}
}
