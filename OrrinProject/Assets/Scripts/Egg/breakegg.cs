using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakegg : MonoBehaviour
{
    // 碎片预制体
    public GameObject fragmentPrefab;
    // 碎片数量
    public int fragmentCount = 10;
    // 碎片飞溅的速度范围
    public float minFragmentSpeed = 2f;
    public float maxFragmentSpeed = 5f;

    public void EggHit()
    {
        for (int i = 0; i < fragmentCount/3; i++)
        {
            // 实例化碎片
            GameObject fragment = Instantiate(fragmentPrefab, transform.position, Quaternion.identity);

            // 获取碎片的刚体组件
            Rigidbody2D fragmentRigidbody = fragment.GetComponent<Rigidbody2D>();

            // 生成随机方向和速度
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomSpeed = Random.Range(minFragmentSpeed, maxFragmentSpeed);

            // 设置碎片的初始速度
            fragmentRigidbody.velocity = randomDirection * randomSpeed;
        }
    }

    // 蛋被击碎的方法
    public void BreakEgg()
    {
        // 销毁蛋的精灵渲染器，使其不可见
        GetComponent<SpriteRenderer>().enabled = false;

        // 获取蛋的位置
        Vector2 eggPosition = transform.position;

        // 生成碎片
        for (int i = 0; i < fragmentCount; i++)
        {
            // 实例化碎片
            GameObject fragment = Instantiate(fragmentPrefab, eggPosition, Quaternion.identity);

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
