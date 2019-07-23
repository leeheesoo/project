package com.cetaphilevent.model.skinregeneration;

import java.io.Serializable;
import java.util.Date;

import javax.persistence.Embeddable;
import javax.persistence.Id;
import javax.validation.constraints.NotNull;

import lombok.Data;

@Embeddable
@Data
public class SkinRegenerationNaverSearchingPrizeSettingKey<TType> implements Serializable {
	/**
	 * 
	 */
	private static final long serialVersionUID = -5280214267035986251L;
	
	//날짜
	@NotNull
	private Date date;
	
	//경품타입
	@NotNull
	private TType prizeType;
}
