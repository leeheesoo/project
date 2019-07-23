package com.cetaphilevent.model.core;

import javax.validation.constraints.NotNull;

import lombok.Data;

@Data
public class CoreNaverSearchingPrizeSettingDto {
    private CoreNaverSearchingPrizeSettingKey<CoreNaverSearchingPrizeType> key;
	
	private Float rate;
	
	@NotNull
	private int totalCount;
	
	@NotNull
	private int startTime;
}