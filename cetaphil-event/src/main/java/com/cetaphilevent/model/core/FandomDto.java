package com.cetaphilevent.model.core;

import java.util.Date;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.springframework.core.annotation.Order;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;
@Data
public class FandomDto {
	@ApiModelProperty(value = "세타필 사용 여부", required = true)
	private String isUse;
	@ApiModelProperty(value = "자녀 여부", required = true)
	private String isChildren;
	@ApiModelProperty(value = "자녀 연령층 0~5세", required = true)
	private Boolean childrenAge;
	@ApiModelProperty(value = "자녀 연령층 6~9세", required = true)
	private Boolean childrenAge2;
	@ApiModelProperty(value = "자녀 연령층 10~12세", required = true)
	private Boolean childrenAge3;
	@ApiModelProperty(value = "자녀 연령층 13세이상", required = true)
	private Boolean childrenAge4;
	@ApiModelProperty(value = "한줄 다짐", required = true)
	private String comment;
	// 이름
	@NotBlank(message = "이름을 입력해주세요.")
	@Length(max = 50, message = "이름을 50자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	@ApiModelProperty(value = "이름", required = true)
	@Order(1)
	private String name;
	@NotBlank(message = "휴대폰 번호를 입력해주세요.")
	@Pattern(regexp = "^(010|011|016|017|019)[0-9]{7,8}$",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@ApiModelProperty(value = "휴대폰 번호", required = true)
	@Order(2)
	private String mobile;
	@NotBlank(message = "생년월일을 입력해주세요.")
	@Pattern(regexp = "^([0-9][0-9])(0[0-9]|1[0-2])(0[1-9]|[1-2][0-9]|3[0-1])$", message = "생년월일을 정확히 입력해주세요. (ex: 800808)")
	@ApiModelProperty(value = "생년월일", required = true)
	@Order(3)
	private String birth;
	@NotBlank(message = "주소를 입력해주세요.")
	@ApiModelProperty(value = "우편번호", required = true)
	@Order(4)
	private String zipcode;
	@NotBlank(message = "주소를 입력해주세요.")
	@ApiModelProperty(value = "주소", required = true)
	@Order(5)
	private String address;
	@NotBlank(message = "상세주소를 입력해주세요.")
	@ApiModelProperty(value = "상세주소", required = false)
	private String addressDetail;
	@ApiModelProperty(value = "sns계정", required = false)
	private String snsAddress;
	@ApiModelProperty(value = "sns계정", required = false)
	private String snsAddress2;
	@ApiModelProperty(value = "sns계정", required = false)
	private String snsAddress3;
	@ApiModelProperty(value = "sns계정", required = false)
	private String snsAddress4;
	@ApiParam(value="마케팅수신동의")
	@AssertTrue(message="개인정보 수집에 동의해주세요.")
	private boolean agree;
}
