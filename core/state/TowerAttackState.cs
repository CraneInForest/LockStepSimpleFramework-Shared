//
// @brief: 塔攻击状态
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class TowerAttackState : BaseState
{
    public TowerAttackState()
    {
        init();
    }

    //- 初始化函数.
    // Some description, can be over several lines.
    // @return value description.
    void init()
    {
        m_scName = "towerattack";
    }

    //- 创建时进入的初始化函数
    // 
    // @param args 附加的创建信息
    // @return none
    public override void onInit(LiveObject args)
    {
        m_unit = args;
    }

    //- 进入该状态时调用的函数
    // 
    // @param args 附加的调用信息
    // @return none
    public override void onEnter(Fix64 args)
    {
        BaseSoldier soldier = (BaseSoldier)m_unit.lockedAttackUnit;

        GameData.g_bulletManager.createBullet(m_unit, soldier, m_unit.m_fixv3LogicPosition, soldier.m_fixv3LogicPosition);
        m_unit.changeState("cooling", m_unit.attackSpeed);
    }


    //- 退出该状态时调用的函数tgfdsdfgdfsgfgggggggg
    // 
    // @return none
    public override void onExit()
    {

    }

    //- 处于该状态时每帧调用的函数
    // 
    // @return none
    public override void updateLogic()
    {
        
    }
}
