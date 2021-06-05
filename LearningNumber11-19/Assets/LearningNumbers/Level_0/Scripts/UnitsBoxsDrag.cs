using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitsBoxsDrag : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static UnitsBoxsDrag Instance;
    
    private RectTransform rectTransform;
    private Canvas canvas_UI;
    private CanvasGroup canvas_Group;

    public Vector3 ItemInitial_Pos, ItemDragEnd_Pos;

    public GameObject ParntObj;
    public GameObject UnitBoxPar;

    public static int childcount;

    void Awake()
    {
       Instance = this;

       GetInitProperties();
    }
    void Start()
    {
        ItemInitial_Pos = this.transform.localPosition;//original pos
        ItemDragEnd_Pos = ParntObj.transform.localPosition;//original pos
    }
        
    public void GetInitProperties()
    {
        rectTransform = this.GetComponent<RectTransform>();
        canvas_UI = this.transform.GetComponentInParent<Canvas>();
        canvas_Group = this.GetComponent<CanvasGroup>();

       //ItemInitial_Pos = this.transform.localPosition;//original pos
        //ItemDragEnd_Pos = ParntObj.transform.localPosition;//original pos
        //Debug.Log("GetInitProperties");
    }

    public void SetValues()
    {
        childcount = 0;
        //Debug.Log("childcount::unit.cs::"+ childcount);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvas_Group.alpha = 0.8f;
        canvas_Group.blocksRaycasts = false;
        
        //Debug.Log("onBegindrag");
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

        if (ItemDragEnd_Pos == transform.localPosition)
        {
            transform.parent = ParntObj.transform;

            childcount = childcount + 1;

          //  Debug.LogError("childcount::unitsboxdrag:::" + childcount);
            
            Level_0_Game.Instance.UnitBoxCard_Placed();

        }
        else
        {
            this.transform.localPosition = ItemInitial_Pos;
        }

        //Debug.Log("start & end are equal :" + ItemInitial_Pos);
    }
}

