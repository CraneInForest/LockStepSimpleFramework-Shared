//
// @brief: 战斗主逻辑
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class BattleLogic
{
    //是否暂停(进入结算界面时, 战斗逻辑就会暂停)
    public bool m_bIsBattlePause = true;

    //帧同步核心逻辑对象
    LockStepLogic m_lockStepLogic = null;

	//战斗日志
	string battleRecord = "";

	//游戏逻辑帧
	static public int s_uGameLogicFrame = 0;

    //是否已开战
    private bool m_bFireWar = false;

    //- 主循环
    // Some description, can be over several lines.
    // @return value description.
    // @author
    public void updateLogic() {
        //如果战斗逻辑暂停则不再运行
        if (m_bIsBattlePause) {
            return;
        }

        //调用帧同步逻辑
        m_lockStepLogic.updateLogic();
    }

    //- 战斗逻辑
    // 
    // @return none
    public void frameLockLogic()
    {
        //如果是回放模式
        if (GameData.g_bRplayMode)
        {
            //检测回放事件
            checkPlayBackEvent(GameData.g_uGameLogicFrame);
        }

        recordLastPos();

        //动作管理器update
        GameData.g_actionMainManager.updateLogic();

        //塔
        for (int i = 0; i < GameData.g_listTower.Count; i++)
        {
            GameData.g_listTower[i].updateLogic();
        }

        //子弹
        for (int i = 0; i < GameData.g_listBullet.Count; i++)
        {
            GameData.g_listBullet[i].updateLogic();
        }

        //士兵
        for (int i = 0; i < GameData.g_listSoldier.Count; i++)
        {
            GameData.g_listSoldier[i].updateLogic();
        }

        if (m_bFireWar && GameData.g_listSoldier.Count == 0)
        {
            stopBattle();
        }
    }

    //- 记录最后的位置
    // 
    // @return none.
    void recordLastPos()
    {
        //子弹
        for (int i = 0; i < GameData.g_listBullet.Count; i++) {
            GameData.g_listBullet[i].recordLastPos();
        }

        //士兵
        for (int i = 0; i < GameData.g_listSoldier.Count; i++)
        {
            GameData.g_listSoldier[i].recordLastPos();
        }
    }

    //- 更新各种对象绘制的位置
    // 包括怪,子弹等等,因为塔的位置是固定的,所以不需要实时刷新塔的位置,提升效率
    // @return none
    public void updateRenderPosition(float interpolation)
    {
        //子弹
        for (int i = 0; i<GameData.g_listBullet.Count; i++) {
            GameData.g_listBullet[i].updateRenderPosition(interpolation);
        }

        //士兵
        for (int i = 0; i<GameData.g_listSoldier.Count; i++)
        {
            GameData.g_listSoldier[i].updateRenderPosition(interpolation);
        }
    }

    //- 初始化
    // 
    // @param mt 上下文句柄
    // @return 元表
    public void init()
    {
		UnityTools.Log ("BattleLogic init!");
		//初始化帧同步逻辑对象
        m_lockStepLogic = new LockStepLogic();
        m_lockStepLogic.setCallUnit(this);

        //游戏运行速度
		UnityTools.setTimeScale(1);

        //战斗不暂停
        m_bIsBattlePause = true;
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    /// <returns></returns>
    public void startBattle()
    {
        GameData.g_srand = new SRandom(1000);
        m_bIsBattlePause = false;
        m_lockStepLogic.init();

        //读取玩家操作数据,为回放做准备
        if (GameData.g_bRplayMode)
        {
            loadUserCtrlInfo();
        }

        GameData.g_uGameLogicFrame = 0;
        TowerStandState.s_fixTestCount = (Fix64)0;
        TowerStandState.s_scTestContent = "";

        //创建塔
        createTowers();
    }

    /// <summary>
    /// 停止战斗
    /// </summary>
    /// <returns></returns>
    public void stopBattle()
    {
        UnityTools.Log("end logic frame: " + GameData.g_uGameLogicFrame);
        UnityTools.Log("s_fixTestCount: " + TowerStandState.s_fixTestCount);
        UnityTools.Log("s_scTestContent: " + TowerStandState.s_scTestContent);
        
        m_bFireWar = false;
        s_uGameLogicFrame = GameData.g_uGameLogicFrame;

        //记录关键事件
        if (!GameData.g_bRplayMode)
        {
            GameData.battleInfo info = new GameData.battleInfo();
            info.uGameFrame = GameData.g_uGameLogicFrame;
            info.sckeyEvent = "stopBattle";
            GameData.g_listUserControlEvent.Add(info);
        }

        gameEnd();
    }

    /// <summary>
    /// 回放战斗录像
    /// </summary>
    /// <returns></returns>
    public void replayVideo()
    {
        GameData.g_bRplayMode = true;
        GameData.g_uGameLogicFrame = 0;
        startBattle();
    }

    /// <summary>
    /// 创建塔
    /// </summary>
    /// <returns></returns>
    private void createTowers()
    {
        for (int i = 0; i < 5; i++)
        {
            var tower = GameData.g_towerFactory.createTower();
            tower.m_fixv3LogicPosition = new FixVector3((Fix64)5, (Fix64)1.3f, (Fix64)(-3.0f) + (Fix64)2.5f * i);
            tower.updateRenderPosition(0);
        }
    }

    /// <summary>
    /// 创建士兵
    /// </summary>
    /// <returns></returns>
    public void createSoldier()
    {
        m_bFireWar = true;

        var soldier = GameData.g_soldierFactory.createSoldier();
        soldier.m_fixv3LogicPosition = new FixVector3((Fix64)0, (Fix64)1, (Fix64)(-4.0f));
        soldier.updateRenderPosition(0);

        float moveSpeed = 3 + GameData.g_srand.Range(0, 3);
        soldier.moveTo(soldier.m_fixv3LogicPosition, new FixVector3(soldier.m_fixv3LogicPosition.x, soldier.m_fixv3LogicPosition.y, (Fix64)8), (Fix64)moveSpeed);

        //记录关键事件
        if (!GameData.g_bRplayMode)
        {
            GameData.battleInfo info = new GameData.battleInfo();
            info.uGameFrame = GameData.g_uGameLogicFrame;
            info.sckeyEvent = "createSoldier";
            GameData.g_listUserControlEvent.Add(info);
        } 
    }

    //- 读取玩家的操作信息
    // 
    // @return none
    void loadUserCtrlInfo()
    {
        GameData.g_listPlaybackEvent.Clear();

		string content = battleRecord;

        string[] contents = content.Split('$');

        for (int i = 0; i < contents.Length - 1; i++)
        {
            string[] battleInfo = contents[i].Split(',');

            GameData.battleInfo info = new GameData.battleInfo();

            info.uGameFrame = int.Parse(battleInfo[0]);
            info.sckeyEvent = battleInfo[1];

            GameData.g_listPlaybackEvent.Add(info);
        }
    }

    //- 检测回放事件
    // 如果有回放事件则进行回放
    // @param gameFrame 当前的游戏帧
    // @return none
    void checkPlayBackEvent(int gameFrame)
    {
        if (GameData.g_listPlaybackEvent.Count > 0) {
            for (int i = 0; i < GameData.g_listPlaybackEvent.Count; i++)
            {
                GameData.battleInfo v = GameData.g_listPlaybackEvent[i];

                if (gameFrame == v.uGameFrame) {
                    if (v.sckeyEvent == "createSoldier") {
                        createSoldier();
                    }
                }
            }
        }
    }

    //- 暂停战斗逻辑
    // 
    // @return none.
    void pauseBattleLogic()
    {
        m_bIsBattlePause = true;
    }

    public void gameEnd()
    {
        if (!m_bIsBattlePause)
        {
            //UnityTools.setTimeScale(1);

            //销毁战场上的所有对象
            //塔
            for (int i = GameData.g_listTower.Count - 1; i >= 0; i--)
            {
                GameData.g_listTower[i].killSelf();
            }

            //子弹
            for (int i = GameData.g_listBullet.Count - 1; i >= 0; i--)
            {
                GameData.g_listBullet[i].killSelf();
            }

            //士兵
            for (int i = GameData.g_listSoldier.Count - 1; i >= 0; i--)
            {
                GameData.g_listSoldier[i].killSelf();
            }

            if (!GameData.g_bRplayMode) {
                recordBattleInfo();
#if _CLIENTLOGIC_
                SimpleSocket socket = new SimpleSocket();
                socket.Init();
                socket.sendBattleRecordToServer(UnityTools.playerPrefsGetString("battleRecord"));
#endif
            }

            pauseBattleLogic();

            GameData.g_bRplayMode = false;

            GameData.release();
        }
    }

    //- 记录战斗信息(回放时使用)
    // 
    // @return none
    void recordBattleInfo() {
        if (false == GameData.g_bRplayMode) {

            //记录战斗数据
            string content = "";
            for (int i = 0; i < GameData.g_listUserControlEvent.Count; i++)
            {
                GameData.battleInfo v = GameData.g_listUserControlEvent[i];
                //出兵
                if (v.sckeyEvent == "createSoldier") {
                    content += v.uGameFrame + "," + v.sckeyEvent + "$";
                }
            }

            UnityTools.playerPrefsSetString("battleRecord", content);
            GameData.g_listUserControlEvent.Clear();
        }
    }

	public void setBattleRecord(string record)
	{
		battleRecord = record;
	}

    //- 释放资源
    // 
    // @return none
    void release()
    {

    }
}
