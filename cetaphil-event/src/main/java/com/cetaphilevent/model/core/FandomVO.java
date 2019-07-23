package com.cetaphilevent.model.core;

import java.util.Date;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;
@Data
public class FandomVO {
	private Boolean isUse;
	private Boolean isChildren;
	private Boolean childrenAge;
	private Boolean childrenAge2;
	private Boolean childrenAge3;
	private Boolean childrenAge4;
	private String comment;
	private String name;
	private String mobile;
	private String birth;
	private String address;
	private String snsAddress;
	private String snsAddress2;
	private String snsAddress3;
	private String snsAddress4;
	private String ipAddress;
	private String channel;
	private Date createdDate;
}
