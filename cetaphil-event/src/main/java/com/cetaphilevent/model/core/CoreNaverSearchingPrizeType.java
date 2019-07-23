package com.cetaphilevent.model.core;

import org.springframework.beans.factory.annotation.Value;

public enum CoreNaverSearchingPrizeType {
	@Value(value="0")
	Lotion,
	@Value(value="1")
	Cream,
	@Value(value="2")
	StarbucksAmericano,
	@Value(value="3")
	AirPots,
	@Value(value="4")
	Dyson,
	@Value(value="5")
	LosingTicket;
}
