package com.babience.quiz.dto;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Pattern;

import org.hibernate.validator.constraints.Length;
import org.hibernate.validator.constraints.NotBlank;
import org.springframework.core.annotation.Order;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class RenewalQuizDTO {
	@NotBlank(message="이름을 입력해주세요.")
	@Length(max=50,message="이름을 50자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	@Order(1)
	private String name;
	@NotBlank(message="휴대폰 번호를 입력해주세요.")
	@Pattern(regexp = "^(010|011|016|017|019)[0-9]{7,8}$",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@Order(2)
	private String mobile;
	@Order(5)
	private boolean agree;
	@AssertTrue(message="개인정보 활용에 동의해주세요.")
	@Order(3)
	private boolean agree2;
	@AssertTrue(message="경품 배송 및 이벤트 관련 위탁에 동의해주세요.")
	@Order(4)
	private boolean agree3;	
	@NotNull(message="정답 개수에 대한 정보를 찾을 수가 없습니다. 새로고침 후 다시 참여해주세요")
	@Order(5)
	private Integer answerCount;
}
