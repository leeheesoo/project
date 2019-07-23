package com.cetaphilevent.util;

import lombok.Data;

@Data
public class EventServiceException extends Exception{
	private String code;
	private Object details;
		
	public EventServiceException(String code,String message,Object details){
		super(message);
		
		this.code = code;
		this.details = details;
	}
}
