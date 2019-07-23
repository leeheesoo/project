package com.cetaphilevent.model.skinregeneration;

import org.springframework.beans.factory.annotation.Value;

public enum SkinRegenerationNaverSearchingPrizeType {
	@Value(value="0")
	LosingTicket,
	@Value(value="1")
	StarbucksAmericano,
	@Value(value="2")
	CetaphilAD;
}
