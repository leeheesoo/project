package com.babience.survey.dto;

import javax.validation.constraints.AssertTrue;
import javax.validation.constraints.Max;
import javax.validation.constraints.Min;
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
public class RenewalAdvanceApplicationDTO {
	@NotBlank(message="이름을 입력해주세요.")
	@Length(max=50,message="이름을 50자이내로 입력해주세요.")
	@Pattern(regexp = "^[a-zA-Z가-힣]+$", message = "이름은 한글 또는 영문으로만 입력 가능합니다.")
	@Order(7)
	private String name;
	@NotBlank(message="휴대폰 번호를 입력해주세요.")
	@Pattern(regexp= "^(010|011|016|017|019)[0-9]{7,8}$",message="휴대폰 번호를 정확히 입력해주세요. (ex:01012345678)")
	@Order(8)
	private String mobile;
	@NotBlank(message="주소를 입력해주세요.")
	@Order(9)
	private String zipCode;
	@NotBlank(message="주소를 입력해주세요.")
	@Length(max=200,message="주소를 200자이내로 입력해주세요.")
	@Order(10)
	private String address;
	@NotBlank(message="상세 주소를 입력해주세요.")
	@Length(max=200,message="상세 주소를 200자이내로 입력해주세요.")
	@Order(11)
	private String addressDetail;
	@NotNull(message="아이생일을 선택해주세요.")
	@Min(value=2018,message="아이생일을 정확하게 입력해주세요.")
	@Max(value=2020,message="아이생일을 정확하게 입력해주세요.")
	@Order(12)
	private Integer babyBirthYear;
	@NotNull(message="아이생일을 선택해주세요.")
	@Min(value=1,message="아이생일을 정확하게 입력해주세요.")
	@Max(value=12,message="아이생일을 정확하게 입력해주세요.")
	@Order(13)
	private Integer babyBirthMonth;
	@NotBlank(message="아기 월령을 선택해주세요.")
	@Order(1)
	private String babyAge;
	@NotBlank(message="설문-사용중인 제품을 입력해주세요.")
	@Length(max=20,message="사용중인 제품을 20자이내로 입력해주세요.")
	@Order(2)
	private String usedProduct;
	@NotBlank(message="수유 방식을 선택해주세요.")
	@Order(3)
	private String feedingType;
	@Length(max=50,message="육아시 고민되는 부분을 50자이내로 입력해주세요.")
	@Order(4)
	private String childcareWorry;
	@NotBlank(message="처음 접하게된 경로를 선택해주세요.")
	@Length(max=50,message="처음 접하게된 경로를 50자이내로 입력해주세요.")
	@Order(5)
	private String firstRoute;
	@Length(max=50,message="처음 접하게된 경로를 50자이내로 입력해주세요.")
	@Order(6)
	private String firstRoute2;
	@Order(16)
	private boolean agree;
	@AssertTrue(message="개인정보 활용에 동의해주세요.")
	@Order(14)
	private boolean agree2;
	@AssertTrue(message="경품 배송 및 이벤트 관련 위탁에 동의해주세요.")
	@Order(15)
	private boolean agree3;
}
