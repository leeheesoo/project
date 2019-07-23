package com.cetaphilevent.model.skinregeneration;

import org.hibernate.validator.constraints.NotBlank;

import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
public class SkinRegenerationSamplingSharingDto {
	@ApiModelProperty(value = "공유 형태", required = true)
	@NotBlank(message="공유 형태를 확인해주세요")
	private String snsType;
}
