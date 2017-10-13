﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MasterData")]
public class MasterData : ScriptableObject
{
    public string masterName = "Master";
    public TankAssembleManager selectedTank;
    public GameObject standardPerfab;
    public int level = 1;
    public int money = 100;
    public float weightLimit = 50f;
}
