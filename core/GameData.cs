//
// @brief: 公用数据类
// @version: 1.0.0
// @author helin
// @date: 03/7/2018
// 
// 
//


using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameData {
    //所有士兵的队列
    public static List<BaseSoldier> g_listSoldier = new List<BaseSoldier>();

    //所有塔的队列
    public static List<BaseTower> g_listTower = new List<BaseTower>();

    //所有子弹的队列
    public static List<BaseBullet> g_listBullet = new List<BaseBullet>();

    //所有操作事件的队列
    public static List<battleInfo> g_listUserControlEvent = new List<battleInfo>();

    //所有回放事件的队列
    public static List<battleInfo> g_listPlaybackEvent = new List<battleInfo>();

    //预定的每帧的时间长度
    public static Fix64 g_fixFrameLen = Fix64.FromRaw(273);

    //游戏的逻辑帧
    public static int g_uGameLogicFrame = 0;

    //是否为回放模式
    public static bool g_bRplayMode = false;

    //士兵工厂
    public static SoldierFactory g_soldierFactory = new SoldierFactory();

    //塔工厂
    public static TowerFactory g_towerFactory = new TowerFactory();

    //action主管理器(用于管理各liveobject内部的独立actionManager)
    public static ActionMainManager g_actionMainManager = new ActionMainManager();

    //子弹管理器
    public static BulletFactory g_bulletManager = new BulletFactory();

    //战斗是否结束
    public static bool g_bBattleEnd = false;

    //随机数对象
    public static SRandom g_srand = new SRandom(1000);

    public struct battleInfo
    {
        public int uGameFrame;
        public string sckeyEvent;
    }

    //- 释放资源
    // 
    // @return none
    public static void release() {
        g_listPlaybackEvent.Clear();

        g_listUserControlEvent.Clear();

        GameData.g_actionMainManager.release();
    }
}
