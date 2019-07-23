package com.cetaphilevent.model.skinregeneration;

import com.cetaphilevent.model.response.ResponseModel;

public class SkinRegenerationNaverSearchingPrizeDto extends ResponseModel {

	private SkinRegenerationNaverSearchingPrizeType prizeType;

	public void setPrizeType(SkinRegenerationNaverSearchingPrizeType prizeType) {
		this.prizeType = prizeType;
	}

	public boolean isWinner() {
		if (this.prizeType == SkinRegenerationNaverSearchingPrizeType.LosingTicket) {
			return false;
		} else {
			return true;
		}
	}

	public String getPrizeImageName() {
		if (this.prizeType == SkinRegenerationNaverSearchingPrizeType.StarbucksAmericano) {
			return "gsitfatr";
		} else if (this.prizeType == SkinRegenerationNaverSearchingPrizeType.CetaphilAD) {
			return "gaifadt";
		} else {
			return "";
		}
	}
}
