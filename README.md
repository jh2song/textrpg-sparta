# Text RPG
## 소개
스파르타 던전을 떠나기 전에 마을에서 장비를 정비하는 Text RPG 게임입니다.
## 코드 설명
### `Item` 클래스
각종 아이템에 대한 정보를 저장합니다. ToString을 구현하여 편리하게 아이템에 대한 정보를 출력하도록 했습니다.
### `Character` 클래스
캐릭터에 대한 정보를 저장합니다.
### `Program` 클래스 (메인 클래스)
플레이어와 인벤토리를 멤버변수로 구성하였습니다.
```csharp
internal class Program
{
    private static Character player;
    private static List<Item> inven;
...
```
- `Main` 메소드
    - 게임 데이터 세팅을 하고(`GameDataSetting`) 게임을 시작합니다.(`DisplayGameIntro`)
- `GameDataSetting` 메소드
    - `player`와 `inven`을 초기화합니다.
- `DisplayGameIntro` 메소드
    - 게임을 시작하고 `상태보기`, `인벤토리` 두 가지 행동을 할 수 있습니다.
- `DisplayMyInfo` 메소드
    - `상태보기`를 구현했고 0번을 누르면 `DisplayGameIntro`로 돌아갑니다.
- `DisplayInventory` 메소드
    - `인벤토리`를 구현했고 0번을 누르면 `DisplayGameIntro`로 돌아갑니다. 1번을 누르면 장착을 관리하는 `EquipmentManagerDisplay`로 들어갑니다.
- `EquipmentManagerDisplay` 메소드
    - 정착관리를 할 수 있습니다. 장착 여부에 따라 캐릭터 스탯이 달라집니다. 0번을 누르면 뒤로 돌아갑니다.
- `CheckValidInput` 메소드
    - 입력의 예외를 규정합니다.
## 기술 스택
- C#
- Visual Studio 2022
