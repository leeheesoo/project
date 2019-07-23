package com.cetaphilevent.model.core;

import java.util.Date;

import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import lombok.Data;
@Data
public class CoreSharginDto {
	private String snsType;
}
