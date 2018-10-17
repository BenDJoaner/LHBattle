﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectChar : MonoBehaviour
{
    public RectTransform CharTable;
    public Text Name;
    public Image CharIcon;
    public Text Desc;
    public Text Skill;
    public Text Pos;
    public Button formPageBtn;
    public Button backPageBtn;
    public Button useCharBtn;

    LHNetworkPlayer LocalPlayer;
    LHNetworkGameManager manager;
    CharacterConfig charListObj;
    string[] PosList = new string[] { "迅捷", "强攻", "支援" };
    int PosIndex;
    CharacterData selectedChar=null ;//选择的角色
    // Use this for initialization
    void Start()
    {
        manager = GetComponent<LHNetworkGameManager>();
        formPageBtn.onClick.AddListener(FromtPage);
        backPageBtn.onClick.AddListener(BackPange);
        useCharBtn.onClick.AddListener(OnClickeUseChar);
    }

    /// <summary>
    /// 传入角色arr和玩家对象
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="_player"></param>
    public void Init(CharacterConfig obj, LHNetworkPlayer _player)
    {
        charListObj = obj;
        LocalPlayer = _player;
        OnSetArrData();
    }

    /// <summary>
    /// 1、根据定位刷新面板
    /// 2、载入当前定位的角色到面板中
    /// </summary>
    /// <param name="index"></param>
    public void OnSetArrData()
    {
        if(charListObj.GetAllList().Count<=0){
            Debug.LogError("没有角色数据！");
            return;
        }
        Pos.text = PosList[PosIndex];//定位文字

        foreach(RectTransform chiled in CharTable)
        {
            Destroy(chiled.gameObject);
        }

        List<CharacterData> tempArr = new List<CharacterData> { };

        switch(PosIndex){
            case 0:
                tempArr=charListObj.GetSlipyList();
                break;
            case 1:
                tempArr=charListObj.GetAttackList();
                break;
            case 2:
                tempArr=charListObj.GetSupportList();
                break;
            default:
                Debug.LogError("无此类型角色："+PosIndex);
                break;

        }

        //if (tempArr.Count >= 0) return;

        Button iteam = AssetConfig.GetPrefabByName("CharBtnIteam").GetComponent<Button>();
        foreach (CharacterData data in tempArr)
        {
            //print(data.charID);
            Button CharBtn = Instantiate(iteam);
            CharBtn.transform.SetParent(CharTable, true);
            CharBtn.image.sprite = data.image;
            CharBtn.onClick.AddListener(delegate ()
            {
                SelectCharactorFunc(data.charID);
            });
        }
        //翻页默认选择第一个
        SelectCharactorFunc(tempArr[0].charID);
    }

    /// <summary>
    /// 根据设置角色相关信息
    /// </summary>
    /// <param name="data"></param>
    void OnSetInfo(CharacterData data)
    {
        Name.text = data.name;
        CharIcon.sprite = data.image;
        Desc.text = data.charDesc;
        Skill.text = data.skillDesc;
    }

    /// <summary>
    /// 点击选择角色
    /// </summary>
    /// <param name="id"></param>
    public void SelectCharactorFunc(int id)
    {
        selectedChar=charListObj.GetCharByID(id);
        OnSetInfo(selectedChar);
    }

    public void OnClickeUseChar()
    {
        LocalPlayer.Init(selectedChar);
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 获取角色ID
    /// </summary>
    /// <returns></returns>
    public int GetCharID()
    {
        return selectedChar.charID;
    }

    /// <summary>
    /// 向前翻页
    /// </summary>
    public void FromtPage()
    {
        PosIndex += 1;
        if (PosIndex > 2)
        {
            PosIndex = 0;
        }
        OnSetArrData();
    }

    /// <summary>
    /// 向后翻页
    /// </summary>
    public void BackPange()
    {
        PosIndex -= 1;
        if (PosIndex < 0)
        {
            PosIndex = 2;
        }
        OnSetArrData();
    }
}