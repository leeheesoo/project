package com.cetaphilevent.model.core;

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
@Table(indexes = { @Index(name = "IX_core_user", columnList = "ipAddress, prizeType"), @Index(name = "IX_core_user_mobile", columnList = "phone") })
public class CoreNaverSearchingUser {
	// 아이디
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;

	// 경품
	@NotNull
	@Column(nullable = false, updatable = false)
	private CoreNaverSearchingPrizeType prizeType;

	
	private String name;
	
	// 유입채널
	@NotNull
	@Column(nullable = false, updatable = false)
	private String channel;

	// 아이피주소
	@NotNull
	@Column(length = 15, nullable = false, updatable = false)
	private String ipAddress;


	// 휴대폰
	@Column(nullable = true, updatable = true)
	private String phone;

	// 우편번호
	@Column(nullable = true, updatable = true)
	private String zipcode;

	// 주소
	@Column(nullable = true, updatable = true)
	private String address;

	// 상세주소
	@Column(nullable = true, updatable = true)
	private String addressDetail;


	// 개인정보 등록일시
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	@Column(nullable = true, updatable = true)
	private Date createdDate;
	
	@Temporal(TemporalType.TIMESTAMP)
	@Column(nullable = true, updatable = true)
	private Date updatedDate;
}
