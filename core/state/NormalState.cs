//
// @brief: 普通状态
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class NormalState : BaseState
{
    public NormalState()
    {
        init();
    }

    //- 初始化函数.
    // Some description, can be over several lines.
    // @return value description.
    void init()
    {
        m_scName = "normal";
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
