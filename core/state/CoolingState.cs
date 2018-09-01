//
// @brief: 冷却状态
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class CoolingState : BaseState
{
    public CoolingState()
    {
        init();
    }

    //- 初始化函数.
    // Some description, can be over several lines.
    // @return value description.
    void init() {
        m_scName = "cooling";
    }

    //- 创建时进入的初始化函数
    // 
    // @param args 附加的创建信息
    // @return none
    public override void onInit(LiveObject args) {
        m_unit = args;
    }

    //- 进入该状态时调用的函数
    // 
    // @param args 附加的调用信息
    // @return none
    public override void onEnter(Fix64 args) {
        m_unit.isCooling = true;

        //冷却时间
        Fix64 cdtime = args;

        m_unit.delayDo(cdtime, delegate () {
            if (null != m_scPrevStateName)
            {
                m_unit.checkStatue();
                m_unit.changeState(m_scPrevStateName);
            }
        }, "changePrevState");
    }


    //- 退出该状态时调用的函数
    // 
    // @return none
    public override void onExit()
    {
        m_unit.isCooling = false;
        m_unit.stopAction("changePrevState");
    }

    //- 处于该状态时每帧调用的函数
    // 
    // @return none
    public override void updateLogic()
    {
        
    }
}
