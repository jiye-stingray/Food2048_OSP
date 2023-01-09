using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _02.Scripts.Lee_Sanghyuk
{
    public enum FoodEnum
    {
        일반라멘, //lv1
        차슈라멘, 숙주라멘, 계란라멘, 와사비라멘, 매운라멘, //lv2
        숙차라멘, 차계라멘, 와계라멘, 계매라멘, //lv3
        돈코츠라멘, 고급와사비라멘, 차슈매운라멘, //lv4
        궁극라멘 //lv5
    }
    public class DialogueData : MonoBehaviour
    {
        public TMP_Text orderText;
        public GameObject[] NPC;
        public GameObject speechbubble;
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


        private readonly string[] _menuName = new []{
            "일반 라멘 주세요", //일반
            "차슈 라멘 주세요", //차슈
            "숙주 라멘 주세요", //숙주
            "계란 라멘 주세요", //계란
            "와사비 라멘 주세요", //와사비
            "매운 라멘 주세요", //매운
            "숙차 라멘 주세요", //숙차
            "차계 라멘 주세요", //차계
            "와계 라멘 주세요", //와계
            "계매 라멘 주세요", //계매
            "돈코츠 라멘 주세요", //돈코츠
            "고급 와사비 라멘 주세요", //고급와사비
            "매운 라멘 주세요", // 매운
            "다 넣어 주세요", // 궁극
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
                //orderText.text = _menuName[orderDetails];
                receip = (FoodEnum)(selectMenu);
                FoodManager.instance.SetReceip(receip);

            });

        }

        public void OrderEnd()
        {
            NPC[npcNum].transform.DOMove(new Vector3(-3.8f,1,1), 1).OnComplete(() =>
            {
                orderText.text = "";
                speechbubble.SetActive(false);
                NPC[npcNum].SetActive(false);
                Order();
            });
        }
    }
}
