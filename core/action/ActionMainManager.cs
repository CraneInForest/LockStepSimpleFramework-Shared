//
// @brief: 事件管理主类(统管所有的actionManager)
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//

using System.Collections;
using System.Collections.Generic;

public class ActionMainManager {

    List<ActionManager> m_listActionMain = new List<ActionManager>();

    public void updateLogic()
    {
        for (int i = 0; i < m_listActionMain.Count; i++)
        {
            if (m_listActionMain[i].enable)
            {
                m_listActionMain[i].updateLogic();
            }
        }

        for (int i = m_listActionMain.Count - 1; i >= 0; i--)
        {
            if (!m_listActionMain[i].enable)
            {
                m_listActionMain.Remove(m_listActionMain[i]);
            }
        }
    }

    public void addActionManager(ActionManager actionManager)
    {
        m_listActionMain.Add(actionManager);
    }

    public void removeActionManager(ActionManager actionManager)
    {
        actionManager.enable = false;
    }

    public void release()
    {
        for (int i = m_listActionMain.Count - 1; i >= 0; i--)
        {
            m_listActionMain.Remove(m_listActionMain[i]);
        }
    }
}
