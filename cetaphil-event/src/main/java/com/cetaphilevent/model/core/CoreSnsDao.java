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
@Table(name = "core_sns")
@Data
public class CoreSnsDao {
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	private Long id;
	
	@NotNull
	private String name;
	@NotNull
	@Column(length=11)
	private String mobile;
	
	@NotNull
	@Column(name = "kakaotalk_sharing_count", columnDefinition = "int default 0")
	private int kakaotalkSharingCount;
	
	@NotNull
	@Column(name = "kakaostory_sharing_count", columnDefinition = "int default 0")
	private int kakaostorySharingCount;

	@NotNull
	@Column(name = "facebook_sharing_count", columnDefinition = "int default 0")
	private int facebookSharingCount;
	
	private String channel;
	
	private String ipAddress;
	@CreationTimestamp
	@Temporal(TemporalType.TIMESTAMP)
	private Date createdDate;
	@Temporal(TemporalType.TIMESTAMP)
	private Date updateDate;
}
