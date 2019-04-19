package com.github.duc010298.sync_file_web_api.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequestMapping("/TestToken")
public class TestTokenController {
	
	@GetMapping
	@ResponseBody
	public String TestToken() {
		return "Success";
	}
}