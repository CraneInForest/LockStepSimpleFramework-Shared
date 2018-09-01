//
// @brief: 直射子弹
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class DirectionShootBullet : BaseBullet
{
    Fix64 m_fixMoveTime = Fix64.Zero;
    Fix64 m_fixSpeed = Fix64.Zero;

    //- 每帧循环
    // 
    // @return none
    public override void updateLogic()
    {
        //调用父类Update
        base.updateLogic();
    }

    //- 初始化数据
    // 
    // @param src 发射源
    // @param dest 射击目标
    // @param poOri 发射的起始位置
    // @param poDst 发射的目标位置
    // @return none.
    public override void initData(LiveObject src, LiveObject dest, FixVector3 poOri, FixVector3 poDst)
    {
        base.initData(src, dest, poOri, poDst);

        Fix64 distance = FixVector3.Distance(poOri, poDst);

        m_fixMoveTime = distance / m_fixSpeed;
    }

    //- 射击
    // 
    // @return none.
    public override void shoot()
    {
        m_fixv3LogicPosition = m_fixv3SrcPosition;

        moveTo(m_fixv3SrcPosition, m_fixv3DestPosition, m_fixMoveTime, delegate ()
        {
            doShootDest();
        });
    }

    //- 根据名字加载预制体
    // 
    // @param name 子弹的名字
    // @return none
    public override void createBody(string nameValue)
    {
        //加载子弹主体
        createFromPrefab("Prefabs/Bullet", this);

        //名字
        m_scName = nameValue;
    }

    //- 加载属性
    // 
    // @return none
    public override void loadProperties()
    {
        m_fixSpeed = (Fix64)10;
    }
}