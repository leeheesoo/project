package com.cetaphilevent.model.caribbean;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;

import io.swagger.annotations.ApiModelProperty;
import io.swagger.annotations.ApiParam;
import lombok.Data;

@Data
public class CaribbeanEntryDto {
		@NotBlank(message="이름을 입력해주세요.")
		@Length(max=50,message="이름을 50자이내로 입력해주세요.")
		@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
		@ApiModelProperty(value = "이름", required = true)
		private String name;
		
		@NotBlank(message="핸드폰번호를 입력해주세요.")
		@Pattern(regexp="[0-9]{10,11}",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
		@ApiModelProperty(value = "핸드폰번호", required = true)
		private String mobile;
		
		@NotBlank(message="주소를 입력해주세요.")
		@ApiModelProperty(value = "우편번호", required = true)
		private String zipcode;

		@NotBlank(message="주소를 입력해주세요.")
		@Length(max=200,message="주소를 200자이내로 입력해주세요.")
		@ApiModelProperty(value = "주소", required = true)
		private String address;
		
		@ApiModelProperty(value = "상세주소")
		@Length(max=200,message="주소를 200자이내로 입력해주세요.")
		private String addressDetail;
		
		@NotBlank(message="생년월일을 입력해주세요.")
		@Pattern(regexp="\\d{6}", message="생년월일을 정확히 입력해주세요. (ex:890912)")
		@ApiModelProperty(value = "생년월일", required = true)
		private String birth;
		
		@NotBlank(message="기대평을 입력해주세요.")
		@Length(max=100,message="기대평을 입력해주세요.")
		@ApiModelProperty(value = "기대평", required = true)
		private String content;
		
		@NotBlank(message="이메일을 입력해주세요.")
		@Pattern(regexp="^[_a-z0-9-]+(.[_a-z0-9-]+)*@(?:\\w+\\.)+\\w+$", message="이메일을 정확히 입력해주세요.")
		@ApiModelProperty(value = "email", required = true)
		private String email;

		
		@ApiParam(value="마케팅수신동의")
		@AssertTrue(message="개인정보 수집에 동의해주세요.")
		private boolean agree;
		
		private String snsType;
		
}
