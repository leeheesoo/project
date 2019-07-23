package com.cetaphilevent.model.skinregeneration;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Index;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.validation.constraints.NotNull;

import org.hibernate.annotations.CreationTimestamp;

import lombok.Data;

@Entity
@Data
@Table(indexes = { @Index(name = "IX_srns_user", columnList = "ipAddress,prizeType") })
public class SkinRegenerationNaverSearchingUser {
	// 아이디
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;

	// 경품
	@NotNull
	@Column(nullable = false, updatable = false)
	private SkinRegenerationNaverSearchingPrizeType prizeType;

	// 유입채널
	@NotNull
	@Column(nullable = false, updatable = false)
	private String channel;

	// 아이피주소
	@NotNull
	@Column(length = 15, nullable = false, updatable = false)
	private String ipAddress;

	// 참여일시
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	@Column(nullable = false, updatable = true)
	private Date createdDate;

	// 이름
	@Column(nullable = true, updatable = true)
	private String name;

	// 휴대폰
	@Column(nullable = true, updatable = true)
	private String phone;

	// 이메일
	@Column(nullable = true, updatable = true)
	private String email;

	// 우편번호
	@Column(nullable = true, updatable = true)
	private String zipcode;

	// 주소
	@Column(nullable = true, updatable = true)
	private String address;

	// 상세주소
	@Column(nullable = true, updatable = true)
	private String addressDetail;

	// 생년월일
	@Column(nullable = true, updatable = true)
	private String birth;

	// 개인정보 등록일시
	@Temporal(TemporalType.TIMESTAMP)
	@Column(nullable = true, updatable = true)
	private Date updatedDate;
}
