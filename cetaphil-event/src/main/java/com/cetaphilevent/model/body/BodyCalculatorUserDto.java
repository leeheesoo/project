package com.cetaphilevent.model.body;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.hibernate.validator.constraints.URL;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;

@Data
public class BodyCalculatorUserDto {
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
	
	@NotNull(message = "당신이 사용하고 있는 한달 평균 페이셜 용품 스킨/로션의 개수를 입력해주세요")
	@ApiModelProperty(value = "페이셜 용품 스킨/로션")
	@Order(4)
	private Integer facialSkinLotion;

	@NotNull(message = "당신이 사용하고 있는 한달 평균 페이셜 용품 에센스의 개수를 입력해주세요")
	@ApiModelProperty(value = "페이셜 용품 에센스")
	@Order(5)
	private Integer facialAssence;

	@NotNull(message = "당신이 사용하고 있는 한달 평균 페이셜 용품 영양/미백의 개수를 입력해주세요")
	@ApiModelProperty(value = "페이셜 용품 영양/미백")
	@Order(6)
	private Integer facialNutritionWhitening;

	@NotNull(message = "당신이 사용하고 있는 한달 평균 페이셜 용품 기타의 개수를 입력해주세요")
	@ApiModelProperty(value = "페이셜 용품 기타")
	@Order(7)
	private Integer facialEtc;
	
	@NotNull(message = "당신이 사용하고 있는 한달 평균 바디 용품 스킨/로션의 개수를 입력해주세요")
	@ApiModelProperty(value = "바디 용품 스킨/로션")
	@Order(8)
	private Integer bodySkinLotion;

	@NotNull(message = "당신이 사용하고 있는 한달 평균 바디 용품 에센스의 개수를 입력해주세요")
	@ApiModelProperty(value = "바디 용품 에센스")
	@Order(9)
	private Integer bodyAssence;

	@NotNull(message = "당신이 사용하고 있는 한달 평균 바디 용품 영양/미백의 개수를 입력해주세요")
	@ApiModelProperty(value = "바디 용품 영양/미백")
	@Order(10)
	private Integer bodyNutritionWhitening;

	@NotNull(message = "당신이 사용하고 있는 한달 평균 바디 용품 기타의 개수를 입력해주세요")
	@ApiModelProperty(value = "바디 용품 기타")
	@Order(11)
	private Integer bodyEtc;
	
	@NotNull(message = "바디 투자비용이 계산되지 않았습니다")
	@ApiModelProperty(value = "바디 투자비용")	
	@Order(12)
	private Integer bodyPayment;
	
	@NotNull(message = "얼굴 투자비용이 계산되지 않았습니다")
	@ApiModelProperty(value = "얼굴 투자비용")
	@Order(13)
	private Integer facialPayment;
	
	@AssertTrue(message = "개인정보 수집 방침의 동의해주세요")
	@ApiParam(value = "개인정보 수집 방침 동의")
	@ApiModelProperty(value = "개인정보 수집 방침 동의")
	@Order(14)
	private boolean agree;
}
