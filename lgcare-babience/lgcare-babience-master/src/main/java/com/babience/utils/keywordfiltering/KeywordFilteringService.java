package com.babience.utils.keywordfiltering;

import org.springframework.stereotype.Service;

@Service
public interface KeywordFilteringService {
	Boolean isMatchKeyword(String keyword);
}
