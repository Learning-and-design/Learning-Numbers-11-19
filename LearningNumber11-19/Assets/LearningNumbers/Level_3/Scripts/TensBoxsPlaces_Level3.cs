using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensBoxsPlaces_Level3 : MonoBehaviour
{
    #region Getter
    private static TensBoxsPlaces_Level3 _instance;
    public static TensBoxsPlaces_Level3 Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TensBoxsPlaces_Level3>();
            }
            return _instance;
        }
    }
    #endregion

    private int thisChildCount = 0;

    public Vector3[] BoxPostions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPosnOfTensBoxs()
    {
        thisChildCount = this.transform.childCount;
        //Debug.Log("thisChildCount::::" + thisChildCount);

        if ((thisChildCount >= 1) && (thisChildCount <= 9))
        {

            switch (thisChildCount)
            {
                case 1:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    break;
                case 2:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    break;
                case 3:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    break;
                case 4:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    this.transform.GetChild(3).gameObject.transform.localPosition = BoxPostions[3];
                    break;
                case 5:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    this.transform.GetChild(3).gameObject.transform.localPosition = BoxPostions[3];
                    this.transform.GetChild(4).gameObject.transform.localPosition = BoxPostions[4];
                    break;
                case 6:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    this.transform.GetChild(3).gameObject.transform.localPosition = BoxPostions[3];
                    this.transform.GetChild(4).gameObject.transform.localPosition = BoxPostions[4];
                    this.transform.GetChild(5).gameObject.transform.localPosition = BoxPostions[5];
                    break;
                case 7:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    this.transform.GetChild(3).gameObject.transform.localPosition = BoxPostions[3];
                    this.transform.GetChild(4).gameObject.transform.localPosition = BoxPostions[4];
                    this.transform.GetChild(5).gameObject.transform.localPosition = BoxPostions[5];
                    this.transform.GetChild(6).gameObject.transform.localPosition = BoxPostions[6];
                    break;
                case 8:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    this.transform.GetChild(3).gameObject.transform.localPosition = BoxPostions[3];
                    this.transform.GetChild(4).gameObject.transform.localPosition = BoxPostions[4];
                    this.transform.GetChild(5).gameObject.transform.localPosition = BoxPostions[5];
                    this.transform.GetChild(6).gameObject.transform.localPosition = BoxPostions[6];
                    this.transform.GetChild(7).gameObject.transform.localPosition = BoxPostions[7];
                    break;
                case 9:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    this.transform.GetChild(2).gameObject.transform.localPosition = BoxPostions[2];
                    this.transform.GetChild(3).gameObject.transform.localPosition = BoxPostions[3];
                    this.transform.GetChild(4).gameObject.transform.localPosition = BoxPostions[4];
                    this.transform.GetChild(5).gameObject.transform.localPosition = BoxPostions[5];
                    this.transform.GetChild(6).gameObject.transform.localPosition = BoxPostions[6];
                    this.transform.GetChild(7).gameObject.transform.localPosition = BoxPostions[7];
                    this.transform.GetChild(8).gameObject.transform.localPosition = BoxPostions[8];
                    break;
                default:
                    this.transform.GetChild(0).gameObject.transform.localPosition = BoxPostions[0];
                    this.transform.GetChild(1).gameObject.transform.localPosition = BoxPostions[1];
                    break;
            }
        }
    }
}
