using DG.Tweening;
using TMPro;
using UnityEngine;
public enum FoodEnum
{
    일반라멘, //lv2
    차슈라멘, 숙주라멘, 계란라멘, //lv3
    차계라멘, 숙차라멘, 숙계라멘, //lv4
    돈코츠라멘, 매운돈코츠라멘, 와사비돈코츠라멘, //lv5
}
public class DialogueData : MonoBehaviour
{
    public SystemManager systemManager;

    public TMP_Text orderText;
    public GameObject[] NPC;
    public GameObject speechbubble;
    public GameObject[] receipPic;
    public FoodEnum receip;

    public static DialogueData instance;

    private int npcNum;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }


    private readonly string[] _menuName = new[]{
            "일반 라멘 주세요", //일반
            "차슈 라멘 주세요", //차슈
            "숙주 라멘 주세요", //숙주
            "계란 라멘 주세요", //계란
            "차계 라멘 주세요", //차계
            "숙차 라멘 주세요", //숙차
            "숙계 라멘 주세요", //숙계
            "돈코츠 라멘 주세요", //돈코츠
            "매운 돈코츠 라멘 주세요", //고급와사비
            "와사비 돈코츠 라멘 주세요", // 매운
        };

    private void Start()
    {
        //주문 받을 때 FlowManaer에다가 생성하기
        Order();
    }

    /// <summary>
    /// 주문 할때 호출
    /// </summary>
    public void Order()
    {
        npcNum = Random.Range(0, 3);
        NPC[npcNum].SetActive(true);
        NPC[npcNum].transform.DOMove(new Vector3(-1.52f, 1.26f, 0), 1).OnComplete(() =>
        {
                //int orderDetails = 0;
                var selectMenu = Random.Range(0, System.Enum.GetValues(typeof(FoodEnum)).Length);
                /*switch (selectMenu)
                {
                    case 0:     //돈코츠라멘
                        orderDetails = 0;
                        break;
                    case 1:     //와사비라멘
                        orderDetails = 1;
                        break;
                    case 2:     //매운라멘
                        orderDetails = 2;
                        break;
                }*/
            speechbubble.SetActive(true);
            orderText.text = _menuName[selectMenu];
            receipPic[selectMenu].SetActive(true);
            systemManager.stop = false;

            //orderText.text = _menuName[orderDetails];
            receip = (FoodEnum)(selectMenu);
            FoodManager.instance.SetReceip(receip);

        });

    }

    public void OrderEnd()
    {
        systemManager.stop = true;
        NPC[npcNum].transform.DOMove(new Vector3(-3.8f, 1, 1), 1).OnComplete(() =>
          {
              orderText.text = "";
              speechbubble.SetActive(false);
              NPC[npcNum].SetActive(false);
              for (int i = 0; i < System.Enum.GetValues(typeof(FoodEnum)).Length; i++)
              {
                  receipPic[i].SetActive(false);
              }
              Order();
          });
    }
}
