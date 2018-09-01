//
// @brief: 存在的对象基类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;
using System.Collections.Generic;

public class LiveObject : BaseObject
{
    //血量
    private Fix64 m_fixHp = Fix64.Zero;
    public Fix64 hp { get { return m_fixHp; } set { m_fixHp = value; } }

    //初始血量
    private Fix64 m_fixOrignalHp = Fix64.Zero;
    public Fix64 orignalHp { get { return m_fixOrignalHp; } set { m_fixOrignalHp = value; } }

    //普通伤害
    private Fix64 m_fixDamage = Fix64.Zero;
    public Fix64 damage { get { return m_fixDamage; } set { m_fixDamage = value; } }

    //攻击我的列表
    public List<LiveObject> m_listAttackMe = new List<LiveObject>();

    //攻击我的子弹列表(死亡后通知其失效)
    public List<BaseObject> m_listAttackMeBullet = new List<BaseObject>();

    //我正在攻击的列表
    public List<LiveObject> m_listAttackingList = new List<LiveObject>();

    //侦测范围
    private Fix64 m_fixAttackRange = Fix64.Zero;
    public Fix64 attackRange { get { return m_fixAttackRange; } set { m_fixAttackRange = value; } }

    //攻击速度
    private Fix64 m_fixAttackSpeed = Fix64.Zero;
    public Fix64 attackSpeed { get { return m_fixAttackSpeed; } set { m_fixAttackSpeed = value; } }

    //锁定的攻击对象
    private LiveObject m_lockedAttackUnit = null;
    public LiveObject lockedAttackUnit { get { return m_lockedAttackUnit; } set { m_lockedAttackUnit = value; } }

    //是否处于冷却状态
    private bool m_bIsCooling = false;
    public bool isCooling { get { return m_bIsCooling; } set { m_bIsCooling = value; } }

    //- 设置血量
    // 
    // @param value 要设置的血量值
    // @return none
    public void setHp(Fix64 value) {
        m_fixHp = value;
        m_fixOrignalHp = value;
    }

    //- 获取血量
    // @return none
    // @author
    public Fix64 getHp()
    {
        return m_fixHp;
    }

    //- 添加我正在攻击的对象
    // 用于在死亡时通知对应对象
    // @param obj 要攻击的对象
    // @return none
    public void addAttackingObj(LiveObject obj)
    {
        //判断是否已经添加过
        if (m_listAttackingList.Contains(obj))
        {
            return;
        }

        //插入队列
        m_listAttackingList.Add(obj);
    }

    //- 移除我正在攻击的对象
    // 对方死亡时,我要从自己正在攻击的队列中把对方移除掉
    // @param obj 要移除的对象
    // @return none
    public void removeAttackingObj(LiveObject obj)
    {
        m_listAttackingList.Remove(obj);
    }

    //- 添加正在攻击我的对象
    // 用于在死亡时通知对应对象
    // @param obj 正在攻击我的对象
    // @return none
    public void addAttackMeObj(LiveObject obj)
    {
        //判断是否已经添加过
        if (m_listAttackMe.Contains(obj))
        {
            return;
        }

        //插入队列
        m_listAttackMe.Add(obj);
    }

    //- 移除正在攻击我的对象
    // 对方死亡时,要从对方正在攻击的队列中把我移除掉
    // @param obj 要移除的对象
    // @return none
    public void removeAttackMeObj(LiveObject obj)
    {
        m_listAttackMe.Remove(obj);
    }

    //- 添加正在攻击我的子弹对象
    // 用于在死亡时通知对应子弹对象
    // @param obj 正在攻击我的子弹对象
    // @return none
    public void addAttackMeBulletObj(BaseObject obj)
    {
        //判断是否已经添加过
        if (m_listAttackMeBullet.Contains(obj))
        {
            return;
        }

        //插入队列
        m_listAttackMeBullet.Add(obj);
    }

    //- 移除正在攻击我的子弹对象
    // 对方死亡时, 要从正在攻击我的子弹队列中移除掉该子弹
    // @param obj 要移除的对象
    // @return none
    public void removeAttackMeBulletObj(BaseObject obj)
    {
        m_listAttackMeBullet.Remove(obj);
    }


    //- 发送死亡信息给相关对象
    // 
    // @return none
    public void sendDeadInfoToRelativeObj()
    {
    //print("name . ", name)
    //print("#attackMeList . ", #attackMeList)
        //让所有攻击我的子弹失效
        for (int i = m_listAttackMeBullet.Count - 1; i >= 0; i--) {
            m_listAttackMeBullet[i].uneffect = true;
            removeAttackMeBulletObj(m_listAttackMeBullet[i]);
        }

        //通知我正在攻击的对象,我已经死了,从我正在攻击的对象身上把自身移除
        for (int i = m_listAttackingList.Count - 1; i >= 0; i--) {
            LiveObject obj = m_listAttackingList[i];
            obj.removeAttackMeObj(this);
            removeAttackingObj(obj);
        }

        //通知正在攻击我的对象,我已经死了,别打了
        for (int i = m_listAttackMe.Count - 1; i >= 0; i--) {
            LiveObject obj = m_listAttackMe[i];
            obj.removeAttackingObj(this);
            removeAttackMeObj(obj);

            if (obj.m_scType == "tower")
            {
                //print("current state . ", obj.getState())
                if (obj.getState() != "cooling")
                {
                    obj.changeState("towerstand");
                }
                else
                {
                    obj.setPrevStateName("towerstand");
                }
            }
        }
    }

    //- 设置攻击力
    // 
    // @param value 攻击力
    // @return none
    public void setDamageValue(Fix64 value){
        m_fixDamage = value;
    }

    //- 获取攻击力
    // 
    // @return none
    public Fix64 getDamageValue()
    {
        return m_fixDamage;
    }

    //- 受到伤害
    // 
    // @param damage 被伤害的值
    // @return none
    public void beDamage(Fix64 damage, bool isSrcCrit = false)
    {
        if (false == m_bKilled) {
            //播放被攻击的动画
            if (m_scType == "tower") {
                playAnimation("Hurt");

                delayDo((Fix64)0.5, delegate () { playAnimation("Stand"); }, "delaytostand");
            }

            //扣血,如果扣到小于等于0则死亡
            m_fixHp = m_fixHp - damage;

            if (m_fixHp <= Fix64.Zero) {
                m_bKilled = true;
            }
        }
    }


    //- 自杀
    // 
    // @return none
    public override void killSelf()
    {
        //告知所有攻击我的对象,别打了,恢复正常吧
        sendDeadInfoToRelativeObj();

        base.killSelf();
    }

    //- 加载属性
    // 
    // @return none
    public virtual void loadProperties()
    {
        
    }

    //- 获取攻击范围
    // 
    // @return none
    public Fix64 getAttackRange()
    {
        return m_fixAttackRange;
    }

    //- 跳转到对应的状态
    // 
    // @param state 要跳转到的状态 
    // @return none
    public void changeState(string state)
    {
        m_statemachine.changeState(state, (Fix64)0);
    }

    //- 跳转到对应的状态
    // 
    // @param state 要跳转到的状态 
    // @return none
    public void changeState(string state, Fix64 args)
    {
        m_statemachine.changeState(state, args);
    }

    //- 设置之前的状态的名字
    // 记录之前的状态,某些状态需要在执行后恢复到之前的状态,所以需要记录
    // @param stateName 要记录的状态名
    // @return none
    public void setPrevStateName(string stateName)
    {
        m_statemachine.setPrevStateName(stateName);
    }

    //- 获取之前的状态的名字
    // @return 之前的状态的名字
    public string getPrevStateName()
    {
        return m_statemachine.getPrevStateName();
    }

    //- 获取当前状态
    // 
    // @return 当前状态
    public string getState()
    {
        return m_statemachine.getState();
    }
}
