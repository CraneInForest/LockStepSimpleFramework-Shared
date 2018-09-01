//
// @brief: 移动到指定位置的动作事件
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class MoveTo : BaseAction {

    FixVector3 m_fixv3MoveDistance = new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);

    Fix64 m_fixMoveTime = Fix64.Zero;

    Fix64 m_fixMoveElpaseTime = Fix64.Zero;

    FixVector3 m_fixMoveStartPosition = new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);

    FixVector3 m_fixEndPosition = new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);

    public override void updateLogic()
    {
        bool actionOver = false;

        m_fixMoveElpaseTime += GameData.g_fixFrameLen;

        Fix64 timeScale = m_fixMoveElpaseTime / m_fixMoveTime;

        if (timeScale >= (Fix64)1) {
            timeScale = (Fix64)1;
            actionOver = true;
        }

        FixVector3 elpaseDistance = new FixVector3(m_fixv3MoveDistance.x * timeScale, m_fixv3MoveDistance.y * timeScale, m_fixv3MoveDistance.z * timeScale);
        FixVector3 newPosition = new FixVector3(m_fixMoveStartPosition.x + elpaseDistance.x, m_fixMoveStartPosition.y + elpaseDistance.y, m_fixMoveStartPosition.z + elpaseDistance.z);
        unit.m_fixv3LogicPosition = newPosition;

        if (actionOver) {
            removeSelfFromManager();

            if (null != actionCallBackFunction){
                actionCallBackFunction();
            }
        }
    }

    public void init(BaseObject unitbody, FixVector3 startPos, FixVector3 endPos, Fix64 time, ActionCallback cb) {
		name = "moveto";

        unit = unitbody;
        unit.m_fixv3LogicPosition = startPos;
        m_fixMoveStartPosition = startPos;
        m_fixEndPosition = endPos;
        m_fixMoveTime = time;
        if (m_fixMoveTime == Fix64.Zero) {
            m_fixMoveTime = (Fix64)0.1f;
        }

        actionCallBackFunction = cb;
        m_fixv3MoveDistance = new FixVector3(m_fixEndPosition.x - startPos.x, m_fixEndPosition.y - startPos.y, m_fixEndPosition.z - startPos.z);
    }
}
