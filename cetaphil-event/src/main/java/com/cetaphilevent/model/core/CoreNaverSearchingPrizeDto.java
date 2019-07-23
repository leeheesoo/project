package com.cetaphilevent.model.core;

import com.cetaphilevent.model.response.ResponseModel;

public class CoreNaverSearchingPrizeDto extends ResponseModel {

	private CoreNaverSearchingPrizeType prizeType;

	public void setPrizeType(CoreNaverSearchingPrizeType prizeType) {
		this.prizeType = prizeType;
	}

	public boolean isWinner() {
		if (this.prizeType == CoreNaverSearchingPrizeType.LosingTicket) {
			return false;
		} else {
			return true;
		}
	}

	public String getPrizeImageName() {
		if (this.prizeType == CoreNaverSearchingPrizeType.StarbucksAmericano) {
			return "dkaprjffutsp";
		} else if (this.prizeType == CoreNaverSearchingPrizeType.Cream) {
			return "tpxkvlfzmflqdldi";
		} else if(this.prizeType == CoreNaverSearchingPrizeType.Dyson){
			return "ajsclzlsdlfkrn";
		} else if(this.prizeType == CoreNaverSearchingPrizeType.AirPots) {
			return "skehrkwrhtlvdj";
		} else if(this.prizeType == CoreNaverSearchingPrizeType.Lotion) {
			return "fhtusdlekttpxkvlf";
		} else { //꽝
			return "whlthdgody";
		}
	}
}
