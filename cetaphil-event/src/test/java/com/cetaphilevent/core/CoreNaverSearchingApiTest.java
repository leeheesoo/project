package com.cetaphilevent.core;

import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.mock.web.MockHttpServletRequest;
import org.springframework.mock.web.MockHttpSession;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.context.web.WebAppConfiguration;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import org.springframework.web.context.WebApplicationContext;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

import com.cetaphilevent.controller.api.core.CoreNaverSearchingApiController;
import com.cetaphilevent.model.core.CoreNaverSearchingPrizeType;
import com.cetaphilevent.model.core.CoreNaverSearchingUser;
import com.cetaphilevent.repository.ITimeProvider;
import com.cetaphilevent.repository.core.CoreNaverSearchingPrizeSettingRepository;
import com.cetaphilevent.repository.core.CoreNaverSearchingUserRepository;
import com.cetaphilevent.service.core.CoreNaverSearchingPrizeSettingService;
import com.cetaphilevent.service.core.CoreNaverSearchingUserService;

@RunWith(SpringRunner.class)
@SpringBootTest
@WebAppConfiguration
@AutoConfigureMockMvc
public class CoreNaverSearchingApiTest {
	private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private MockMvc mvc;
	
	@Autowired private WebApplicationContext wac; 
    @Autowired private MockHttpSession session;
				
	@MockBean
	@Autowired
	private ITimeProvider timeProvider;
	
	@Autowired
	private WebApplicationContext webApplicationContext;
	
	@Autowired
	private CoreNaverSearchingUserService service;	
	@MockBean
	private CoreNaverSearchingPrizeSettingService prizeService;	
	@MockBean
	private CoreNaverSearchingUserRepository repository;	
	@MockBean
	private CoreNaverSearchingPrizeSettingRepository prizeRepository;

	@Before
    public void setup() {
        this.mvc = MockMvcBuilders.webAppContextSetup(this.wac).build();
    }
	
	@Test
	public void TestWinnerValidationFail() throws Exception {
		//given
		when(timeProvider.now()).thenReturn("20190126");
				
		//when
		this.mvc.perform(post("/api/core/instant-lottery/update-winner")
				.param("prizeImageName", "tpxkvlfzmflqdldi")
				.param("name", "이수영")
				.param("phone", "01046282987")
				.param("zipcode" , "12345")
				.param("address", "서울 강남구 역삼동")
				.param("addressDetail", "")
				.param("agree", "false"))
		.andDo(print())
		.andExpect(content().contentType("application/json;charset=utf-8"))
		.andExpect(content().json("{\"error\":\"개인정보 수집 방침에 동의해주세요.\",\"status\":\"400\"}"))
		.andExpect(status().isBadRequest()) // 상태값 OK
		;
	}
	
//	@Test
//	public void TestInstant() throws Exception {
//		
//		session = new MockHttpSession(webApplicationContext.getServletContext());
//		session.setAttribute("CoreNaverSearchingStatus", "START");
//		
//		CoreNaverSearchingUser entry = new CoreNaverSearchingUser();
//		entry.setChannel("pc");
//		entry.setPrizeType(CoreNaverSearchingPrizeType.LosingTicket);
//		entry.setIpAddress("127.0.0.1");
//		
//		when(service.save(entry, false)).thenReturn(entry);
//		when(timeProvider.getDate().getHours()).thenReturn(1);
//		when(repository.save(entry)).thenReturn(entry);
//				
//		//when
//		this.mvc.perform(post("/api/core/instant-lottery").session(session))
//		.andDo(print())
//		.andExpect(status().isOk()) // 상태값 OK
//		;
//	}

}
