using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class Product : MonoBehaviour
{

    public ProductData productData;

    public Color FlashColor;
    public Color BaseColor;
    public Color SelectColor;
    public Color SoldOutColor;
    public Color PurchaseFailedColor;

    public bool soldOut;

    public Image image;
    public Image squareImage;
    public Text title;
    public Text description;
    public Text price;

    public Tween colorTween;


    public void Start()
    {
        //squareImage = GetComponent<Image>();


        if (productData!=null)
        {
            UpdateDisplay();
            if(productData.amount==0)
            {
                soldOut = true;
                SoldOutEffect();
            }
        }
    }

    public void UpdateDisplay()
    {
        image.sprite = productData.sprite;
        title.text = productData.title;
        description.text = productData.description;
        price.text = productData.price.ToString();
    }
    public void Purchase()
    {
        if(soldOut||GameManager.Instance.money<productData.price)
        {
            //
            PurchaseFailedEffect();
            return;
        }

        productData.amount -= 1;
        if(productData.amount==0)
        {
            soldOut = true;
            PurchaseEffect();
        }
        else
        {
            productData.price += 10;
        }

        UpdateDisplay();
    }
    public void PurchaseEffect()
    {
        colorTween.Kill();
        colorTween=squareImage.DOColor(FlashColor, 0.3f).OnComplete(() => { squareImage.DOColor(SelectColor, 0.4f); });

    }

    public void OnSelectEffect()
    {
        colorTween.Kill();
        squareImage.DOColor(SelectColor, 0.3f);
        transform.DOScale(1.1f, 0.5f);
    }

    public void OnDeSelectEffect()
    {
        colorTween.Kill();
        squareImage.DOColor(BaseColor, 0.3f);
        transform.DOScale(1f, 0.5f);
    }

    public void SoldOutEffect()
    {
        colorTween.Kill();
        squareImage.DOColor(SoldOutColor, 0.3f);
    }

    public void PurchaseFailedEffect()
    {
        colorTween.Kill();
        squareImage.DOColor(PurchaseFailedColor, 0.3f);
    }

}
