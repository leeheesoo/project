package com.babience.utils.exception;

import java.util.Date;

import lombok.Data;

@Data
public class ResponseExceptionVo {
	private String error;  
	private String status; 
	private Date timestamp;
	private String Path;
}
