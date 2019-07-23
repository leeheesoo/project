package com.cetaphilevent.service.core;

import java.util.List;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.model.core.FandomVO;
import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.util.EventServiceException;

@Service
public interface CoreFandomService {
	FandomDao createEntry(FandomDao entry);
	
	boolean existUser(String phone);
	
	List<FandomDao> getFandomEntryList();
}
