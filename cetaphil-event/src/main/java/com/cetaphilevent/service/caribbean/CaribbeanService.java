package com.cetaphilevent.service.caribbean;

import java.util.List;

import org.springframework.stereotype.Service;

import com.cetaphilevent.model.caribbean.CaribbeanEntry;

@Service
public interface CaribbeanService {
	CaribbeanEntry createEntry(CaribbeanEntry entry);
	CaribbeanEntry getEntryById(Long id);
	List<CaribbeanEntry> getEventList();
}
