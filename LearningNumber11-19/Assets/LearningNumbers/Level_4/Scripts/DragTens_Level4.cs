using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragTens_Level4 : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    #region Getter
    private static DragTens_Level4 _instance;
    public static DragTens_Level4 Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DragTens_Level4>();
            }
            return _instance;
        }
    }
    #endregion
    private RectTransform rectTransform;
    private Canvas canvas_UI;
    private CanvasGroup canvas_Group;
    //public int ImgCount_i;
    public Image DragEndAns_Img;
    public Vector3 MyOriginal_Pos;
    public Vector3 MyDragEnd_Pos;
    public GameObject DragBoxs_Parnt;

    private void Awake()
    {       

        rectTransform = this.GetComponent<RectTransform>();
        canvas_UI = this.transform.GetComponentInParent<Canvas>();
        canvas_Group = this.GetComponent<CanvasGroup>();
        Default_Pos();       
    }

    void Default_Pos()
    {
        MyOriginal_Pos = this.transform.localPosition;//original pos    
        MyDragEnd_Pos = DragEndAns_Img.transform.localPosition;
        // Debug.Log("MyDragEnd_Pos}}}}}}}}}}}}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas_Group.alpha = 0.8f;
        canvas_Group.blocksRaycasts = false;
        // Debug.Log("onBegindrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas_UI.scaleFactor;
        //Debug.Log("ondrag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvas_Group.alpha = 1f;
        canvas_Group.blocksRaycasts = true;
        // Debug.Log("OnEndDrag");

        if (transform.localPosition == MyDragEnd_Pos)
        {
            this.transform.parent = DragEndAns_Img.transform;          
            
            if (Level_4_Game.Instance)
            {
                Level_4_Game.Instance.CheckTensBoxQuantity();
            }                
                      
            // Debug.Log("image is at end pos :: gone to MyDragEnd_Pos");
        }
        else
        {
            this.transform.parent = DragBoxs_Parnt.transform;
            //transform.localPosition = MyOriginal_Pos;
            iTween.MoveTo(this.gameObject, iTween.Hash("x", MyOriginal_Pos.x, "y", MyOriginal_Pos.y, "z", MyOriginal_Pos.z, "time", 0.1f, "delay", 0f, "islocal", true, "easeType", iTween.EaseType.linear));
            //  Debug.Log("image is not at end pos :: gone to original pos");
            if (Level_4_Game.Instance)
            {
                Level_4_Game.Instance.CheckTensBoxQuantity();
            }
        }
    }   
}


