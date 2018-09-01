//
// @brief: Action基类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
using System.Collections;

public class BaseAction {
    private ActionCallback m_actionCallBackFunction = null;
    public ActionCallback actionCallBackFunction { get { return m_actionCallBackFunction; } set { m_actionCallBackFunction = value; } }

    private bool m_bEnable = true;
    public bool enable { get { return m_bEnable; } set { m_bEnable = value; } }

    private string m_scLabel = "";
    public string label { get { return m_scLabel; } set { m_scLabel = value; } }

    private string m_scName = "";
    public string name { get { return m_scName; } set { m_scName = value; } }

    private BaseObject m_unit = null;
    public BaseObject unit { get { return m_unit; } set { m_unit = value; } }

    ActionManager m_belongToManager = null;

    public void setBelongToManager(ActionManager manager)
    {
        m_belongToManager = manager;
    }

    public ActionManager getBelongToManager() {
        return m_belongToManager;
    }

    public void removeSelfFromManager()
    {
        m_belongToManager.removeAction(this);
    }

    public void setLabel(string value) {
        label = value;
    }

    public virtual void updateLogic()
    {

    }
}
