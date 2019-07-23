package com.babience.nickname.dao;

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
public class KabritaNicknameUser {		
	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private Long id;
	
	@NotNull
	@Column(columnDefinition="NVARCHAR(50)")
	private String nickname;
	
	@NotNull
	@Column(columnDefinition="NVARCHAR(50)")
	private String reason;
	
	@NotNull
	@Column(columnDefinition="NVARCHAR(50)")
	private String name;
	
	@NotNull
	@Column(length=11)
	private String phone;
	
	@NotNull
	@Column(columnDefinition="NVARCHAR(10)")
	private String channel;

	@NotNull
	@Column(length=5)
	private String zipcode;

	@NotNull
	@Column(columnDefinition="NVARCHAR(300)")
	private String address;

	@NotNull
	@Column(columnDefinition="NVARCHAR(300)")
	private String addressDetail;
	
	@NotNull
	@Column(name = "is_show", nullable = false, columnDefinition="boolean default true")
	private Boolean isShow;
	
	@NotNull
	@Column(length=15)
	private String ipAddress;
	
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	private Date createdDate;
	
	@NotNull
	@Column(name = "kakao_sharing_count", columnDefinition="int default 0")
	private int kakaoSharingCount;
	
	@NotNull
	@Column(name = "url_sharing_count", columnDefinition="int default 0")
	private int urlSharingCount;
	
	@NotNull
	@Column(name = "is_agree_marketing", nullable = false, columnDefinition="boolean default true")
	private Boolean agreeMarketing;
}
