//
// @brief: unity对象基类
// @version: 1.0.0
// @author helin
// @date: 8/20/2018
// 
// 
//
#if _CLIENTLOGIC_
using UnityEngine;
#endif

using System.Collections;

public class UnityObject
{
    public string m_scBundle = "";
    public string m_scAsset = "";
    public string m_scType = "";

    //是否被杀掉了
    public bool m_bKilled = false;
#if _CLIENTLOGIC_
    public GameObject m_gameObject;
#endif
    //最后的位置
    public FixVector3 m_fixv3LastPosition = new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);

    //逻辑位置
    public FixVector3 m_fixv3LogicPosition = new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);

    //旋转值
    FixVector3 m_fixv3LogicRotation;

    //缩放值
    FixVector3 m_fixv3LogicScale;

    public void createFromPrefab(string path, UnityObject script) {
#if _CLIENTLOGIC_
        prefab.create(path, script);
#endif
    }


    public void updateRenderPosition(float interpolation) {
#if _CLIENTLOGIC_
        if (m_bKilled)
        {
            return;
        }

        //只有会移动的对象才需要采用插值算法补间动画,不会移动的对象直接设置位置即可
        if ((m_scType == "soldier" || m_scType == "bullet") && interpolation != 0)
        {
            m_gameObject.transform.localPosition = Vector3.Lerp(m_fixv3LastPosition.ToVector3(), m_fixv3LogicPosition.ToVector3(), interpolation);
        }
        else
        {
            m_gameObject.transform.localPosition = m_fixv3LogicPosition.ToVector3();
        }
#endif
    }

    //- 播放动画
    // 
    // @param animationName 动画名
    // @return; none
    public void playAnimation(string animationName) {

    }

    //- 排队播放动画
    // 
    // @param animationName 动画名
    // @return; none
    public void playAnimationQueued(string animationName) {
#if _CLIENTLOGIC_

#endif
    }

    //- 停止动画
    // 
    // @return; none
    public void stopAnimation() {
#if _CLIENTLOGIC_
        Animation animation = m_gameObject.transform.GetComponent<Animation>();
         if (null != animation)
         {
                animation.Stop();
         }
#endif
    }

    //- 设置缩放值
    // 
    // @param value 要设置的缩放值
    // @return; none
    public void setScale(FixVector3 value)
    {
        m_fixv3LogicScale = value;

#if _CLIENTLOGIC_
        m_gameObject.transform.localScale = value.ToVector3();
#endif
    }

    //- 获取缩放值
    // 
    // @return; 缩放值
    public FixVector3 getScale()
    { 
        return m_fixv3LogicScale;
    }

    //- 设置旋转值
    // 
    // @param value 要设置的旋转值
    // @return; none
    public void setRotation(FixVector3 value)
    {
        m_fixv3LogicRotation = value;
#if _CLIENTLOGIC_
        m_gameObject.transform.localEulerAngles = value.ToVector3();
        setVisible(true);
#endif
    }

    //- 获取旋转值
    // 
    // @return; 旋转值
    public FixVector3 getRotation()
    {
        return m_fixv3LogicRotation;
    }

    //- 设置是否可见
    // 
    // @param value 是否可见
    // @return; none
    public void setVisible(bool value)
    {
#if _CLIENTLOGIC_
        m_gameObject.SetActive(value);
#endif
    }

    //- 删除gameobject
    // 
    // @return; none
    public void destroyGameObject()
    {
#if _CLIENTLOGIC_
        GameObject.Destroy(m_gameObject);
        m_gameObject.transform.localPosition = new Vector3(10000, 10000, 0);
#endif
    }

    //- 设置GameObject的名字
    // 
    // @param name 名字
    // @return; none
    public void setGameObjectName(string name)
    {
#if _CLIENTLOGIC_
        m_gameObject.name = name;
#endif
    }

    //- 获取GameObject的名字
    // 
    // @return; GameObject的名字
    public string getGameObjectName()
     {
#if _CLIENTLOGIC_
        return m_gameObject.name;
#else
		return "";
#endif
    }

    //- 设置位置
    // 
    // @param position 要设置到的位置
    // @return; none
    public void setGameObjectPosition(FixVector3 position)
    {
#if _CLIENTLOGIC_
        m_gameObject.transform.localPosition = position.ToVector3();
#endif
    }

    //- 获取位置
    // 
    // @return; 当前逻辑位置
    //public FixVector3 getPosition() {
    //     if (!GameData.g_client) { return new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);}

    //    return gameObject.transform.localPosition;
    // }

    //- 设置颜色
    // 
    // @param r 红
    // @param g 绿
    // @param b 蓝
    // @return; none
    public void setColor(float r, float g, float b)
    {
#if _CLIENTLOGIC_
        m_gameObject.GetComponent<SpriteRenderer>().color = new Color(r, g, b, 1);
#endif
    }
}
