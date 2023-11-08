using System.Data;

internal class Program
{
    private static Character player;
    private static List<Item> inven;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        inven = new List<Item>();
        inven.Add(new Item(false, "무쇠갑옷", 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
        inven.Add(new Item(false, "낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다."));
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                // 작업해보기
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보르 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");

        int totalAddedAtk = inven.Where(item => item.IsEquipped).Sum(item => item.AddedAtk);
        int totalAddedDef = inven.Where(item => item.IsEquipped).Sum(item => item.AddedDef);
        string totalAddedAtkStr = "";
        string totalAddedDefStr = "";
        if (totalAddedAtk != 0)
            totalAddedAtkStr = $"({totalAddedAtk:+#;-#;0})";
        if (totalAddedDef != 0)
            totalAddedDefStr = $"({totalAddedDef:+#;-#;0})";

        Console.WriteLine($"공격력 : {player.Atk} {totalAddedAtkStr}");
        Console.WriteLine($"방어력 : {player.Def} {totalAddedDefStr}");
        Console.WriteLine($"체력 : {player.Hp}");
        Console.WriteLine($"Gold : {player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 0);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
        }
    }

    static void DisplayInventory()
    {
        Console.Clear();

        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        foreach (Item item in inven)
        {
            Console.WriteLine($"- {item.ToString()}");
        }
        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, 1);
        switch (input)
        {
            case 0:
                DisplayGameIntro();
                break;
            case 1:
                EquipmentManagerDisplay();
                break;
        }
    }

    static void EquipmentManagerDisplay()
    {
        Console.Clear();

        Console.WriteLine("인벤토리 - 장착 관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < inven.Count; i++)
        {
            Console.WriteLine($"- {i + 1} {inven[i].ToString()}");
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");

        int input = CheckValidInput(0, inven.Count);
        switch (input)
        {
            case 0:
                DisplayInventory();
                break;
            default:
                if (inven[input - 1].IsEquipped)
                {
                    // 장착 해제
                    inven[input - 1].IsEquipped = false;
                    player.Atk -= inven[input - 1].AddedAtk;
                    player.Def -= inven[input - 1].AddedDef;
                }
                else
                {
                    // 장착 시도
                    inven[input - 1].IsEquipped = true;
                    player.Atk += inven[input - 1].AddedAtk;
                    player.Def += inven[input - 1].AddedDef;
                }
                EquipmentManagerDisplay();
                break;
        }
    }

    static int CheckValidInput(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

public class Character
{
    public string Name { get; set; }
    public string Job { get; set; }
    public int Level { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public int Gold { get; set; }

    private List<Item> _equippedItem = new List<Item>();

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}

public class Item
{
    public bool IsEquipped { get; set; } = false;
    public string ItemName { get; set; } = "";
    public int AddedAtk { get; set; } = 0;
    public int AddedDef { get; set; } = 0;
    public string Explanation { get; set; } = "";

    public Item(bool isEquipped, string itemName, int addedAtk, int addedDef, string explanation)
    {
        IsEquipped = isEquipped;
        ItemName = itemName;
        AddedAtk = addedAtk;
        AddedDef = addedDef;
        Explanation = explanation;
    }

    public override string ToString()
    {
        string equippedSym = "";
        if (IsEquipped)
            equippedSym = "[E]";

        string atkSym = "";
        if (AddedAtk > 0)
            atkSym = $" 공격력 +{AddedAtk} ";

        string defSym = "";
        if (AddedDef > 0)
            defSym = $" 방어력 +{AddedDef} ";


        return $"{equippedSym}{ItemName}\t|{atkSym}{defSym}\t\t| {Explanation}";
    }
}