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

- [ ] 오브젝트 상호작용 애니메이션
- [ ] 버프/디버프 UI 표시
- [ ] 도전기능 하나씩 추가
- [ ] 높아질수록 높은 난이도 구현

---

## 👨‍💻 개발자 노트

- 구조화된 코드 작성(SOLID)과 역할 분리를 기반으로 유지보수에 집중  
- 테스트용 Mock 데이터는 ScriptableObject로 관리 중  
- TIL 기반 학습 및 트러블슈팅 문서화를 병행

---
---

🛠 트러블 슈팅 모음
# 📅 2025년 5월 16일 트러블슈팅 로그그

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
# 🛠 2025-05-17 트러블 슈팅 로그그

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
## 📅 2025-05-18 트러블슈팅 로그

---

### 🛠 주요 정비 항목 요약

#### 📘 `docs(comment)`: 스크립트 주석 정비 및 책임 명시
- `Enum.cs`, `ItemData.cs`, `InspectableData.cs` 등 ScriptableObject 및 Enum 클래스에 역할 주석 추가
- `PlayerMovement`, `PlayerController`, `PlayerFallDamage` 등 주요 플레이어 로직에 책임 기반 설명 주석 보완
- `UIManager`, `JumpPad`, `ItemPickup` 등 기능 스크립트에 흐름 중심 주석 삽입
- 전체적으로 코드 문서화 수준 향상 및 팀원 협업 가독성 개선 목적

---

### 🔄 `feat(item-pool)`: 아이템 풀링 시스템 및 소비형 아이템 통합 구현

#### 🔧 주요 구현 내용
- `IPoolable` 인터페이스 도입 → 풀 객체에게 소속 풀 정보 주입 (`SetPool`)
- `ObjectPool` 클래스 생성 → 풀 내부 Queue 구조 및 동적 생성 처리
- `PoolManager` 확장 → `PoolType` 기반 등록 / 요청 / 반환 통합 관리
- `ItemPickup.cs`에서 `Player`와 충돌 시, `ItemUseHandler.UseItem()` 호출 후 **풀 복귀 or 재활성화 대기**

#### ✅ 적용 사례
- `JumpPumkin` → 점프력 증가 (버프)
- `HealFish` → 체력 회복 (힐)

---

### 💡 `feat(item)`: 소비형 아이템 효과 정리 및 통합

- `ItemData`에 다음 필드 추가:
  - `float healAmount`
  - `float jumpBoost`
  - `float duration`

- `ItemUseHandler.cs`에서 `"호박"`은 `ApplyJumpBoost()` 호출  
- `"연어"`는 `PlayerHealth.Heal()` 호출로 체력 회복 처리

- `HealItem.cs` 제거 → 전용 스크립트 대신 **데이터 기반으로 효과 통합**

---

### 🔁 `feat(item-respawn)`: 아이템 리스폰 시스템 구현

- `ItemRespawner.cs` 도입 → 일정 시간 후 오브젝트 `SetActive(true)` 처리
- `ItemPickup.cs`에서 기존 `originPool.Return()` 제거 → 충돌 후 `ItemRespawner.Instance.RespawnAfterDelay(...)` 사용
- `MainScene`에 `ItemRespawner` 매니저 오브젝트 배치

---

### ⚠️ `refactor(fall)`: 낙하 데미지 기준 리팩토링

#### 기존 문제
- `previousYVelocity` 한 프레임 기준으로 착지 시점 판단
- 점프대 등에서 리셋되거나 타이밍상 데미지 미적용

#### 해결 방법
- `minYVelocity`로 **낙하 중 가장 빠른 속도 기록**
- `fallTime`으로 짧은 낙하 제외
- `speedThreshold` 기준 비교 → 데미지 = `abs(minYVelocity) * multiplier`
- `Debug.Log()`로 착지 속도 및 데미지 실시간 확인

---

## ✅ 최종 결과

- ✅ 아이템 효과 정상 작동 (호박 → 점프, 연어 → 체력 회복)
- ✅ 풀링 기반 구조 확립 → 메모리 절약 및 반복 사용 가능
- ✅ 낙하 데미지 기준 개선 → 점프대 등 복합 구조에서도 안정적 동작
- ✅ 상호작용 UI, 리스폰, 소비 로직 모두 일관된 구조로 정비됨

---

## 🧭 다음 목표

- [ ] 점프나 대쉬 등 특정 행동 시 소모되는 스태미나를 표시하는 바 구현
- [ ] 움직이는 플랫폼 구현
- [ ] 버프/디버프 HUD 시각화 및 타이머 표시
---
---
## 📅 2025-05-18 트러블슈팅 로그

## 🚩 이슈 개요
**PlayerController.cs 리팩토링 도중 NullReferenceException 발생**

SRP(Single Responsibility Principle)를 기반으로 구조 개선을 시도하며  
`PlayerMovement`, `PlayerJump`, `PlayerDash`, `PlayerLook` 등을 별도 컴포넌트로 분리했으나  
**의존성 주입과 초기화 순서**의 문제로 인해 다수의 `NullReferenceException`이 발생함.

---

## 🧩 문제 상세

| 항목 | 내용 |
|------|------|
| 발생 위치 | `PlayerController.cs`, `PlayerMovement.cs`, `PlayerDash.cs` 등 |
| 원인 | `GetComponent` 호출 시점이 `Awake`보다 느리거나, 종속성 주입 누락 |
| 의도 | 단일 책임 원칙에 따라 Player 컨트롤 로직 분리 |
| 증상 | 점프, 대쉬, 회전 등 일부 기능 비정상 작동 또는 Null 오류 발생 |

---

## 🔍 시도한 해결 과정

- `PlayerController`에서 서브 컴포넌트 전부 명시적 초기화 시도 (`Awake → Start` 순서 점검)
- `RequireComponent`와 `SerializeField` 병행 사용으로 의존성 해결 시도
- 중복으로 참조되던 `Player.cs` 내 컴포넌트 참조 제거
- `PlayerInputHandler`에서 입력 통합 처리 시도 → 내부 공유 방식 충돌로 보류
- **결국 리팩토링 브랜치 폐기, 기존 커밋으로 롤백 처리**

---

## ✅ 최종 구조 및 조치

- `PlayerController.cs` → 기능 분기용 허브로 **최소화**
- 기능별 스크립트를 다음과 같이 **폴더별 정리**  
  - `Systems/` → StaminaSystem, GroundChecker 등  
  - `Abilities/` → PlayerDash, PlayerDoubleJump 등  
  - `States/` → PlayerState, GroundChecker 등  
  - `View/` → StaminaBarUI 등
- 구조는 유지하고 **기능 정상 작동 확인 완료**

---

## 💡 교훈 및 인사이트

- ✅ **리팩토링 전 설계 명확화 필수**  
  - 특히 컴포넌트 간 종속성이 복잡한 경우, 설계 없이 리팩토링 시 오히려 기능 파괴 가능성 ↑

- ✅ **동작하는 시스템은 가급적 점진적 개선으로 접근할 것**  
  - 전면 구조 변경은 매우 높은 리스크 동반

- ✅ **Rollback은 패배가 아님**  
  - 실험의 일부로 받아들이고, 실패에서 얻은 통찰은 구조 개선에 장기적으로 기여함
