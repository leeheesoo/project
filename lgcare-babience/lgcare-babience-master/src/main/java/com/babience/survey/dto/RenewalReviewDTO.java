package com.babience.survey.dto;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class RenewalReviewDTO {
	@NotBlank(message="이름을 입력해주세요.")
	@Length(max=50,message="이름을 50자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	private String name;
	@NotBlank(message="휴대폰 번호를 입력해주세요.")
	@Pattern(regexp="[0-9]{10,11}",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	private String mobile;
	@NotBlank(message="주소를 입력해주세요.")
	private String zipCode;
	@NotBlank(message="주소를 입력해주세요.")
	@Length(max=200,message="주소를 200자이내로 입력해주세요.")
	private String address;
	@NotBlank(message="상세 주소를 입력해주세요.")
	@Length(max=200,message="상세 주소를 200자이내로 입력해주세요.")
	private String addressDetail;
	@NotBlank(message="후기URL을 입력해주세요.")
	@Length(max=100,message="후기URL을 100자이내로 입력해주세요.")
	@Pattern(regexp = "^(http(s)?:\\/\\/.)(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{2,256}(\\.[a-z]{2,6}|:[0-9]{3,4})\\b([-a-zA-Z0-9@:%_\\+.~#?&\\/\\/=]*)$", message = "후기URL을 정확히 입력해주세요.")
	private String reviewUrl;
	private boolean agree;
	@AssertTrue(message="개인정보 활용에 동의해주세요.")
	private boolean agree2;
	@AssertTrue(message="경품 배송 및 이벤트 관련 위탁에 동의해주세요.")
	private boolean agree3;
}
