//
// @brief: 状态机对象基类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class BaseState
{
    //所挂载的主体单元
    protected LiveObject m_unit = null;

    //之前的状态名
    protected string m_scPrevStateName = "";

    //当前状态名
    protected string m_scName = "";

    //- 创建时进入的初始化函数
    // 
    // @param args 附加的创建信息
    // @return none
    virtual public void onInit(LiveObject args) {

    }

    //- 进入该状态时调用的函数
    // 
    // @param args 附加的调用信息
    // @return none
    virtual public void onEnter(Fix64 args)
    {

    }

    //- 退出该状态时调用的函数
    // 
    // @return none
    virtual public void onExit()
    {

    }

    //- 处于该状态时每帧调用的函数
    // 
    // @return none
    virtual public void updateLogic()
    {

    }

    //- 设置之前的状态的名字
    // 记录之前的状态,某些状态需要在执行后恢复到之前的状态,所以需要记录
    // @param stateName 要记录的状态名
    // @return none
    public void setPrevStateName(string stateName)
    {
        m_scPrevStateName = stateName;
    }

    //- 获取之前的状态的名字
    // @return 之前的状态的名字
    public string getPrevStateName()
    {
        return m_scPrevStateName;
    }
}
