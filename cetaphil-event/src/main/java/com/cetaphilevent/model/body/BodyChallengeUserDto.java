package com.cetaphilevent.model.body;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.hibernate.validator.constraints.URL;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;

@Data
public class BodyChallengeUserDto {
	@NotBlank(message = "이름을 입력해주세요")
	@Length(max = 50, message = "이름을 50자이내로 입력해주세요")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다")
	@ApiModelProperty(value = "이름")
	@Order(1)
	private String name;

	@NotBlank(message = "휴대폰 번호를 입력해주세요")
	@Pattern(regexp = "^(010|011|016|017|019)[0-9]{7,8}$", message = "휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@ApiModelProperty(value = "휴대폰 번호")
	@Order(2)
	private String phone;
	
	@NotBlank(message = "생년월일을 입력해주세요")
	@Pattern(regexp = "^(?:[0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1,2][0-9]|3[0,1]))", message = "생년월일을 정확히 입력해주세요. (ex: 800808)")
	@ApiModelProperty(value = "생년월일")
	@Order(3)
	private String birth;
	
	@NotBlank(message = "구매 후기를 등록하신 SNS 주소를 등록해주세요")
	@URL(message = "구매 후기를 등록하신 SNS 주소를 정확히 입력해주세요")
	@ApiModelProperty(value = "페이셜 용품 스킨/로션")
	@Order(4)
	private String link;

	@AssertTrue(message = "개인정보 수집 방침의 동의해주세요")
	@ApiParam(value = "개인정보 수집 방침 동의")
	@ApiModelProperty(value = "개인정보 수집 방침 동의")
	@Order(5)
	private boolean agree;
}
