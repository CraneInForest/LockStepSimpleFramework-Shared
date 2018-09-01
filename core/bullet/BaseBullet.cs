//
// @brief: 子弹基类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;
using System.Collections.Generic;

public class BaseBullet : BaseObject {
    LiveObject m_src = null;
    protected LiveObject m_dest = null;
    protected FixVector3 m_fixv3SrcPosition = new FixVector3();
    protected FixVector3 m_fixv3DestPosition = new FixVector3();
    protected Fix64 m_fixDamage = Fix64.Zero;

    //- 每帧循环
    // 
    // @return none
    virtual public void updateLogic() {
        checkEvent();
    }

    //- 初始化数据
    // 
    // @param src 发射源
    // @param dest 射击目标
    // @param poOri 发射的起始位置
    // @param poDst 发射的目标位置
    // @return none.
    virtual public void initData(LiveObject src1, LiveObject dest1, FixVector3 poOri, FixVector3 poDst) {
        m_scType = "bullet";

        loadProperties();

        m_src = src1;
        m_dest = dest1;
        m_fixv3SrcPosition = poOri;
        m_fixv3DestPosition = poDst;

        m_fixDamage = m_src.getDamageValue();

        m_dest.addAttackMeBulletObj(this);
    }

    //- 射击
    // 
    // @return none.
    virtual public void shoot() {


    }

    //- 攻击目标对象
    // 
    // @return none.
    virtual public void doShootDest() {
        //目标被扣血
        if (false == m_bUneffect) {
            removeFromDestBulletList();

            m_dest.beDamage(m_fixDamage);
        }

        m_bKilled = true;
    }

    //- 从攻击者的子弹列表中移除自身
    // 避免被攻击者已经死了子弹还在攻击的问题
    // @return none
    protected void removeFromDestBulletList() {
        List<BaseObject> list = m_dest.m_listAttackMeBullet;
        list.Remove(this);
    }

    //- 加载属性
    // 
    // @param id 类型id
    // @return none
    virtual public void loadProperties() {

        
    }

    //- 根据名字加载预制体
    // 
    // @param name 子弹的名字
    // @return none
    virtual public void createBody(string name) {

    }
}
