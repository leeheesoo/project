package com.cetaphilevent.service.core;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import com.cetaphilevent.model.body.dao.BodyAgeUserDao;
import com.cetaphilevent.model.core.FandomDao;
import com.cetaphilevent.model.core.FandomVO;
import com.cetaphilevent.model.core.CoreSnsDao;
import com.cetaphilevent.repository.caribbean.CaribbeanEntryRespository;
import com.cetaphilevent.repository.core.FandomRepository;
import com.cetaphilevent.repository.core.CoreSnsRepository;
import com.cetaphilevent.util.EventServiceException;

@Service
public class CoreFandomServiceImpl implements CoreFandomService{
private Logger logger = LoggerFactory.getLogger(getClass());
	
	@Autowired
	private FandomRepository repository;
	
	@Autowired
	private CoreSnsRepository snsRepository;
	@Override
	public FandomDao createEntry(FandomDao entry) {
		// TODO Auto-generated method stub
		return repository.save(entry);
	}
	
	@Override
	public boolean existUser(String mobile) {
		return repository.findByMobile(mobile) != null;
	}

	@Override
	public List<FandomDao> getFandomEntryList() {
		return repository.findAll(sortByIdDesc());
	}
	
	private Sort sortByIdDesc() {
        return new Sort(Sort.Direction.DESC, "id");
    }
}
