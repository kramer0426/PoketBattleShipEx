using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
// LocalizeTextEntity
//
[System.Serializable]
public class LocalizeTextEntity
{
    public int Id;
    public string krString;
    public string enString;
    public string jpString;
    public string cnString;
    public string twString;
    public string spanishString;
    public string portugueseString;
    public string indonesianString;
    public string thaiString;
    public string vietnameseString;
}


//
// ShipDataEntity
//
[System.Serializable]
public class ShipDataEntity
{
    public int Id;
    public string Name;
    public string Class;
    public int Hp;
    public int Ap;
    public int StartAim;
    public double StartFireCool;
    public float MaxFireCool;
    public int MaxDefense;
    public int Fuel;
    public int ShellSize;
    public int ShellCnt;
    public float SightRange;
    public float FireRange;
    public float MoveSpeed;
    public int AttackType;
    public int AttackCnt;
    public string ResourceName;

}

//
// EnemyShipEntity
//
[System.Serializable]
public class EnemyShipEntity
{
    public int Id;
    public string Name;
    public string Class;
    public int Hp;
    public int Ap;
    public int Aim;
    public float FireCool;
    public int SideDp;
    public int TopDp;
    public int TorepedoDp;
    public int Fuel;
    public int ShellSize;
    public int ShellCnt;
    public float SightRange;
    public float FireRange;
    public float MoveSpeed;
    public int AttackType;
    public int AttackCnt;
    public string ResourceName;

}