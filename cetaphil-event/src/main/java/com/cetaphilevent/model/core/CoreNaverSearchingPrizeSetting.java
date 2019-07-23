package com.cetaphilevent.model.core;

import javax.persistence.EmbeddedId;
import javax.persistence.Entity;
import javax.validation.constraints.NotNull;

import lombok.Data;

@Entity
@Data
public class CoreNaverSearchingPrizeSetting {
	@EmbeddedId
    private CoreNaverSearchingPrizeSettingKey<CoreNaverSearchingPrizeType> key;
	
	//당첨확률(%)
	private Float rate;
	
	//총 경품수
	@NotNull
	private int totalCount;
	
	//당첨된 경품수
	@NotNull
	private int winnerCount;
	
	//시작시간
	@NotNull
	private int startTime;
}