using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class bodyTwoAttackOneBomb : MonoBehaviour
{
    [SerializeField]
    public int touchDamage = 1;
    [SerializeField]
    private float repelForce = 3;
    public AnimationCurve moveCurve_Y; // 自定义动画曲线
    public AnimationCurve moveCurve_X; // 自定义动画曲线

    public GameObject landingEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(touchDamage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            int i = collision.transform.position.x < this.transform.position.x ? 1 : -1;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.right * i + Vector2.up) * repelForce, ForceMode2D.Impulse);
            Destroy(gameObject, 4/60f);
        }
    }
    public void move(float x, float y, float t)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + y, t).SetEase(moveCurve_Y))
           .Join(transform.DOMoveX(transform.position.x + x, t).SetEase(moveCurve_X))
           .OnComplete(() =>
           {
               GameObject effect = Instantiate(landingEffect, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
               Destroy(gameObject, 4/60f);
           });

    }
}
