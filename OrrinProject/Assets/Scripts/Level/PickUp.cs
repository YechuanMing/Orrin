using DG.Tweening;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public ResourceType type;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            switch (type)
            {
                case ResourceType.SpiritEnergy:
                    {
                        PlayerSpiritualization.Instance.currSpiritEnergy += 10;
                        break;
                    }
                case ResourceType.MaxHealth:
                    {
                        GameManager.Instance.playerDataObj.MaxHealth += 1;
                        break;
                    }
                case ResourceType.Health:
                    {
                        GameManager.Instance.currPlayer.GetComponent<Destructable>().Heal(1);
                        break;
                    }
                case ResourceType.MaxSpiritEnergy:
                    {
                        GameManager.Instance.playerDataObj.MaxSpiritEnergy += 30;
                        break;
                    }
            }
            //transform.DOScale(3f, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => { Destroy(this.gameObject); });
            Destroy(this.gameObject);
        }

    }

    public enum ResourceType
    {
        SpiritEnergy, Health, MaxHealth, MaxSpiritEnergy, ATK,JumpMaxTime,DashSkillGain,DoubleJumpGain,TeleportGain
    }



}
