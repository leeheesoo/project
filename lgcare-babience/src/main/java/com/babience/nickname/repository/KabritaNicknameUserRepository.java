package com.babience.nickname.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.babience.nickname.dao.KabritaNicknameUser;

@Repository
public interface KabritaNicknameUserRepository extends JpaRepository<KabritaNicknameUser, Long> {	
	/* 이벤트 */
	List<KabritaNicknameUser> findTop60ByIsShowTrueOrderByCreatedDateDesc();
}
 