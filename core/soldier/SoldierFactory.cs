//
// @brief: 士兵工厂类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class SoldierFactory
{

	//- 创建士兵
    // 
    // @return 创建出的士兵.
    public BaseSoldier createSoldier() {
        BaseSoldier soldier;

        soldier = new Grizzly();

        GameData.g_listSoldier.Add(soldier);

        //立即记录最后的位置,否则通过vector3.lerp来进行移动动画时会出现画面抖动的bug
        soldier.recordLastPos();

        return soldier;
    }
}
