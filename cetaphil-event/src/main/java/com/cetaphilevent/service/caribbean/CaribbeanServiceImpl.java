package com.cetaphilevent.service.caribbean;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.caribbean.CaribbeanEntry;
import com.cetaphilevent.repository.caribbean.CaribbeanEntryRespository;

@Service
public class CaribbeanServiceImpl implements CaribbeanService{
private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private CaribbeanEntryRespository repository;

	@Override
	public CaribbeanEntry createEntry(CaribbeanEntry entry) {
		return repository.save(entry);
	}
	
	@Override
	public CaribbeanEntry getEntryById(Long id) {
		return repository.getOne(id);
	}
	
	@Override
	public List<CaribbeanEntry> getEventList() {
		return repository.findAll();
	}
}
