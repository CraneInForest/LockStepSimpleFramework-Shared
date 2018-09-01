//
// @brief: 状态机类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class StateMachine {
    BaseState m_currentState = null;
    string m_scCurrentStateName = "";
    LiveObject m_unit = null;

    // - 每帧循环
    // 由MainLogic的Update来进行调用, 自己不会调用
    // @return value description.
    public void updateLogic()
    {
        if (null != m_currentState) {
            m_currentState.updateLogic();
        }
    }

    //- 更改状态
    // 
    // @param state 要更改到的状态
    // @return none
    public void changeState(string state, Fix64 args) {
        //检查是否存在之前的状态,有则先退出之前的状态,做好善后工作
        exitOldState();

        m_currentState = null;

        //根据不同的状态参数创建对应的状态
        if (state == "towerattack") {
            m_currentState = new TowerAttackState();
        }
        else if (state == "towerstand")
        {
            m_currentState = new TowerStandState();
        }
        else if (state == "normal")
        {
            m_currentState = new NormalState();
        }
        else if (state == "cooling")
        {
            m_currentState = new CoolingState();
        }

        //为新创建的状态做好准备工作
        m_currentState.onInit(m_unit);

        //设置之前的状态名
        m_currentState.setPrevStateName(m_scCurrentStateName);

        //记录当前的状态名
        m_scCurrentStateName = state;

        //直接进入该状态
        m_currentState.onEnter(args);
    }

    //- 设置之前的状态的名字
    // 记录之前的状态,某些状态需要在执行后恢复到之前的状态,所以需要记录
    // @param stateName 要记录的状态名
    // @return none
    public void setPrevStateName(string stateName){
        m_currentState.setPrevStateName(stateName);
    }

    //- 获取之前的状态的名字
    // @return 之前的状态的名字
    public string getPrevStateName() {
        return m_currentState.getPrevStateName();
    }

    //- 获取当前状态
    // 
    // @return 当前状态
    public string getState(){
        return m_scCurrentStateName;
    }

    //- 退出之前的状态
    // 
    // @return none
    public void exitOldState(){
        if (null != m_currentState) {
            m_currentState.onExit();
        }
    }

    //- 设置起作用的单元主体
    // 
    // @param unit 作用于的单元主体
    // @return none
    public void setUnit(LiveObject value){
        m_unit = value;
    }
}
