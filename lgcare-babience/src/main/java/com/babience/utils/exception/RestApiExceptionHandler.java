package com.babience.utils.exception;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.core.annotation.Order;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.validation.BindException;
import org.springframework.validation.BindingResult;
import org.springframework.validation.FieldError;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;
import org.springframework.web.context.request.WebRequest;

@RestControllerAdvice({"com.babience.nickname.controller.api", "com.babience.survey.controller.api", "com.babience.quiz.controller.api"})
public class RestApiExceptionHandler {
	private Logger logger = LoggerFactory.getLogger(RestApiExceptionHandler.class);
	 
	//	ModelAttribute Validation 
	@ExceptionHandler(value = BindException.class)
	public ResponseEntity<?> restApiBindException(BindException e, WebRequest request) {		
		ResponseExceptionVo model = new ResponseExceptionVo();
		model.setStatus(HttpStatus.BAD_REQUEST.toString());
		model.setError(sortBindingReulstbyOrderAnnotation(e.getBindingResult()));
		model.setTimestamp(new Date());
		model.setPath(request.getDescription(false));
		
		return new ResponseEntity<>(model,HttpStatus.BAD_REQUEST);
	}
	
	//	RequestBody Validation
	@ExceptionHandler(value = MethodArgumentNotValidException.class)
	public ResponseEntity<?> restApimethodArgumentNotValidException(MethodArgumentNotValidException e,WebRequest request) {
		ResponseExceptionVo model = new ResponseExceptionVo();
		model.setStatus(HttpStatus.BAD_REQUEST.toString());
		model.setError(sortBindingReulstbyOrderAnnotation(e.getBindingResult()));
		model.setTimestamp(new Date());
		model.setPath(request.getDescription(false));
	
		return new ResponseEntity<>(model,HttpStatus.BAD_REQUEST);
	}
	
	//	Entity Validation
//	@ExceptionHandler(value = ConstraintViolationException.class)
//	public ResponseEntity<?> restApiConstraintViolationException(ConstraintViolationException e,WebRequest request) {
//		Object[] data = e.getConstraintViolations().toArray();
//		logger.debug("=======restApiConstraintViolationException=======");
//		e.printStackTrace();
//		
//		ResponseExceptionModel model = new ResponseExceptionModel();
//		model.setStatus(HttpStatus.BAD_REQUEST.toString());
//		model.setError(((ConstraintViolationImpl<?>)data[0]).getMessageTemplate());
//		model.setTimestamp(new Date());
//		model.setPath(request.getDescription(false));
//		
//		return new ResponseEntity<>(model,HttpStatus.BAD_REQUEST);
//	}
	
	@ExceptionHandler(value= AccessDeniedException.class)
	public ResponseEntity<?> restApiAccessDeniedException(AccessDeniedException e,WebRequest request) {
		logger.debug("=======restApiAccessDeiedException=======");
		e.printStackTrace();
	
		ResponseExceptionVo model = new ResponseExceptionVo();
		model.setStatus(HttpStatus.FORBIDDEN.toString());
		model.setError(e.getMessage());
		model.setTimestamp(new Date());
		model.setPath(request.getDescription(false));
		
		return new ResponseEntity<>(model,HttpStatus.FORBIDDEN);
	}
	
	@ExceptionHandler(value = EventServiceException.class)
	private ResponseEntity<?> kabritaServeryServiceExceptionHandler(EventServiceException e,WebRequest request){
		
		ResponseExceptionVo model = new ResponseExceptionVo();
		model.setStatus(HttpStatus.BAD_REQUEST.toString());
		model.setError(e.getMessage());
		
		return new ResponseEntity<>(model,HttpStatus.BAD_REQUEST);
	}
	
	@ExceptionHandler(value = Exception.class)
	public ResponseEntity<?> restApiException(Exception e,WebRequest request) {	
		logger.info("=======restApiAllException=======");
		e.printStackTrace();
		logger.info("=======END=======");
		ResponseExceptionVo model = new ResponseExceptionVo();
		model.setStatus(HttpStatus.INTERNAL_SERVER_ERROR.toString());
		model.setTimestamp(new Date());
		model.setError("서비스 점검중입니다. 잠시 후 시도해보시거나, 담당자에게 문의하시기 바랍니다.");
		model.setPath(request.getDescription(false));
		
		return new ResponseEntity<>(model,HttpStatus.INTERNAL_SERVER_ERROR);
	}
	
	  public String sortBindingReulstbyOrderAnnotation(BindingResult bindingResult) {
	        Class<? extends Object> clazz = bindingResult.getTarget().getClass();

	        List<FieldError> fieldErrors = new ArrayList<>(bindingResult.getFieldErrors());

	        if(fieldErrors.size() <= 0) {
	        	return bindingResult.getAllErrors().get(0).getDefaultMessage();
	        }else {
	        	fieldErrors.sort((fe1, fe2) -> {
		            try {
		                Order o1 = clazz.getDeclaredField(fe1.getField()).getAnnotation(Order.class);
		                Order o2 = clazz.getDeclaredField(fe2.getField()).getAnnotation(Order.class);

		                if (o1 == null && o2 == null) {
		                    return 0;
		                }
		                if (o1 == null) {
		                    return 1;
		                }
		                if (o2 == null) {
		                    return -1;
		                }
		                return o1.value() > o2.value() ? 1 : o1.value() == o2.value() ? 0 : -1;
		            } catch (NoSuchFieldException | SecurityException e) {
		                e.printStackTrace();
		                return 0;
		            }
		        });
		        
		        return fieldErrors.get(0).getDefaultMessage();	        	
	        }
	  }
}
