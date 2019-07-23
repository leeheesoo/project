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
@Table(name = "body_calculator_users")
@Data
public class BodyCalculatorUserDao {
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
	private Integer facialSkinLotion;

	@Column(nullable = false, updatable = false)
	private Integer facialAssence;

	@Column(nullable = false, updatable = false)
	private Integer facialNutritionWhitening;

	@Column(nullable = false, updatable = false)
	private Integer facialEtc;

	@Column(nullable = false, updatable = false)
	private Integer bodySkinLotion;

	@Column(nullable = false, updatable = false)
	private Integer bodyAssence;

	@Column(nullable = false, updatable = false)
	private Integer bodyNutritionWhitening;

	@Column(nullable = false, updatable = false)
	private Integer bodyEtc;

	@Column(nullable = false, updatable = false)
	private Integer bodyPayment;

	@Column(nullable = false, updatable = false)
	private Integer facialPayment;

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
