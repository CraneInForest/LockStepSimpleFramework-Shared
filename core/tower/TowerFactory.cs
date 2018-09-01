//
// @brief: 塔工厂类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class TowerFactory
{

    //- 创建塔
    // 
    // @param name 名字
    // @param pos 在地图中的位置(注意是地图块的位置, 不是坐标)
    // @return 创建出的士兵.
    public BaseTower createTower() {
        BaseTower tower = new MagicStand();

        tower.changeState("towerstand");

        GameData.g_listTower.Add(tower);

        return tower;
    }
}
