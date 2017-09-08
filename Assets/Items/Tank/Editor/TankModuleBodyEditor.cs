﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TankModuleBody))]
public class TankModuleBodyEditor : TankModuleEditor 
{
    private TankModuleBody body;

    public override void SetDefaultValue()
    {
        base.SetDefaultValue();
        body = target as TankModuleBody;
        body.leftWheelTop = GameMathf.ClampZeroWithRound(moduleBounds.center + new Vector3(-moduleBounds.extents.x, -moduleBounds.extents.y, 0));
        body.rightWheelTop = GameMathf.ClampZeroWithPrecision(moduleBounds.center + new Vector3(moduleBounds.extents.x, -moduleBounds.extents.y, 0));
    }
}
