## 게임 플레이 영상
[https://www.youtube.com/watch?v=Vi8njVpfxWo](https://www.youtube.com/watch?v=2rZyxrSrX4w)
# 🐻 Project JumpBear

**"점프하고, 먹고, 등반하라!"**  
Unity 기반 3D 생존 액션 게임

---

## 🎮 게임 소개

**Project JumpBear**는 곰 캐릭터를 조작해 다양한 환경을 탐험하며,  
아이템과 장비를 활용해 능력을 강화하고 장애물을 돌파하는  
**점프 중심 생존 액션 게임**입니다.

- 🍖 **소비형 아이템 시스템**  
  호박(점프력 강화), 연어(체력 회복) 등 효과를 가진 아이템을 즉시 사용 가능  
  → Coroutine 기반 지속 효과, ScriptableObject 데이터 연동 구조  
  → **ItemEffectType enum 기반 효과 처리로 유지보수성 강화**

- 🧰 **장비 시스템**  
  Raycast로 세계에 놓인 장비를 인식하고 장착 가능  
  골드를 소비하여 능력 강화 (예: 이동 속도 증가), E 키 기반 상호작용  
  → **장착 시 능력치 자동 보정: 이동 속도, 점프력, 낙하 데미지 반영**

- 🪧 **상호작용 조사 시스템**  
  Raycast 기반 조사 UI로 세계 오브젝트 탐색  
  → InspectableData, TMP 폰트 연동으로 자연스러운 로컬라이징 지원

- 🧱 **점프대 및 플랫폼 시스템**  
  물리 반응 기반 점프대 / 움직이는 플랫폼 구현  
  DeltaPosition 방식으로 미끄러짐 방지

- 💰 **골드 획득 및 소비 시스템**  
  고도 상승 등 행동에 따라 자동 골드 획득  
  OnGoldChanged 이벤트 기반 UI 자동 반영

- 🪜 **벽 타기 및 매달리기 기능**  
  벽 감지 후 중력 해제 및 수직 이동 가능  
  → 부드러운 입력 반응과 점프 연계 가능

- 🕹 **부드러운 애니메이션 흐름**  
  점프·달리기 상태에 따른 Animator 연동  
  상태 기반 전환 및 Exit Time 설정으로 깜빡임 방지

- 📦 **풀링 기반 아이템 리스폰 시스템**  
  오브젝트 풀 구조 및 리스폰 매니저를 통한 메모리 효율적 재사용

- 🧠 **입력/효과/데이터 캐싱 최적화**  
  `InputSystem`, `ItemEffect`, `ActionMap` 등을 캐싱 방식으로 구조 최적화  
  → 반복 탐색 제거로 성능 향상

- 📄 **문서화 기반 개발 문화**  
  모든 주요 스크립트에 XML 주석을 도입하여 구조적 이해와 팀 협업을 용이하게 함

---

> **기능이 증가할수록 SRP(Single Responsibility Principle)를 바탕으로**  
> 입력, 애니메이션, 물리 처리, UI 등 기능을 모듈화하여 유지보수성과 확장성을 강화했습니다.

---

## 🚧 향후 계획 (업데이트됨)

- [x] 장비 능력치 적용 (예: 이동 속도 +10, 점프력 +3 등)
- [x] 아이템 효과 분기 구조 개선 (enum 기반 `ItemEffectType` 적용)
- [x] 주요 시스템 XML 주석 정비 완료
- [x] 레이저 트랩 구현 및 시각화 처리 (LineRenderer 기반)
- [ ] 버프/디버프 HUD 시각화 및 지속 시간 표시
- [ ] 인벤토리 시스템 재설계 (UI 슬롯 연결 구조 개선 후 재구축 예정)
- [ ] 난이도 상승 구조 설계 (예: 고도에 따른 보상/위험 증가)
- [ ] 미니게임 및 도전과제 시스템 추가
- [ ] Checkpoint 및 SavePoint 기능 도입

---

## 👨‍💻 개발자 노트 (업데이트됨)

- ✅ **구조적 설계 우선**
  - SRP(Single Responsibility Principle)를 바탕으로 `Input`, `Movement`, `Animation`, `Effect`, `Interaction` 시스템 분리
  - `EquipmentHandler`에서 장비 능력치 합산 → 각 시스템에 주입 적용

- ✅ **문서화 및 주석 중심 개발**
  - 전 스크립트에 XML 주석 추가 (`<summary>`, `<param>`, `<returns>`)
  - 구조 흐름과 역할, 사용법을 명확히 주석 처리하여 협업 가독성 향상

- ✅ **에디터 친화적 구조 설계**
  - `ScriptableObject`로 아이템, 인스펙트 데이터, 장비 스탯 등 분리
  - `Enum` 기반 구조로 오타 방지 및 타입 안전성 확보

- ✅ **트러블슈팅 기반 개선 문화**
  - 기존 `"호박"`, `"연어"` 등의 문자열 분기 → `ItemEffectType` 기반 구조로 리팩토링
  - `InputSystem`의 `FindAction` 반복 호출 → 캐싱 구조로 변경
  - `LineRenderer` 누락 및 `dealDamage()` 경고 제거 등 디버깅 처리 완료
  - 롤백 가능한 커밋 전략으로 UI 구조 충돌 시 빠른 복원 가능

- ✅ **UX 품질 향상 시도**
  - 레이저 트랩 시각화 및 플랫폼 동작 개선
  - 상호작용 프롬프트, 아이템 이펙트 등 시각적 피드백 강화
  - `World Space Canvas` + `Billboard` 구조로 직관적인 UI 제공

---
---

🛠 트러블 슈팅 모음
# 📅 2025년 5월 16일 트러블슈팅 로그

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
# 🛠 2025-05-17 트러블 슈팅 로그

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
## 📅 2025-05-19 트러블슈팅 로그

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
---
---
변경 요약
## 📅 2025-05-20 트러블슈팅 로그

### 🔁 1. 점프 및 대시 처리 리팩토링
- **기존 문제점**: `FixedUpdate` 내에서 점프와 대시 처리 로직이 길고 복잡하여 유지보수가 어려움.
- **개선 내용**:
  - `HandleJump()`, `HandleDash()` 메서드로 분리하여 SRP 원칙 적용.
  - 방향 벡터 계산 로직을 `GetMoveDirection()` 메서드로 통합하여 중복 제거.
- **장점**:
  - 각 기능별 책임이 분명해져 디버깅 및 확장 용이.
  - 카메라 축 회전 대응, 슬로우/버프 등 특수 조건 확장 기반 마련.

### ✂️ 2. 점프 시스템 분리 (PlayerJumpHandler 도입)
- **기존 문제점**: 이중 점프와 점프 버프 처리가 `PlayerMovement` 내부에 혼재.
- **개선 내용**:
  - `PlayerJumpHandler` 클래스를 생성하여 점프 전반의 책임을 위임.
  - `PlayerDoubleJump`에서 외부 상태 제어(`HasDoubleJumped`) 방식으로 변경.
  - `ItemUseHandler`에서 점프 버프 적용 시 `JumpHandler` 직접 참조하도록 구조 변경.
- **부가 수정**:
  - 오타 수정: `NotifyDshStart` → `NotifyDashStart`.
  - `input` → `inputHandler`로 통일하여 `NullReferenceException` 방지.

### 🧱 3. 플랫폼 탑승 시 위치 보정 기능 구현
- **기존 문제점**: 플레이어가 이동 플랫폼 위에서 미끄러지는 현상 발생.
- **개선 내용**:
  - `PlayerFollowPlatform.cs` 추가: `OnCollisionStay` 내 `DeltaPosition` 기반 보정 처리.
  - `MovingPlatform.cs`: 외부 참조를 위한 `DeltaPosition` 프로퍼티 제공.
  - `PlayerMovement.cs`: 애니메이션 판정 시 `IsGrounded` 제거하여 슬라이딩 방지.
- **관련 요소**:
  - `MainScene`, `JumpPad`, `TagManager`, 관련 리소스 및 메타 파일 반영.

## ✅ 결과 및 테스트 상태
- 플레이어가 플랫폼에 안정적으로 탑승하며, 대시/점프/애니메이션 전환이 자연스럽게 작동함.
- 이동 방향 계산 및 상태 전환 로직의 일관성 유지 확인됨.
- 디버그 로그와 커스텀 메시지로 의도한 동작 흐름 추적 완료.

## 📚 오늘의 교훈
- 입력과 물리 기반 로직은 명확하게 분리하고, SRP 기반의 컴포넌트 분산 설계가 유지보수에 유리하다.
- `DeltaPosition`을 활용한 플랫폼 보정은 `SetParent`보다 부작용이 적고 신뢰성 높음.
- 동작 처리 흐름을 핸들러로 위임하면 기능 확장/비활성화 조건 관리가 훨씬 깔끔해진다.

## 🧭 다음 목표
- 벽타기 및 매달리기 구현하기
- 장비 장착 구현(날개?)하기
- 레이저 트랩 구현하기
---
---
## 📅 2025-05-21 트러블슈팅 로그

## 📍 개요
이번 작업에서는 **장비 시스템, 골드 소비 로직, 벽 타기 기능**을 추가하는 과정에서 발생한 문제들을 분석하고 해결했습니다.

---

## ❗ 문제 상황

### [1] 장비가 플레이어에 붙긴 하지만, 위치/회전이 어색하고 바닥 오브젝트가 그대로 남아있음
- **원인:** 프리팹의 로컬 방향이 본 위치 기준과 맞지 않음. `Destroy(gameObject)` 호출도 적절하지 않음
- **영향:** 장비가 손에 비정상적으로 장착되며, 월드에 복제된 원본이 계속 남아 화면을 어지럽힘

### [2] E 키 입력 처리 로직이 `Input.GetKeyDown()`으로 되어 있었음
- **원인:** 프로젝트 전반이 `InputSystem` 기반인데, 해당 부분만 `Old Input` 사용
- **영향:** 입력이 작동하지 않거나 예외 발생 가능성 존재

### [3] 장비 장착 시 골드 차감 로직이 없었음
- **원인:** 장비 시스템은 존재하지만 경제 시스템과 연결되지 않음
- **영향:** 플레이어가 리소스 없이 무한 장비 장착 가능 (게임 밸런스 붕괴 위험)

---

## 🔍 시도한 해결

### ✅ 장비 위치 및 방향 정렬
- 장비 프리팹에 `Offset` 오브젝트 추가 → 부착 기준 맞춤
- `localPosition = Vector3.zero`, `localRotation = Quaternion.identity` 적용
- 바닥 오브젝트는 남겨두되, 추후 필요시 제거 가능하게 설계

### ✅ 입력 시스템 통합
- `PlayerInput.actions["Equip"]`를 사용한 장비 장착 처리
- `Keyboard.current.eKey.wasPressedThisFrame` → `InputAction.performed`로 대체

### ✅ 골드 시스템 연동
- `GoldSystem.Instance.SpendGold(goldCost)` 사용
- 장착 실패 시 메시지 출력 (`골드 부족`)
- `AddGold()`는 `HeightTracker`에서 최고 고도 갱신 시 자동 지급

---

## 🧩 추가 구현 요소

| 기능 | 설명 |
|------|------|
| 장비 프롬프트 | Raycast로 장비 인식 시 UIManager 통해 `"E 키로 장착"` 안내 |
| 벽 타기 | 벽 감지 후 중력 해제 + 입력에 따라 벽 위로 이동 |
| UI 연동 | 골드 텍스트 자동 갱신 (`OnGoldChanged` 이벤트 기반) |

---

## ✅ 최종 결과

- 플레이어는 E 키로 장비를 장착할 수 있으며, 골드가 부족하면 장착이 불가능함
- 골드는 게임 내 행동(높이 상승 등)에 따라 획득되고, 실시간 UI로 반영됨
- 장비 위치는 본에 정확히 부착되며, 추후 해제/교체를 위한 기반 구조가 갖춰짐
- 벽 타기는 작동하며, 물리 기반 점프로 벽을 튕겨나올 수 있음

---

## 🗂️ 관련 파일

- `Scripts/Items/EquipmentItem.cs`
- `Scripts/Items/EquipmentPickup.cs`
- `Scripts/Player/Systems/EquipmentHandler.cs`
- `Scripts/Player/Systems/EquipmentInteractor.cs`
- `Scripts/UI/UIManager.cs`
- `Scripts/Player/Systems/PlayerWallClimbHandler.cs`
- `Scripts/Gold/GoldSystem.cs`, `GoldUI.cs`, `HeightTracker.cs`

---

## 📌 다음 개선 과제

- 장비 능력치 반영 (ex. 스피드 +10)
- 레이져 트랩
- 플랫폼 발사기
---
---
# 🧯 2025년 5월 22일 트러블슈팅 & 리팩토링 기록

## 📛 문제 요약 (Problems)

### 1. 문자열 분기에 의존한 아이템 효과 처리
- `"호박"`, `"연어"` 등의 **하드코딩 처리**로 인해 새로운 아이템 추가 시마다 조건 분기 코드가 늘어났음
- 유지보수성과 확장성 모두 저하됨

### 2. 주요 시스템 주석 미흡
- 이동, 점프, 대시, 스태미나, 장비, 골드 등 **핵심 시스템의 문서화 부족**으로 팀 협업과 유지보수에 리스크

### 3. 장비 능력치가 실제 시스템에 반영되지 않음
- `EquipmentItem`의 능력치 필드가 있으나, **플레이어 이동/점프/낙하 등에 적용되지 않음**

### 4. LaserTrap 구조의 시각적 부재 및 경고
- 레이저 기능은 있었으나 **LineRenderer가 빠져 시각적 피드백 없음**
- `dealDamage()` 같은 **사용되지 않는 코드 존재** → 경고 메시지 다수 발생

### 5. Input 시스템 반복 탐색 비용 증가
- `PlayerInputHandler`가 `InputAction`을 매번 동적 탐색
- `FindAction()` 남용 → 퍼포먼스 저하 우려

---

## 🔧 해결 과정 (Attempts & Solutions)

### ✅ [A1] 문자열 분기 제거 → Enum 기반 처리
- `ItemEffectType` enum 도입 (Heal, JumpBoost, SpeedBoost, FallDamageReduction 등)
- `ItemUseHandler`에서 `switch-case`로 효과 처리 통합

### ✅ [A2] 주요 스크립트에 XML 주석 보완
- `/// <summary>` 주석 전면 도입
- 대상: `PlayerMovement`, `PlayerJumpHandler`, `EquipmentHandler`, `ObjectPool` 등 핵심 시스템
- 사용 목적, 흐름, 주의사항 등을 명확히 명시

### ✅ [A3] 장비 능력치 → 실시간 반영 구조 설계
- `EquipmentHandler`에 능력치 총합 계산 메서드 추가:
  - `GetTotalSpeedBonus()`
  - `GetTotalJumpBonus()`
  - `GetTotalFallDamageReduction()`
- 해당 보정값을 `PlayerMovement`, `JumpHandler`, `FallDamage`에 실제 반영

### ✅ [A4] LaserTrap 리팩토링
- `LineRenderer` 구성 및 머티리얼(`Raser.mat`) 적용
- 감지 주기 및 데미지 적용 정비
- 불필요한 `dealDamage()` 제거하여 경고 해소

### ✅ [A5] 입력 시스템 캐싱 구조로 개선
- `OnEnable` 시 `InputAction` 한 번만 캐싱
- `OnDisable`에서 안전하게 해제
- `EquipmentPickup` 또한 `equipAction` 캐싱 처리

---

## 💥 인벤토리 롤백 처리

> ⚠️ **Issue**: 슬롯 생성/연결 로직이 UI 계층 구조와 충돌 발생  
> 🧭 **Action**: 커밋 백업 후 구조 리셋 → 재설계 예정

---

## 📚 알게 된 점 (Knowledge)

- 문자열 조건은 잠깐 편리하지만, 결국 구조적 확장에 가장 큰 장애물이 됨
- 시스템 전반의 **명세화(XML 주석)**는 문서가 아닌 ‘실시간 가이드’가 된다
- 능력치/효과는 "데이터 보유"와 "적용 로직"이 반드시 분리되어야 함
- 기능이 아닌 ‘구조’를 먼저 잡는 것이 나중을 살린다

---

## 🏁 다음 작업 예정

- 코드 정리 및 주석처리
- 과제 제출 준비
