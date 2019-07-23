package com.cetaphilevent.model.skinregeneration;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Email;
import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.hibernate.validator.constraints.ScriptAssert;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;

@Data
@ScriptAssert.List({ @ScriptAssert(lang = "javascript", script = "_.prizeImageName.equals(\"gaifadt\") ? (_.zipcode != null && _.zipcode.length() > 0) : true", alias = "_", message = "우편번호를 버튼을 클릭 후 주소를 입력해주세요"),
		@ScriptAssert(lang = "javascript", script = "_.prizeImageName.equals(\"gaifadt\") ? (_.address != null && _.address.length() > 0) : true", alias = "_", message = "주소를 입력해주세요"),
		@ScriptAssert(lang = "javascript", script = "_.prizeImageName.equals(\"gaifadt\") ? (_.addressDetail != null && _.addressDetail.length() > 0) : true", alias = "_", message = "상세주소를 입력해주세요") })
public class SkinRegenerationNaverSearchingUserDto {
	// 경품
	@NotBlank(message = "경품을 확인해주세요")
	@ApiModelProperty(value = "경품 이미지명", required = true)
	@Order(1)
	private String prizeImageName;

	private SkinRegenerationNaverSearchingPrizeType prizeType;

	public SkinRegenerationNaverSearchingPrizeType getPrizeType() {
		if (this.prizeImageName.equals("gsitfatr")) {
			return SkinRegenerationNaverSearchingPrizeType.StarbucksAmericano;
		} else if (this.prizeImageName.equals("gaifadt")) {
			return SkinRegenerationNaverSearchingPrizeType.CetaphilAD;
		} else {
			return SkinRegenerationNaverSearchingPrizeType.LosingTicket;
		}
	};

	// 이름
	@NotBlank(message = "이름을 입력해주세요.")
	@Length(max = 50, message = "이름을 50자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	@ApiModelProperty(value = "이름", required = true)
	@Order(2)
	private String name;

	// 휴대폰
	@NotBlank(message = "휴대폰 번호를 입력해주세요.")
	@Pattern(regexp = "^(010|011|016|017|019)[0-9]{7,8}$",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@ApiModelProperty(value = "휴대폰 번호", required = true)
	@Order(3)
	private String phone;

	// 우편번호
	@ApiModelProperty(value = "우편번호", required = false)
	@Order(4)
	private String zipcode;

	// 주소
	@ApiModelProperty(value = "주소", required = false)
	@Order(5)
	private String address;

	// 상세주소
	@ApiModelProperty(value = "상세주소", required = false)
	@Order(6)
	private String addressDetail;

	// 생년월일
	@NotBlank(message = "생년월일을 입력해주세요.")
	@Pattern(regexp = "[0-9]{6}", message = "생년월일을 정확히 입력해주세요. (ex: 800808)")
	@ApiModelProperty(value = "생년월일", required = true)
	@Order(7)
	private String birth;

	// 이메일
	@NotBlank(message = "이메일을 입력해주세요.")
	@Email(message = "이메일을 정확히 입력해주세요.")
	@ApiModelProperty(value = "email", required = true)
	@Order(8)
	private String email;

	// 개인정보 수집 방침 동의
	@AssertTrue(message = "개인정보 수집 방침의 동의해주세요.")
	@ApiParam(value = "개인정보 수집 방침 동의")
	@ApiModelProperty(value = "개인정보 수집 방침 동의", required = true)
	@Order(9)
	private boolean agree;
}
