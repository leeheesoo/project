package com.cetaphilevent.repository.core;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.core.FandomDao;

@Repository
public interface FandomRepository extends JpaRepository<FandomDao, Long>{
	FandomDao findByMobile(String mobile);
}
