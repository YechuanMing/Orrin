using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spirit2WhichStateMask : MonoBehaviour
{
    public SoulsBeDestroyInStage2 soulsBeDestroy;
    public bool shocked1=false;//灵魂第一阶段被摧毁
    public bool shocked2 = false;//灵魂第二阶段也被摧毁了
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shocked1 = soulsBeDestroy.shock1;
        shocked2 = soulsBeDestroy.shock2;
    }
}
