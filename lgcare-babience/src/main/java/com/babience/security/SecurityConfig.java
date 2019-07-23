package com.babience.security;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

import com.babience.config.AppConfig;

@Configuration
@EnableWebSecurity
@EnableGlobalMethodSecurity(prePostEnabled = true)
public class SecurityConfig extends WebSecurityConfigurerAdapter{
	@Autowired
	private AppConfig appConfig;
	
	@Override
	public void configure(WebSecurity web) throws Exception {
		web.httpFirewall(appConfig.allow());
		web.ignoring().antMatchers("/resources/**","/static/**","/css/**","/js/**","/images/**", "/vendor/**");
	}
	
	@Override
	protected void configure(HttpSecurity http) throws Exception{
		
		http
		//.requiresChannel().requestMatchers(request -> request.getHeader("X-Forwarded-Proto") != null).requiresSecure()	// https redirect
		//.and()
		.csrf().disable()
		.headers().httpStrictTransportSecurity().maxAgeInSeconds(31536000).includeSubDomains(true)
		.and()
		.and().headers().frameOptions().sameOrigin();
	}
}

