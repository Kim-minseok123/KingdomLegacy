# [Unity 2D] Turn-Based Card Game Portfolio 
## 목차
  - [소개](#소개) 
  - [개발 동기](#개발-동기)
  - [개발 환경](#개발-환경)
  - [사용 기술](#사용-기술)
  - [플레이영상](#플레이영상)
  - [게임 다운로드](#게임-다운로드)
    
## 소개
<div align="center">

<img alt="Title" src="https://github.com/user-attachments/assets/f5555353-dd17-4291-b2e8-8d9c35201300" width="49%" height="330"/>
<img alt="Start" src="https://github.com/user-attachments/assets/25c2a730-5a9c-4826-b813-e6912fcc2de5" width="49%" height="330"/>
<img alt="1Stage" src="https://github.com/user-attachments/assets/a83a8802-9fb7-4202-9e05-9386cb44dc23" width="49%" height="330"/>
<img alt="Menu" src="https://github.com/user-attachments/assets/32d8d3b7-b0f3-4867-bbd3-e8801d4619b7" width="49%" height="330"/>
<img alt="Shop" src="https://github.com/user-attachments/assets/3380376b-ee7c-45a8-9d66-640ce94695cf" width="49%" height="330"/>
<img alt="Battle1" src="https://github.com/user-attachments/assets/85e90cfc-4164-490b-a2c7-48d6ef4d6a2c" width="49%" height="330"/>
<img alt="Boss1" src="https://github.com/user-attachments/assets/1cb16232-4ce8-4790-8538-8cca06625f7c" width="49%" height="330"/>
<img alt="Battle2" src="https://github.com/user-attachments/assets/d0d154e4-829e-41b9-b2d1-d0f3030dc527" width="49%" height="330"/>
<img alt="Rest" src="https://github.com/user-attachments/assets/ca276619-6744-4551-aa17-5d5ea8d7a7cf" width="49%" height="330"/>
<img alt="Boss2" src="https://github.com/user-attachments/assets/12a85d4e-67d7-4b9a-9bb2-77f2a2799f72" width="49%" height="330"/>
<img alt="Boss3" src="https://github.com/user-attachments/assets/2e3cd9b4-ad89-4bfa-81ec-7ebb1e24ea98" width="49%" height="330"/>
<img alt="Encyclopedia" src="https://github.com/user-attachments/assets/a66c9822-064a-4ed5-bac7-2e360cd9632b" width="49%" height="330"/>

  < 게임 플레이 사진 >

</div>

+ Unity 2D 턴제 카드 게임입니다.

+ UI에 친숙해지고 집중적으로 연습하기 위한 포트폴리오입니다.

+ 오직 컨텐츠는 UI만 사용하여 개발 되었습니다. 

+ 현재 클라이언트는 유로 에셋으로 인해 일부만 공개되어 있습니다.

+ 개발 기간: 2023.08.14 ~ 2024.11.18 ( 약 3개월 )

+ 개발 인원 : 1인

+ 형상 관리: Git SourceTree

<br>

## 개발 동기
클라이언트 개발자로 성장하기 위해, 실제 게임 개발 환경에서 자주 마주치는 **UI 중심의 상호작용 구현 능력**을 집중적으로 연습하고자 했다.

이러한 목표에 가장 적합한 장르로, 다양한 상태 변화와 인터페이스 조작이 반복되는 **턴제 카드 게임**을 선택하게 되었다.

특히 Slay the Spire를 참고해 기획을 단순화하고, **모든 게임 요소를 Unity의 UI 시스템만으로 구현하는 것을 핵심 도전 과제**로 삼았다.  

카드 선택, 드로우, 에너지 시스템, 턴 관리, 전투 로그, 적의 의도 표시 등 **다양한 상호작용과 시각적 피드백을 필요로 하는 장르로**,  
- **UI로만 구성된 시스템**  
- **게임 상태에 따라 실시간으로 변하는 화면 구성**  
- **직관적인 유저 피드백을 주는 연출과 인터페이스 설계**  

와 같은 실무 중심의 개발 역량을 연습하기 위한 목적에서 출발했다.

복잡한 오브젝트 없이도 UI만으로 게임 구조를 설계하고 구현해보는 경험을 통해,  
클라이언트 개발자로서의 **UI 설계 감각과 구조적 사고**를 실전처럼 다져보고자 했다.
<br>

## 개발 환경
+ Unity 22022.3.11f1 LTS

+ Visual Studio 2022

<br>

## 사용 기술
### 🗺️ 맵 생성 시스템
- **그리드·레이어 기반 로그라이크 절차적 맵 생성 시스템 구현**  
  - 스테이지 설정에 따라 `MapConfig`를 로드하고, 레이어별 간격과 열 수를 기반으로 그리드 노드를 생성  
  - 노드 종류는 기본값 + 랜덤 치환 확률로 결정하고, 화면상 X 좌표는 등간격, Y 좌표는 누적 거리 합으로 배치

- **시작~보스 간 경로 자동 생성 알고리즘 작성**  
  - `Start → (Pre-Boss) → Boss`로 이어지는 주요 경로를 난수 기반으로 생성하고, 여분의 `extraPaths`를 추가하여 구조에 다양성을 부여  
  - 경로 탐색은 "다음 층으로 한 칼럼 이동(좌/정면/우)만 허용, 최대 기울기 1(45도)" 조건으로 구성

- **시각 혼선 제거 및 연결 처리**  
  - 각 노드는 `incoming/outgoing` 리스트로 연결 상태를 구성, 2×2 셀 내 교차선은 자동 감지하여 수평·수직으로 치환하거나 제거  
  - ±1칸 내 무작위 위치 오프셋을 부여하여 정형 그리드 느낌 완화

- **최종적으로 연결된 노드만 남기고 Boss blueprint를 적용한 Map 객체를 반환**  
  - 해당 객체는 렌더링, 저장, 플레이어 이동 등에 직접 사용

### 🃏 카드 시스템
- **카드 액션 및 사용 조건을 인터페이스 기반으로 통합 설계**  
  - 모든 카드는 `ICardAction`, `ICardCondition` 등의 인터페이스를 구현해 구조화  
  - 새로운 카드를 추가할 때 기능만 정의하면 적용될 수 있도록 설계

### 🧩 UI 시스템
- **모든 컨텐츠를 UI로 구성하고, UI Manager에서 스택 방식으로 제어**  
  - UI 프리팹은 `UI Manager`가 스택으로 관리하며, 스폰 및 렌더링 순서를 일괄 조절  
  - 화면 전환, 패널 중첩, 입력 처리 등을 일관된 방식으로 처리해 유지보수 용이
    
### 📄 데이터 관리
- **XML 파일 기반 데이터 관리 및 로딩 시스템 구현**  
  - 카드 정보, 효과 등의 게임 데이터를 XML 파일로 분리해 관리 
  - XML 파싱을 통해 런타임 중 데이터를 동적으로 로드할 수 있도록 구성
  - 코드 수정 없이 외부 파일만 수정해도 밸런싱 조정이나 콘텐츠 확장이 가능하도록 설계

### 💾 저장 기능
- **세이브/로드 시스템 구현**  
  - 플레이어 상태, 덱 구성, 진행 상황 등을 저장하고 불러올 수 있도록 구성 
  - 중간에 종료하더라도 이어서 플레이가 가능하도록 설계

<br>

## 플레이영상
https://youtu.be/Z_0KZKLjm_U

## 게임 다운로드
https://drive.google.com/drive/folders/1CFE4nKiHjfKAKDESpc4CLE421BYSKfDV?usp=sharing


