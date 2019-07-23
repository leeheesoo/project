package com.cetaphilevent.model.core;

import javax.persistence.Column;
import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;

@Data
public class CoreNaverSearchingUserDto {
	
	@NotBlank(message = "경품을 확인해주세요")
	@ApiModelProperty(value = "경품 이미지명", required = true)
	@Order(1)
	private String prizeImageName;
	
	
	private CoreNaverSearchingPrizeType prizeType;
	
	public CoreNaverSearchingPrizeType getPrizeType() {
		if (this.prizeImageName.equals("dkaprjffutsp")) {
			return CoreNaverSearchingPrizeType.StarbucksAmericano;
		} else if (this.prizeImageName.equals("tpxkvlfzmflqdldi")) {
			return CoreNaverSearchingPrizeType.Cream;
		} else if(this.prizeImageName.equals("ajsclzlsdlfkrn")) {
			return CoreNaverSearchingPrizeType.Dyson;
		} else if(this.prizeImageName.equals("skehrkwrhtlvdj")) {
			return CoreNaverSearchingPrizeType.AirPots;
		} else if(this.prizeImageName.equals("fhtusdlekttpxkvlf")) {
			return CoreNaverSearchingPrizeType.Lotion;
		} else {
			return CoreNaverSearchingPrizeType.LosingTicket;
		}
	};

	@NotBlank(message = "이름을 입력해주세요.")
	@Length(max = 50, message = "이름을 50자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	@ApiModelProperty(value = "이름", required = true)
	@Order(2)
	private String name;

	@NotBlank(message = "휴대폰 번호를 입력해주세요.")
	@Pattern(regexp = "^(010|011|016|017|019)[0-9]{7,8}$",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@ApiModelProperty(value = "휴대폰 번호", required = true)
	@Order(3)
	private String phone;

	@NotBlank(message = "주소를 확인해 주세요.")
	@ApiModelProperty(value = "우편번호", required = false)
	@Order(4)
	@Column(nullable = true, updatable = true)
	private String zipcode;

	@NotBlank(message = "주소를 입력해 주세요.")
	@ApiModelProperty(value = "주소", required = false)
	@Order(5)
	private String address;

	// 상세주소
	@ApiModelProperty(value = "상세주소", required = false)
	@Order(6)
	private String addressDetail;
	
	@AssertTrue(message = "개인정보 수집 방침에 동의해주세요.")
	@ApiParam(value = "개인정보 수집 방침 동의")
	@ApiModelProperty(value = "개인정보 수집 방침 동의", required = true)
	@Order(7)
	private boolean agree;
}
