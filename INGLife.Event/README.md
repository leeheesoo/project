# INGLife.Event
ING 생명보험 이벤트 웹사이트 입니다.

## Website URL
https://www.orange-event.kr


## Admin URL
https://www.orange-event.kr/manager


## 테스트 서버
https://test.www.orange-event.kr


## hosts 설정
```
127.0.0.1 dev.www.orange-event.kr
```

## TCP Port / ID
- 1

## Jenkins
- 직접배포


## 배포 및 운영서버 정보
- 웹서버: 61.111.18.88 (KMC 본인확인서비스 모듈 설치됨)
- DB서버: 61.111.18.75 (LG U+ 메시징 모듈 설치됨)
- 유미영대리에게 문의


## DBMS (maria DB)
- local 확인용(pentacle test server): dev-testserver.pentacle.co.kr (Database Name : INGLife.Event)
- test server : 61.111.18.75 (Database Name : INGLife.Event.Test)
- real server : 61.111.18.75 (Database Name : INGLife.Event)


## S3

## 의존성 프로젝트
- 구글 애널리틱스 분석 api : 관리자페이지에 구글 애널리틱스 통계 데이터(pagepath별 pv/uv) 표출


## ING생명 작업 관련 모듈 설치 방법 Confluence에서 확인
- https://pentacle.atlassian.net/wiki/spaces/inglife/overview
