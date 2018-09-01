//
// @brief: 豺狼人
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//

using System.Collections;

public class Grizzly : BaseSoldier
{

    public Grizzly()
    {
        loadProperties();

        base.createFromPrefab("Prefabs/Soldier", this);

        //设置类型
        m_scName = "grizzly";
    }

    //- 每帧循环
    // 
    // @return none
    public override void updateLogic()
    {
        //调用父类Update
        base.updateLogic();
    }

    //- 加载属性
    // 
    // @return none
    public override void loadProperties()
    {
        setHp((Fix64)200);
    }
}
