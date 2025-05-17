## 게임 플레이 영상
https://www.youtube.com/watch?v=Vi8njVpfxWo
# 🐻 Project JumpBear

**"점프하고, 먹고, 진화하라!"**  
Unity 기반 3D 생존 액션 게임

---

## 🎮 게임 소개

**Project JumpBear**는 곰 캐릭터를 조작해 다양한 환경을 탐험하며,  
아이템을 사용해 능력을 일시적으로 강화하고 장애물을 돌파하는  
**점프 중심 생존 게임**입니다.

- 🍖 **소비형 아이템 시스템**: 점프력을 강화하는 고기를 즉시 사용 가능
- 🪧 **상호작용 시스템**: Raycast 기반 조사 UI로 세계관 오브젝트 탐색
- 🧱 **점프대 오브젝트**: 물리 반응 기반 점프 강화 구조물
- ✨ **한글 폰트 및 UI 구성**: TMP 폰트 적용으로 자연스러운 로컬라이징
- 🕹 **부드러운 애니메이션 흐름**: 점프·달리기 상태에 따른 Animator 연동

---

## ⚙️ 개발 스택

- **엔진**: Unity (2022+)
- **언어**: C#
- **UI**: TextMeshPro 기반
- **기술**: ScriptableObject, Raycast, Coroutine, Rigidbody Physics

---

## ✅ 현재 구현 상태

- [x] 플레이어 기본 이동 및 점프
- [x] 점프 애니메이션 연동
- [x] 점프대 물리 반응
- [x] 소비 아이템 사용 및 지속 효과
- [x] 상호작용 조사 UI
- [x] 한글 폰트 대응

---

## 🚧 향후 계획

- [ ] 무기 시스템 및 전투 애니메이션
- [ ] 버프/디버프 UI 표시
- [ ] 몬스터 및 보스 등장
- [ ] 스테이지 기반 성장 구조

---

## 👨‍💻 개발자 노트

- 구조화된 코드 작성(SOLID)과 역할 분리를 기반으로 유지보수에 집중  
- 테스트용 Mock 데이터는 ScriptableObject로 관리 중  
- TIL 기반 학습 및 트러블슈팅 문서화를 병행

---
---

🛠 트러블 슈팅 모음
# 📅 2025년 5월 16일 트러블슈팅 기록

## 🎮 Unity 3D 생존 프로젝트 - 플레이어 시스템 리팩토링

### 🚧 문제 상황

- **PlayerController.cs에 과도한 기능 집중**
  - 이동, 점프, 회전, 낙하 데미지, 입력 처리 등 모든 로직이 하나의 클래스에 몰려 구조가 비대하고 유지보수가 어려웠음.

- **점프 입력 무반응**
  - `JumpPressed` 값은 true였으나 실제로 점프가 발생하지 않음.
  - 콘솔 로그 상 조건은 만족하였으나, `AddForce()` 호출이 실행되지 않음.

- **카메라 회전이 작동하지 않음**
  - `LookInput`은 정상적으로 입력되고 있었으나,
  - 실제 회전에 적용되는 값은 `mouseDelta`라는 잘못된 값으로 고정되어 있었음.

---

### 🛠 해결 방법

#### ✅ **기능 분리 및 리팩토링**
- **입력 처리 / 실행 로직 분리**
  - `PlayerInputHandler.cs`: 모든 입력 상태를 저장
  - `PlayerController.cs`: 입력 라우팅만 담당하도록 축소
- **행동별 기능 분할**
  - `PlayerMovement.cs`: 이동 및 점프 처리
  - `PlayerLook.cs`: 마우스 기반 카메라 회전
  - `PlayerFallDamage.cs`: 낙하 속도 기반 데미지 계산
  - `GroundChecker.cs`: 접지 여부 확인 전용 유틸리티

#### ✅ **UI 구조 개선**
- 플레이어 상단에 `World Space Canvas` 적용
- `Billboard.cs`를 통해 항상 카메라를 바라보도록 체력바 처리

#### ✅ **입력 이벤트 구조 개선**
- `PlayerInput`의 모든 액션 이벤트는 `PlayerInputHandler`에 연결
- `PlayerController`는 오직 해당 핸들러를 통해 간접 참조

---

### ✅ 해결 결과

- 점프, 이동, 마우스 회전, 낙하 데미지, HP UI 정상 작동
- 각 스크립트가 명확한 책임을 가지도록 구조 개선됨
- 유닛 테스트 및 향후 기능 추가(대시, 더블 점프 등)가 훨씬 용이해짐

---

### 💡 느낀 점

- 기능이 많아질수록 **단일 책임 원칙(SRP)**을 지키는 것이 필수
- Unity의 Input System은 입력과 처리를 분리하면 관리가 쉬워짐
- `Debug.Log()`로 입력 흐름을 추적하는 습관이 매우 중요
- 체력 UI는 월드 스페이스 + Billboard 방식이 가장 직관적이고 안정적

---

> ⛳ 다음 목표: 필수 기능 구현하기
---
---
# 🛠 2025-05-17 트러블 슈팅 요약

## 1. 낙하 데미지 적용 안됨  
- **문제**: 추락해도 데미지 없음  
- **해결**: `PlayerController.FixedUpdate()`에서 `CheckFallDamage()` 직접 호출

## 2. 조사 UI 작동 안함  
- **문제**: 생선 조사해도 UI 안 뜸  
- **해결**:  
  - `BoxCollider` 추가  
  - Raycast 기준을 `CameraContainer.forward`로 수정  
  - `InspectableObject` & `InspectableData` 구조 도입

## 3. TMP 한글 출력 오류  
- **문제**: 설명에 한글이 안 나옴  
- **해결**:  
  - `Black Han Sans` 폰트 적용  
  - TMP Font Asset 생성 후 연결

## 4. 점프대 무반응  
- **문제**: 플레이어가 밟아도 튕기지 않음  
- **해결**:  
  - `OnCollisionEnter()`에 `CompareTag("Player")` 추가  
  - `AddForce(..., ForceMode.Impulse)` 적용

## 5. 아이템 효과 즉시 종료  
- **문제**: 점프력 버프가 바로 사라짐  
- **해결**: `ApplyJumpBoost()`에 Coroutine으로 지속 시간 처리

## 6. 애니메이션 재생 안됨  
- **문제**: Jump, Run 애니메이션 반응 없음  
- **해결**:  
  - `PlayerAnimator` 분리  
  - 파라미터 이름 정확히 일치  
  - Animator 수동 연결

## 7. 애니메이션 너무 빨리 꺼짐  
- **문제**: 점프 애니메이션이 깜빡임  
- **해결**: `Has Exit Time` 및 `Transition Duration` 조정

---

## ✅ 결과 요약

- 상호작용, 폰트, 소비형 아이템, 점프대, 애니메이션 모두 정상 작동  
- 책임 분리 (`PlayerMovement` ↔ `PlayerAnimator`)로 구조 개선  
- ScriptableObject 및 Enum 기반 확장 가능 설계 완료
---
---
