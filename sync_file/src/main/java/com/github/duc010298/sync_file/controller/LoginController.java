package com.github.duc010298.sync_file.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequestMapping("/abc")
public class LoginController {
	
	@PostMapping
	@ResponseBody
	public String abc() {
		return "Welcome to login page";
	}

}
