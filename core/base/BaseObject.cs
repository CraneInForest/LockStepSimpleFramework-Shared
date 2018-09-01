//
// @brief: 物体对象基类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class BaseObject : UnityObject
{
    //移动的action
    protected MoveTo m_movetoAction = null;
    public MoveTo movetoAction { get { return m_movetoAction; } set { m_movetoAction = value; } }

    //名字
    protected string m_scName = "";
    public string name { get { return m_scName; } set { m_scName = value; } }

    //action管理器
    protected ActionManager m_actionManager = null;
    public ActionManager actionManager { get { return m_actionManager; } set { m_actionManager = value; } }

    protected StateMachine m_statemachine = null;
    public StateMachine priorAttackTarget { get { return m_statemachine; } set { m_statemachine = value; } }

    protected bool m_bUneffect = false;
    public bool uneffect { get { return m_bUneffect; } set { m_bUneffect = value; } }

    public BaseObject()
    {
        init();
    }

    // Use this for initialization
    void init () {
        m_actionManager = new ActionManager();
        GameData.g_actionMainManager.addActionManager(m_actionManager);
    }

    //- 移动到制定位置
    // 
    // @param obj 要移动的对象
    // @param startPos 移动开始的位置
    // @param endPos 移动结束的位置
    // @param time 移动计划经历的时间
    // @param cb 移动完毕后的回调函数
    // @return none
    public void moveTo(FixVector3 startPos, FixVector3 endPos, Fix64 time, ActionCallback cb = null)
    {
        if (null == m_movetoAction) {
            m_movetoAction = new MoveTo();
            m_movetoAction.init(this, startPos, endPos, time, cb);
            m_actionManager.addAction(m_movetoAction);
        }
    }

    //- 延迟执行方法
    // 
    // @param time 要延迟的时间
    // @param cb 时间到了以后的回调函数
    // @param label 延迟动作对象的标签(用于停止延迟动作)
    // @return none
    public void delayDo(Fix64 time, ActionCallback cb, string label = null){
        DelayDo delaydoAction = new DelayDo();
        delaydoAction.init(time, cb);

        if (null != label) {
            delaydoAction.setLabel(label);
        }

        m_actionManager.addAction(delaydoAction);
    }

    //- 停止移动
    // 
    // @return none
    public void stopMove(){
        if (null != m_movetoAction) {
            m_actionManager.removeAction(m_movetoAction);

            m_movetoAction = null;
        }
    }

    //- 停止指定的action
    // 
    // @param label 要停止的action的label
    // @return none
    public void stopAction(string label){
        m_actionManager.stopAction(label);
    }

    //- 根据action类型停止指定的action
    // 
    // @param label 要停止的action的类型
    // @return none
    public void stopActionByName(string type){
        m_actionManager.stopActionByName(type);
    }

    //- 停止所有的action
    // 
    // @return none
    public void stopAllAction(){
        m_actionManager.stopAllAction();
    }

    //- 清除action管理器
    // 
    // @return none
    public void killActionManager(){
        m_actionManager.enable = false;
    }


    //- 检测事件并执行
    // 由于事件都是一次性的,所以执行完毕后立即清空
    // @return none
    public void checkEvent()
    {
        //释放内存
        if (m_bKilled) {
            //停止所有delaydo
            stopActionByName("delaydo");

                //塔
            if (m_scType == "tower") {

                for (int i = GameData.g_listTower.Count - 1; i >= 0; i--)
                {
                    if (this == GameData.g_listTower[i])
                    {
                        GameData.g_listTower.Remove(GameData.g_listTower[i]);
                        break;
                    }
                }
                
            }
            //士兵
                else if (m_scType == "soldier") {
                    for (int i = GameData.g_listSoldier.Count - 1; i >= 0; i--)
                    {
                        if (this == GameData.g_listSoldier[i])
                        {
                            GameData.g_listSoldier.Remove(GameData.g_listSoldier[i]);
                            break;
                        }
                    }
                }
                //子弹
                else if (m_scType == "bullet") {
                    for (int i = GameData.g_listBullet.Count - 1; i >= 0; i--)
                    {
                        if (this == GameData.g_listBullet[i])
                        {
                            GameData.g_listBullet.Remove(GameData.g_listBullet[i]);
                            break;
                        }
                    }
                }
                //其它
                else {
                    UnityTools.LogError("wrong type : " + m_scType);
                }

            destroyGameObject();
        }
     }

    // - 检测逻辑上是否已经死亡
    // 如果死亡则做对应的一系列处理
    // @return value description.
    public void checkIsDead(){
        if (m_bKilled) {
            killSelf();
        }
    }

    //-设置位置
    //
    // @param position 要设置到的位置
    // @return none
    virtual public void setPosition(FixVector3 position){
        m_fixv3LogicPosition = position;
    }

    // - 获取位置
    //
    // @return 当前逻辑位置
    public FixVector3 getPosition(){
        return m_fixv3LogicPosition;
    }

    // - 自杀
    //
    // @return none
    virtual public void killSelf(){
        stopAllAction();
        killActionManager();

        if (null != m_statemachine) {
            m_statemachine.exitOldState();
        }

        m_bKilled = true;

        checkEvent();
    }

    //- 检查状态
    // 在冷却状态结束后检测一下当前状态,以便根据当前状态刷新逻辑
    // @return none
    virtual public void checkStatue()
    {

    }

    //- 记录最后的位置
    // 
    // @return none.
    public void recordLastPos(){
        m_fixv3LastPosition = m_fixv3LogicPosition;
    }
}
