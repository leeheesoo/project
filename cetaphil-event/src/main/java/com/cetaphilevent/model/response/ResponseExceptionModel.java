package com.cetaphilevent.model.response;

import java.util.Date;
import java.util.List;

import org.springframework.validation.ObjectError;

import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
public class ResponseExceptionModel {
	@ApiModelProperty(value = "에러 메세지")
	private String error;  
	private String status;
}
