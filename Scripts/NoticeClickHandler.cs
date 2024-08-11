using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NoticeClickHandler : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        /*
        if (SceneManager.GetActiveScene().name == "Chapter2Scene")
        {

        }
       */

        this.gameObject.SetActive(false);
    }


}
