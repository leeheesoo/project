package com.cetaphilevent.security;

import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.builders.WebSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.web.authentication.AuthenticationSuccessHandler;
import org.springframework.security.web.firewall.DefaultHttpFirewall;
import org.springframework.security.web.firewall.HttpFirewall;
import org.springframework.security.web.util.matcher.AntPathRequestMatcher;
import org.springframework.web.filter.CharacterEncodingFilter;

@Configuration
@EnableWebSecurity
@EnableGlobalMethodSecurity(prePostEnabled = true)
public class SecurityConfig extends WebSecurityConfigurerAdapter{
	@Autowired
	private BCryptPasswordEncoder bCryptPasswordEncoder;
    
	@Autowired
	private DataSource dataSource;
	@Value("${query.security.users}")
	private String usersQuery;
	@Value("${query.security.roles}")
	private String rolesQuery;
	
	@Override
	public void configure(WebSecurity web) throws Exception {
		web.httpFirewall(allow());
		web.ignoring().antMatchers("/resources/**","/static/**","/css/**","/js/**","/fonts/**","/images/**","/scssCommon/**");
	}
	
	@Override
	protected void configure(HttpSecurity http) throws Exception{
		http
		.requiresChannel().requestMatchers(request -> request.getHeader("X-Forwarded-Proto") != null).requiresSecure()
		.and()
		.authorizeRequests()
		.antMatchers("/swagger-ui.html**").hasAuthority("Administrator")	// swagger-ui페이지 권한제어 
		.and().csrf().disable().formLogin()
		.loginPage("/manager/login").failureUrl("/manager/login?error=true")
		.defaultSuccessUrl("/manager").successHandler(successHandler())
		.usernameParameter("email")
		.passwordParameter("password")
		.and().logout().permitAll()
		.logoutRequestMatcher(new AntPathRequestMatcher("/manager/logout"))
		.logoutSuccessUrl("/manager/login")
		.and().headers().httpStrictTransportSecurity().maxAgeInSeconds(31536000).includeSubDomains(true)
		.and()
		.and().headers().frameOptions().sameOrigin();
		
		http.csrf().disable();
	}
	
	@Bean
	public AuthenticationSuccessHandler successHandler(){
		return new CustomLoginSuccessHandler("/manager");
	}
	
	@Override
	protected void configure(AuthenticationManagerBuilder auth) throws Exception {
		 auth.jdbcAuthentication()
		 	.usersByUsernameQuery(usersQuery)
		 	.authoritiesByUsernameQuery(rolesQuery)	
		 	.passwordEncoder(bCryptPasswordEncoder)
		 	.dataSource(dataSource); 	
	}
	
	
	 @Bean
	 public CharacterEncodingFilter characterEncodingFilter() {
		CharacterEncodingFilter characterEncodingFilter = new CharacterEncodingFilter();
		characterEncodingFilter.setEncoding("UTF-8");
		characterEncodingFilter.setForceEncoding(true);
		return characterEncodingFilter;
	 }
	 @Bean
	 public HttpFirewall allow() {
	     DefaultHttpFirewall firewall = new DefaultHttpFirewall();
	     firewall.setAllowUrlEncodedSlash(true);
	     
	     return firewall;
	 }
	
}