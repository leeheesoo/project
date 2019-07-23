package com.cetaphilevent.model.core;

import java.io.Serializable;
import java.util.Date;

import javax.persistence.Embeddable;
import javax.persistence.Id;
import javax.validation.constraints.NotNull;

import lombok.Data;

@Embeddable
@Data
public class CoreNaverSearchingPrizeSettingKey<TType>  implements Serializable{
	//날짜
	@NotNull
	private Date date;
	
	//경품타입
	@NotNull
	private TType prizeType;
}
