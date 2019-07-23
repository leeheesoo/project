package com.cetaphilevent.model.caribbean;

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

@Data
@Entity
public class CaribbeanEntry {
	
	//아이디
		@Id
		@GeneratedValue(strategy=GenerationType.AUTO)
		private Long id;
		
		//이름
		@NotNull
		private String name;
		
		@NotNull
		private String birth;
		
		//기대평 
		@NotNull
		@Column(length=2000)
		private String content;
		
		//휴대폰번호
		@NotNull
		@Column(length=11)
		private String mobile;
		
		//유입채널
		private String channel;
		

		@NotNull
		@Column(length=5)
		private String zipcode;

		@NotNull
		private String address;

		private String addressDetail;
		
		@NotNull
		private String email;
		
		private int shareKaKaoCount;
		
		private int shareFacebookCount;
		
		//아이피주소
		@NotNull
		@Column(length=15)
		private String ipAddress;
		
		@CreationTimestamp
		@Temporal(TemporalType.TIMESTAMP)
		@Column(updatable=false)
		private Date createDate;
}
