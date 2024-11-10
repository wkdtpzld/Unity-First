# 유니티 2D 튜토리얼 기록용 커밋

## 목차

1. Unity 세팅 && Vscode 세팅
2. Colliders, Rigibody
3. Script
4. [SerializeField]
5. Sprite Sheet
6. Animator
7. 2D Collision Detection
8. C# inheritance


## 1. Unity 세팅 && Vscode 세팅

Unity 다운로드는 스킵.

Vscode 를 다운로드 하고 Unity 와 Vscode 연동을 시켜줘야 했다.

### 1.1 Mono 설치

https://www.mono-project.com/download/stable/

### 1.2 .NET SDK 설치

https://dotnet.microsoft.com/en-us/download

### 1.3 Vscode Extension 

![image](https://github.com/user-attachments/assets/1161cdbc-e3f1-443d-8484-a829414b10d1)
![image](https://github.com/user-attachments/assets/f16c3211-61a7-4ad1-b619-a62417474504)

나머지는 취향대로 다운받아도 될 듯 하다?

### 1.4 Unity Vscode 연동

### 1.4.1 

![image](https://github.com/user-attachments/assets/77b036a9-af5d-4a93-8e34-c5f95389cc3a)

### 1.4.2

![image](https://github.com/user-attachments/assets/ca4fa6cf-4134-4848-8d9e-0df8f27b7977)


## 2. Colliders, Rigibody 

![image](https://github.com/user-attachments/assets/25c63b18-68c1-415f-8196-fecdab3b48c0)

플레이어를 만들어서 기본적인 바디를 만들고 Colliders 를 만들어 오브젝트 충돌 범위를 설정해주었다.

Rigibody2D 에서는 다음과 같은 부분에 대한 옵션을 알아봤다.

![image](https://github.com/user-attachments/assets/1b2c14a3-65a6-422d-ad2e-d947f91571a5)

위에서부터 Material 은 벽과 충돌하였을 때 미끄러지지 않고 그대로 벽에 붙은 상태로 끝나기 떄문에 추가하여 벽의 미끄러움을 구현하였다.

![image](https://github.com/user-attachments/assets/1311416f-2b0d-4f60-8d13-d693b1422c02)

각각 미끄러움 / 탄성 / 마찰 조합 방식 / 튕김 조합 방식 이다.

### 2.1 Mass - 질량

### 2.2 Linear Damping

선형 속도를 감소시키는 정도를 제어할 수 있다.
값이 높을수록 움직임이 빨리 멈춘다.

### 2.3 Angular Damping

회전 속도를 감쇠시키는 정도를 제어 할 수 있다.

값이 높을수록 회전이 더 빠르게 멈춘다.

### 2.4 Gravity Scale

중력의 크기를 조정

### 2.5 Collision Detection

RigidBody 2D가 오브젝트아의 충돌을 어떻게 감지할지. 선택할수 있는 셀렉박스가 나온다.

#### Discrete, Continuous, Continuous Speculative

점점 더 정교하게 충돌을 계산하지만 그만큼 성능에 영향을 준다.

### Sleeping Mode

Rigidbody 의 수면 상태를 설정할 수 있으며 수면을 어떻게 시킬지에 대한 셀렉 박스가 존재한다.

### Interpolate

Rigidbody 의 움직임을 부드럽게 보이도록 보간할 수 있다.

### Constraints

Rigidbody 의 움직임을 제어하거나 고정시킬 수 있다.
본인은 Z 축을 고정시킴으로서 오브젝트가 넘어지는 현상을 제어했다.

## 3. Script

Sciprt 를 생성하여 Unity 에 적용시키는 일이다.

기본적으로 MonoBehaviour 라는 객체를 선언시켜야 Unity 에서 반응하는 것 같다.

```c#
public class Entity : MonoBehaviour {
  ...

  void Start() {
  }

  void Update() {
  }
}
```

시작과, 매 시간마다 업데이트라는 함수가 실행된다.

## 4. SerializeField

Script 에서 public 으로 객체를 생성하면 유니티에서 그 값을 설정하거나 볼 수 있다.

하지만 public 으로 작성시 캡슐화가 되지 않으며 다른 객체에서 접근을 할 수 있는 안티패턴 코드가 작성되므로

직렬화를 통하여 유니티에서 볼 수 있으면서도 캡슐화를 구현할 수 있게 도와준다.

```c#
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
```

다른 유틸리티 도 있다.

```c#
[Header("Collision Info")]

[Space]
```

유니티에서 각각 섹션을 분리하는 용도로 사용할 수 있다.

![image](https://github.com/user-attachments/assets/649c5e22-2481-46c5-bc05-629a593e32d6)


## 5. Sprite Sheet

도트를 자를 수 있게 도와준다. 도트 파일을 가져오고 클릭을 해보면 이런 설정이 나오는데

여기서 Open Sprite Editor 를 클릭하여 설정 가능하다.

![image](https://github.com/user-attachments/assets/00259de6-9357-4d3d-97a1-dddb0f1eb106)

![image](https://github.com/user-attachments/assets/4f1adbeb-a460-4446-a173-c12c22e11a2f)

각각 몇 픽셀을 차지할지 등을 보면서 설정하면 좋다.

## 6. Animator

애니메이션을 만들때 굉장히 놀랐다. 고도 엔진에 비해서 개발자가 하는 작업이 적었다.

Animator 를 작성하야 각 RigidBody 에 종속시킨 다음. RigidBody 에 애니메이션 알고리즘을 작성할 수 있었다.

```c#
  protected Animator animator;

  
  protected virtual void Start()
  {
      animator = GetComponentInChildren<Animator>();
  }
```

![image](https://github.com/user-attachments/assets/0ff75c4e-5b9e-4806-8062-ac2bb5a8b05d)

애니메이터 + 애니메이션 창을 나오게 한 후 각 애니메이션에 어떤 액션을 보여줄지에 대한 도트 이미지를 순서대로 나열할 수 있으며

각 애니메이션의 프레임을 조절할 수 있다.

또한 각 프레임에 대해서 이벤트를 추가할 수 있었다.

애니메이터는 조금 더 중요하다.
변수를 저장하고 그걸 반응시킬 수 있다는 점이 그렇다.

![image](https://github.com/user-attachments/assets/e18610a4-cf51-4322-8a44-db98e0371b7b)

![image](https://github.com/user-attachments/assets/52651bd4-b969-46f9-be1a-0fbd43b74009)

움직임을 예시로 보면

playerIdle -> playerMove 의 상태를 보면 유저가 이동 키를 눌렀으므로 Conditions 에서 isMoving 이 true 로 설정되어있다.

```c#
  protected Animator animator;

  
  protected virtual void Start()
  {
      animator = GetComponentInChildren<Animator>();
  }

  ...
  bool isMoving = rb.linearVelocity.x != 0;
  animator.SetBool("isMoving", isMoving);
```

특정 스크립트를 실행하여 isMoving 의 state 가 업데이트가 될 경우 isMoving 에 따른 애니메이션이 발생한다.

![image](https://github.com/user-attachments/assets/fe3c28c8-0143-440b-9cdd-3c08a5fd4caf)

다른 마법같은 기능도 존재하였는데.

Blend Tree 라는 기능이 있었다.

![image](https://github.com/user-attachments/assets/b61243cb-5e14-470f-9342-e27745168df7)
![image](https://github.com/user-attachments/assets/81c33e2d-2d06-4a29-a6ea-2020bc1740c0)

각 가중치에 대한 설정을 하고 위와 같이 변수 값이 변경되었을 때 우측에 있는 가중치를 계산해서 각 상황에 맞는 애니메이션이 나온다.

이 그래프는 내가 점프를 했을 때 올라가는 모션과 내려가는 모션을 구분해 둔 것이다.

## 7. 2D Collision Detection

충돌 모션이나 지금 내가 바닥에 있는지 어떻게 계산하는걸까? 할 때 나온 내용이다.

![image](https://github.com/user-attachments/assets/30f610a3-435e-4908-89fb-a2033594bb93)

인스펙터 우측상단에 레이어에 관해서 설정이 가능하다.

예를들어 바닥이라는 오브젝트를 인식하기 위해서 바닥이라는 레이어를 추가하였고.

내 몸에서 특정 거리에 바닥이라는 오브젝트가 있다면 나는 지금 바닥에 있다. 라는 상태를 조절하게 할 수 있다.

![image](https://github.com/user-attachments/assets/4a1f6b11-b5db-4243-865f-11e11508e959)

나는 내 몸에서 어느정도의 길이정도 Gizmos 라인을 그려 이에 도달하였을 경우 바닥이라는 상태값이 변하도록 하였다.

```c#
    protected virtual void CollistionCheck()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround);
    }
```

2D Raycast는 2D 콜라이더와의 충돌을 감지하는 코드이다.

## 8. C# inheritance

코드하면 예쁜 코드 아니겠는가.

역시 나왔다 클린코드 작법

객체지향적 언어인 만큼 익숙한 내용이 나오는데.

c# 은 처음인지라 조금 주의깊게 들었다.

대충 요약해보면 상속에 대한 내용이다. 클래스간 계층 구조를 정의할 수 있도록 한다.

```c#
public class Parent
{
    public void Greet() => Console.WriteLine("Hello from Parent");
}

public class Child : Parent
{
}
```

그리고 override 를 구현하기 위해서는 virtual 로 선언하여 override 로 재정의 할 수 있다.

```c#
public class Parent
{
    protected virtual void Greet() => Console.WriteLine("Hello from Parent");
}

public class Child : Parent
{
    protected override void Greet() => Console.WriteLine("Hello from Child");
}
```
