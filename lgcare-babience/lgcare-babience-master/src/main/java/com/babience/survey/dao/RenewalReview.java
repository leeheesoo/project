package com.babience.survey.dao;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.validator.constraints.NotBlank;
import org.hibernate.validator.constraints.NotEmpty;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
public class RenewalReview {
	@Id
	@GeneratedValue(strategy=GenerationType.AUTO)
	private Long id;
	@Column(nullable=false,length=50)
	@NotEmpty
	@NotBlank
	private String name;
	@Column(nullable=false,length=11)
	@NotEmpty
	@NotBlank
	private String mobile;
	@Column(nullable=false,length=10)
	@NotEmpty
	@NotBlank
	private String zipCode;
	@Column(nullable=false,length=200)
	@NotEmpty
	@NotBlank
	private String address;
	@Column(nullable=false,length=200)
	@NotEmpty
	@NotBlank
	private String addressDetail;
	@Column(nullable=false,length=100)
	@NotEmpty
	@NotBlank
	private String reviewUrl;
	private boolean agree;
	
	@Column(nullable=false,length=10)
	@NotEmpty
	@NotBlank
	private String channel;
	@Column(nullable=false,length=15)
	@NotEmpty
	@NotBlank
	private String ipAddress;
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	@Column(updatable=false)
	private Date createDate;
	
	@Column(nullable=false)
	@ColumnDefault("2")
	private int trial;
}
