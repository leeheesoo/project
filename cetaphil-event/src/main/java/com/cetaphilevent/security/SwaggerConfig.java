package com.cetaphilevent.security;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Profile;

import springfox.documentation.builders.ApiInfoBuilder;
import springfox.documentation.builders.PathSelectors;
import springfox.documentation.builders.RequestHandlerSelectors;
import springfox.documentation.service.ApiInfo;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

@Configuration
@EnableSwagger2
@Profile("default")
public class SwaggerConfig {
	@Bean
    public Docket api() { 
        return new Docket(DocumentationType.SWAGGER_2)  
          .select()      
          .apis(RequestHandlerSelectors.basePackage("com.cetaphilevent.controller.api.core"))
          .paths(PathSelectors.any())
          .build()
          .apiInfo(apiInfo());                                           
    }
	
	private ApiInfo apiInfo(){
		return new ApiInfoBuilder()
				.title("세타필 API 문서")
				.description("Eclipse [Run As > Spring Boot App] 후에 테스트 하실 수 있습니다.^^")
				.build();
	}
}