using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SingleCardNumberDrag : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static SingleCardNumberDrag Instance;

    private RectTransform rectTransform;
    private Canvas canvas_UI;
    private CanvasGroup canvas_Group;
    //public int ImgCount_i;
    public Image DragEndAns_Img;
    public Vector3 MyOriginal_Pos;
    public Vector3 MyDragEnd_Pos;

    private void Awake()
    {
        Instance = this;

        rectTransform = this.GetComponent<RectTransform>();
        canvas_UI = this.transform.GetComponentInParent<Canvas>();
        canvas_Group = this.GetComponent<CanvasGroup>();
        Default_Pos();
        //Invoke("Def_Pos", 2f);
    }

    void Default_Pos()
    {
        MyOriginal_Pos = this.transform.localPosition;//original pos    
        MyDragEnd_Pos = DragEndAns_Img.transform.localPosition;
        //Debug.Log("MyDragEnd_Pos}}}}}}}}}}}}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas_Group.alpha = 0.8f;
        canvas_Group.blocksRaycasts = false;
        //Debug.Log("onBegindrag");

        /*if (Game_B02071.Instance)
        {
            Game_B02071.Instance.ChangeParentOfNumb(ImgCount_i);
        }*/

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
            if(Level_0_Game.Instance)
            {
                Level_0_Game.Instance.SingleCardDraged_AnswersEvaluation();
            }
          //  Debug.Log("image is at end pos :: gone to MyDragEnd_Pos");
        }
        else
        {

            //transform.localPosition = MyOriginal_Pos;
            iTween.MoveTo(this.gameObject, iTween.Hash("x", MyOriginal_Pos.x, "y", MyOriginal_Pos.y, "z", MyOriginal_Pos.z, "time", 0.2f, "delay", 0.1f, "islocal", true, "easeType", iTween.EaseType.linear));

          //  Debug.Log("image is not at end pos :: gone to original pos");
        }

    }
}
