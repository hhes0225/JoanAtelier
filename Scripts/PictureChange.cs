using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PictureChange : MonoBehaviour
{


    public void OnTabSelected()
    {
        this.gameObject.SetActive(true);
        this.GetComponent<Image>().DOFade(1, 1.5f);
    }

    public void OnTabDeselected()
    {
        this.GetComponent<Image>().DOFade(0, 1.5f);
        this.gameObject.SetActive(false);
    }

    #region ¾È¾²´Â »¹Áþ
    /*
    public void OnSlotSelected()
    {
        if (pictures[0].activeSelf)
        {
            Debug.Log("name: " + this.name);
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (pictures[1].activeSelf )
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("slot selected error");
            Debug.Log("name: " + this.name);
        }
    }

    public void OnSlotDeselected()
    {
        if (pictures[0].activeSelf)
            this.transform.GetChild(0).gameObject.SetActive(false);
        else if (pictures[1].activeSelf)
            this.transform.GetChild(1).gameObject.SetActive(false);
        else
            Debug.Log("slot deselected error");
    }
    */
    #endregion
}
