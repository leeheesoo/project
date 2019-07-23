package com.cetaphilevent.model.core;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;
import javax.validation.constraints.NotNull;

import org.hibernate.annotations.CreationTimestamp;

import com.cetaphilevent.model.body.dao.BodyAgeUserDao;

import lombok.Data;
@Entity
@Table(name = "core_fandom")
@Data
public class FandomDao {
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;
	@NotNull
	private Boolean isUse;
	@NotNull
	private Boolean isChildren;
	@NotNull
	private String birth;
	@NotNull
	private Boolean childrenAge;
	
	private Boolean childrenAge2;
	
	private Boolean childrenAge3;
	
	private Boolean childrenAge4;
	@NotNull
	@Column(length=20)
	private String comment;
	@NotNull
	private String name;
	@NotNull
	@Column(length=11)
	private String mobile;
	@NotNull
	@Column(length=5)
	private String zipcode;
	@NotNull
	private String address;
	
	private String addressDetail;

	private String snsAddress;
	
	private String snsAddress2;
	
	private String snsAddress3;
	
	private String snsAddress4;
	
	private String channel;
	
	private String ipAddress;
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	private Date createdDate;
}
