package com.cetaphilevent.model.skinregeneration;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.validation.constraints.NotNull;

import org.hibernate.annotations.CreationTimestamp;

import lombok.Data;

@Entity
@Data
public class SkinRegenerationSamplingUser {
	// 아이디
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;

	// 이름
	@NotNull
	private String name;

	// 휴대폰
	@NotNull
	private String phone;

	// 우편번호
	@NotNull
	private String zipcode;

	// 주소
	@NotNull
	private String address;

	// 상세주소
	@NotNull
	private String addressDetail;

	// 생년월일
	@NotNull
	private String birth;

	// 이메일
	@NotNull
	private String email;

	// 유입채널
	@NotNull
	private String channel;

	// 아이피주소
	@NotNull
	@Column(length = 15)
	private String ipAddress;

	// 참여일시
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	@Column(updatable = false)
	private Date createDate;

	// 카카오 공유
	@NotNull
	@Column(name = "kakao_sharing_count", columnDefinition = "int default 0")
	private int kakaoSharingCount;

	// 페이스북 공유
	@NotNull
	@Column(name = "facebook_sharing_count", columnDefinition = "int default 0")
	private int facebookSharingCount;
}
