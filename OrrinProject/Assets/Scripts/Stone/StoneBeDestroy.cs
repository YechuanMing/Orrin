using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBeDestroy : MonoBehaviour
{
    public int fragmentCount = 10;
    public float minFragmentSpeed = 2f;
    public float maxFragmentSpeed = 5f;

    public GameObject[] fragments;
    // Start is called before the first frame update
    void Start()
    {
        // 初始化碎片数组
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void stoneBroken()
    {
        // 销毁蛋的精灵渲染器，使其不可见
        GetComponent<SpriteRenderer>().enabled = false;

        // 获取蛋的位置
        Vector2 eggPosition = transform.position;

        // 生成碎片
        for (int i = 0; i < fragmentCount; i++)
        {
            // 生成随机旋转角度（Z轴）
            float randomRotationZ = Random.Range(0f, 360f);

            GameObject selectedFragment = fragments[Random.Range(0, fragments.Length)];
            // 实例化碎片
            GameObject fragment = Instantiate(selectedFragment, eggPosition, Quaternion.Euler(0f, 0f, randomRotationZ));

            // 获取碎片的刚体组件
            Rigidbody2D fragmentRigidbody = fragment.GetComponent<Rigidbody2D>();

            // 生成随机方向和速度
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomSpeed = Random.Range(minFragmentSpeed, maxFragmentSpeed);

            // 设置碎片的初始速度
            fragmentRigidbody.velocity = randomDirection * randomSpeed;
        }

        // 销毁蛋对象（可选，如果不再需要蛋对象）
        Destroy(gameObject, 0.5f); // 延迟销毁，避免在生成碎片时出现错误
    }
}
