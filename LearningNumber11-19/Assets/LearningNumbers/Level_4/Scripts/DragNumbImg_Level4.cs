using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragNumbImg_Level4 : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    #region Getter
    private static DragNumbImg_Level4 _instance;
    public static DragNumbImg_Level4 Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DragNumbImg_Level4>();
            }
            return _instance;
        }
    }
    #endregion

    private RectTransform rectTransform;
    private Canvas canvas_UI;
    private CanvasGroup canvas_Group;
    public int ImgVal_i;
    public Image DragEndAns_Img;
    public Vector3 MyOriginal_Pos;
    public Vector3 MyDragEnd_Pos;
    public GameObject DragBoxs_Parnt;

    private void Awake()
    {
        rectTransform = this.GetComponent<RectTransform>();
        canvas_UI = this.transform.GetComponentInParent<Canvas>();
        canvas_Group = this.GetComponent<CanvasGroup>();
        Default_EndPosforNumbCard();
    }

    void Default_EndPosforNumbCard()
    {       
        MyDragEnd_Pos = DragEndAns_Img.transform.localPosition;
        //Debug.Log("+++++++++++++image is at end pos :: gone to MyDragEnd_Pos"+ ImgVal_i);
        // //Debug.Log("MyDragEnd_Pos}}}}}}}}}}}}");       
    }

    public void AfterSetting_PosOfNumbCard()
    {
        MyOriginal_Pos = this.transform.localPosition;//original pos    
        // //Debug.Log("enter position original");
        //Debug.LogError("+++++++++++++image is at end pos :: gone to MyDragEnd_Pos" + this.transform.localPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas_Group.alpha = 0.8f;
        canvas_Group.blocksRaycasts = false;
        // //Debug.Log("onBegindrag");
    }
    public void OnDrag(PointerEventData eventData)
    {       
        rectTransform.anchoredPosition += eventData.delta / canvas_UI.scaleFactor;
        ////Debug.Log("ondrag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvas_Group.alpha = 1f;
        canvas_Group.blocksRaycasts = true;
        // //Debug.Log("OnEndDrag");

        if (transform.localPosition == MyDragEnd_Pos)
        {
            this.transform.parent = DragEndAns_Img.transform;
            this.transform.localPosition = new Vector3(0f,0f,0f);

            if (Level_4_Game.Instance)
            {
                Level_4_Game.Instance.CheckNumbrsBoxQuantity(ImgVal_i);
            }                        
            //Debug.Log("+++++++++++++image is at end pos :: gone to MyDragEnd_Pos"+ ImgVal_i);
        }
        else
        {
            this.transform.parent = DragBoxs_Parnt.transform;
            this.transform.localPosition = MyOriginal_Pos;

            //Debug.Log("image is not at end pos :: gone to original pos");
                        
            /*if (Level_4_Game.Instance)
            {
                Level_4_Game.Instance.SetPostionsOfNumbrsObjs();
                // Level_4_Game.Instance.SetPosNumbCards();
            }*/
            
            /*if (Level_4_Game.Instance)
            {
                Level_4_Game.Instance.CheckNumbrsBoxQuantity(ImgVal_i);
            }*/
        }
    }

}


