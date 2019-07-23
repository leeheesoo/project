package com.cetaphilevent.model.core;

import java.util.Date;

import lombok.Data;
@Data
public class CoreSnsVO {
	private Long id;
	private String name;
	private String mobile;
	private int kakaotalkSharingCount;
	private int kakaostorySharingCount;
	private int facebookSharingCount;
	private String channel;
	private String ipAddress;
	private Date createdDate;
	private Date updateDate;
}
