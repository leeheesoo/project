package com.cetaphilevent.service.core;

import java.util.List;

import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.util.EventServiceException;

@Service
public interface CoreSnsService {
	Long fandomSnsUserSave(CoreSnsDao entry);
	
	void snsCounterUpdate(Long userId, String snsType) throws EventServiceException;
	
	List<CoreSnsDao> getSnsEntryList();
    
}
