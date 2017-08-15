﻿using UnityEngine;

public abstract class AttackManager : MonoBehaviour 
{
    public string shortcutName = "Fire0";                           // 攻击键名称
    public float coolDownTime = 0.8f;                               // 冷却时间

    public bool IsCoolDown { get { return !CDTimer.IsTimeUp; } }    // 是否正在冷却

    protected CountDownTimer cdTimer;                               // 冷却时间计时器
    protected CountDownTimer CDTimer
    {
        get
        {
            if (cdTimer == null)
                cdTimer = new CountDownTimer(coolDownTime);
            return cdTimer;
        }
    }      

    /// <summary>
    /// 设置快捷键名称
    /// </summary>
    /// <param name="name">名称</param>
    public void SetShortcutName(string name)
    {
        shortcutName = name;
    }

    /// <summary>
    /// 释放攻击，响应OnAttack事件
    /// </summary>
    /// <param name="values">参数列表</param>
    public void Attack(params object[] values)
    {
        if (!CDTimer.IsTimeUp)
            return;
        CDTimer.Reset();
        OnAttack(values);
    }

    /// <summary>
    /// 真正的攻击效果
    /// </summary>
    /// <param name="values">参数列表</param>
    abstract protected void OnAttack(params object[] values);

}
