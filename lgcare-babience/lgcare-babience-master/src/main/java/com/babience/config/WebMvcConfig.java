package com.babience.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurerAdapter;

//.yml read file
@Configuration
//spring securit active
@EnableWebSecurity
//Controller authorize setting
@EnableGlobalMethodSecurity(prePostEnabled = true)
public class WebMvcConfig extends WebMvcConfigurerAdapter{
	
}
