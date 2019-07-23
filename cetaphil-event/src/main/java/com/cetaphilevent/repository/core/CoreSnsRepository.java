package com.cetaphilevent.repository.core;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.cetaphilevent.model.core.CoreSnsDao;

@Repository
public interface CoreSnsRepository extends JpaRepository<CoreSnsDao, Long>{
}
