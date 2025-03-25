using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWave : MonoBehaviour
{
    public enum AttackDirection
    {
        Front,Up,Down
    }

    public AttackDirection direction;
    public int damage;

    [SerializeField]
    private float impulseParam=1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Gotcha");
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage);

            switch(direction)
            {
                case AttackDirection.Front:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right*transform.localScale.x * damage * impulseParam, ForceMode2D.Impulse);
                    PlayerController.Instance.GetComponent<Rigidbody2D>().AddForce(Vector2.right * transform.localScale.x * damage * impulseParam * (-1), ForceMode2D.Impulse);
                    break;
                case AttackDirection.Up:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * damage * impulseParam, ForceMode2D.Impulse);
                    break;
                case AttackDirection.Down:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * damage * impulseParam, ForceMode2D.Impulse);
                    PlayerController.Instance.GetComponent<Rigidbody2D>().AddForce(Vector2.up * damage * impulseParam, ForceMode2D.Impulse);
                    break;

            }
            PlayerSpiritualization.Instance.HitAddSpirit();
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - this.transform.position) * damage * impulseParam, ForceMode2D.Impulse);
        }
        else if(collision.gameObject.CompareTag("Destructable"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(1);

        }
    }
}
