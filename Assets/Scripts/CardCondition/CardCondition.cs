using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoneCardCondition : ICardCondition
{
    public bool isUsable()
    {
        return true;
    }
}
public class UsedCardThreeOver : ICardCondition
{
    public bool isUsable()
    {
        //if ī�带 3�� �̻� ����� �Ͽ� ��밡��
        return false;
    }
}
public class HaveCardOneOver : ICardCondition { 
    public bool isUsable()
    {
        //ī�带 ���� �̻� �տ� ��� ���� ��� ��밡��
        return false;
    }
}
public class IsSpawnInfiSword : ICardCondition { 
    public bool isUsable() { 
        //������ ���� ��ȯ �Ǿ� ���� ��� ��밡��
        return false;
    }
}
public class HaveHpThirtyOver : ICardCondition {
    public bool isUsable() {
        //ü���� 30 �̻��̸� ��밡��
        return false;
    }
}

