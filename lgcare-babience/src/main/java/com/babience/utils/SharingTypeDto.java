package com.babience.utils;

import org.hibernate.validator.constraints.NotBlank;

import lombok.Data;

@Data
public class SharingTypeDto {
	@NotBlank(message="공유타입을 입력해주세요.")
	public String sharingType;
}
