using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IconStatus : MonoBehaviour
{
    [SerializeField]
    private string m_strParamName;
    public string paramName { get { return m_strParamName; } }
    private TextMeshProUGUI m_txtLabel;
    private TextMeshProUGUI m_txtRank;
    private TextMeshProUGUI m_txtCurrnet;
    private TextMeshProUGUI m_txtMax;
    private TextMeshProUGUI m_txtUp;

    private void Awake()
    {
        m_txtLabel = transform.Find("label").GetComponent<TextMeshProUGUI>();
        m_txtRank = transform.Find("txtRank").GetComponent<TextMeshProUGUI>();
        m_txtCurrnet = transform.Find("txtParam").GetComponent<TextMeshProUGUI>();
        m_txtMax = transform.Find("txtParamMax").GetComponent<TextMeshProUGUI>();
        m_txtUp = transform.Find("txtUp").GetComponent<TextMeshProUGUI>();
    }

    public void ShowUp(int _iParam)
    {
        string strParam = "";
        if( 0 < _iParam)
        {
            strParam = "+" + _iParam.ToString();
        }
        m_txtUp.text = strParam;
    }

    void Start()
    {
        SetParam(123);
        m_txtMax.text = "/456";
        //m_txtUp.text = "";
    }
    public void SetParam( int _iParam)
    {
        m_txtCurrnet.text = _iParam.ToString();
        m_txtRank.text = GetRank(_iParam);
    }

    public struct RankData
    {
        public int param;
        public string rank;
    };

    public string GetRank( int _iParam)
    {
        RankData[] rankDataArr = new RankData[]
        {
            new RankData(){param = 1200 , rank= "SS+"},
            new RankData(){param = 1100 , rank= "SS"},
            new RankData(){param = 1000 , rank= "S"},
            new RankData(){param =  900 , rank= "A+"},
            new RankData(){param =  800 , rank= "A"},
            new RankData(){param =  100 , rank= "G+"},
            new RankData(){param =    0 , rank= "G"},
        };

        foreach( RankData data in rankDataArr)
        {
            if( data.param <= _iParam)
            {
                return data.rank;
            }
        }
        return "none";
    }




    void Update()
    {
        
    }
}
