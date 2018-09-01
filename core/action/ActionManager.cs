//
// @brief: 事件管理类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//

using System.Collections;
using System.Collections.Generic;

public class ActionManager{
    List<BaseAction> m_listAction = new List<BaseAction>();
    public bool m_bEnable = true;
    public bool enable { get { return m_bEnable; } set { m_bEnable = value; } }

    public void updateLogic() {
        for (int i = 0; i < m_listAction.Count; i++)
        {
            if (m_listAction[i].enable) {
                m_listAction[i].updateLogic();
            }
        }

        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            if (!m_listAction[i].enable)
            {
                m_listAction.Remove(m_listAction[i]);
            }
        }
    }

    public void addAction(BaseAction action) {
        m_listAction.Add(action);

        action.setBelongToManager(this);
    }

    public void removeAction(BaseAction action)
    {
        action.enable = false;
    }

    public void stopAllAction() {
        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            m_listAction.Remove(m_listAction[i]);
        }
    }

    public void stopAction(string label) {
        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            if (label == m_listAction[i].label) {
                m_listAction.Remove(m_listAction[i]);
            }
        }
    }

    public void stopActionByName(string name) {
        for (int i = m_listAction.Count - 1; i >= 0; i--)
        {
            if (name == m_listAction[i].name)
            {
                m_listAction.Remove(m_listAction[i]);
            }
        }
    }
}
