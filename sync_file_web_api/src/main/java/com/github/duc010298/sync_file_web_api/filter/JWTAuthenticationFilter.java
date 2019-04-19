package com.github.duc010298.sync_file_web_api.filter;

import java.io.IOException;
import java.util.Collections;
import java.util.Date;

import javax.servlet.FilterChain;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;

import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.filter.GenericFilterBean;

import com.github.duc010298.sync_file_web_api.entity.AppUser;
import com.github.duc010298.sync_file_web_api.repository.AppUserRepository;
import com.github.duc010298.sync_file_web_api.services.TokenAuthenticationService;

public class JWTAuthenticationFilter extends GenericFilterBean {
	
	private final AppUserRepository appUserRepository;
	
	public JWTAuthenticationFilter(AppUserRepository appUserRepository) 
    {
        this.appUserRepository = appUserRepository;
    }

	@Override
	public void doFilter(ServletRequest request, ServletResponse response, FilterChain chain)
			throws IOException, ServletException {
		Authentication authentication = null;
		
		AppUser userInfoInToken = TokenAuthenticationService.getUserInfoFromToken((HttpServletRequest) request);
		AppUser userInfoOnDB = appUserRepository.findByUserName(userInfoInToken.getUserName());
        
        if(userInfoOnDB != null) {
        	if(userInfoOnDB.getTokenActiveAfter().before(userInfoInToken.getTokenActiveAfter())) {
        		authentication = new UsernamePasswordAuthenticationToken(userInfoInToken.getUserName(), null, Collections.emptyList());
        	}
        }
		
        SecurityContextHolder.getContext().setAuthentication(authentication);
        chain.doFilter(request, response);
	}
}

