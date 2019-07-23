package com.cetaphilevent.model.body.dao;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

import org.hibernate.annotations.CreationTimestamp;

import lombok.Data;

@Entity
@Table(name = "body_age_users")
@Data
public class BodyAgeUserDao {
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;

	@Column(nullable = false, updatable = false)
	private String channel;

	@Column(nullable = false, updatable = false)
	private String name;

	@Column(nullable = false, updatable = false)
	private String phone;

	@Column(nullable = false, updatable = false)
	private String birth;

	@Column(nullable = false, updatable = false)
	private boolean checklist1;

	@Column(nullable = false, updatable = false)
	private boolean checklist2;
	
	@Column(nullable = false, updatable = false)
	private boolean checklist3;
	
	@Column(nullable = false, updatable = false)
	private boolean checklist4;
	
	@Column(nullable = false, updatable = false)
	private boolean checklist5;
	
	@Column(nullable = false, updatable = false)
	private int bodyAge;

	@Column(nullable = false, updatable = false)
	private String ipAddress;

	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	@Column(nullable = false, updatable = false)
	private Date createdDate;
	
	@Column(nullable = true, updatable = true)
	private int sharingKakaotalk;
	
	@Column(nullable = true, updatable = true)
	private int sharingKakaostory;
	
	@Column(nullable = true, updatable = true)
	private int sharingFacebook;
}
