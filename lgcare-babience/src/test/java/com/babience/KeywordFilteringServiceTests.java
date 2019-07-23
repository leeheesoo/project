//package com.babience;
//
//import static org.junit.Assert.assertFalse;
//import static org.junit.Assert.assertTrue;
//
//import org.junit.Test;
//import org.junit.runner.RunWith;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.boot.test.context.SpringBootTest;
//import org.springframework.test.context.junit4.SpringRunner;
//
//import com.babience.utils.keywordfiltering.KeywordFilteringService;
//
//@RunWith(SpringRunner.class)
//@SpringBootTest
//public class KeywordFilteringServiceTests {
//	
//	@Autowired
//	private KeywordFilteringService service;
//	
//	@Test
//	public void KeywordFilteringService_Failure() {
//		Boolean result = service.isMatchKeyword("똥이다");
//		
//		assertTrue(result);		
//	}
//	
//	@Test
//	public void KeywordFilteringService_Success() {
//		Boolean result = service.isMatchKeyword("이유입니다");
//		
//		assertFalse(result);		
//	}
//}
