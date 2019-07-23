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
public class CoreSnsDto {
	@NotBlank(message = "이름을 입력해주세요.")
	@Length(max = 5, message = "이름을 5자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	@ApiModelProperty(value = "이름", required = true) // allowableValues = "이새라"
	@Order(1)
	private String name;
	@NotBlank(message = "휴대폰 번호를 입력해주세요.")
	@Pattern(regexp = "^(010|011|016|017|019)[0-9]{7,8}$",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@ApiModelProperty(value = "휴대폰 번호", required = true)
	@Order(2)
	private String mobile;
	@ApiParam(value="마케팅수신동의")
	@AssertTrue(message="개인정보 수집에 동의해주세요.")
	private boolean agree;
}
