using _02.Scripts.Lee_Sanghyuk;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public bool isMerging = false;
    public static FoodManager instance = null;

    public FoodEnum receip;
    [SerializeField] GameObject trashPrefab;

    public FlowManager flowManager;

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

    /// <summary>
    /// 스테이지 레시피 설정
    /// </summary>
    /// <param name="food"></param>
    public void SetReceip(FoodEnum food)
    {
        Debug.Log(food);
        receip = food;
    }

    /// <summary>
    /// merge 가능한지 체크
    /// </summary>
    /// <param name="food1"></param>
    /// <param name="food2"></param>
    public GameObject TryMerge(Food food1, Food food2, Vector2 Pos)
    {
        if ((food1.myLevel == 0 && food2.myLevel == 0) || (food1.myLevel == 1 && food2.myLevel == 1 && food1.foodName == food2.foodName)) //둘 다 레벨 0일 때
        {
            return Merge(food1, food2, Pos); // food1을 삭제 후 병합
        }
        else if ((food1.myLevel == 1 && food2.myLevel > 1) || (food2.myLevel == 1 && food1.myLevel > 1))
        {
            Debug.Log("콤바인 작동");
            return Merge(food1, food2);
            /*if (food1.myLevel == 4)       //기본 라멘에서 추가 제료가 들어가는 상태라면 (완성 상태 체크 = LV4) 
            {
                Food mergeFood = food1.isRamen == true ? food1 : food2; //
                Food deFood = food1.isRamen == false ? food1 : food2;
                return Merge(mergeFood, deFood);
            }
            else //Lv4 이하일 때
            {
                //Destroy(food2.gameObject);
                return Merge(food1, Pos); // food1을 삭제 후 병합
            }*/
        }

        return food2.gameObject;
    }

    private GameObject Merge(Food food1, Food food2,  Vector2 Pos) //병합 Lv4 < Object
    {
        Debug.Log("food1의 이름은: " + food1.foodName);
        Food mergeFood = null;
        
        if(food1.myLevel <= 1 && food1.foodName == food2.foodName)
            mergeFood = food1.nextFood[0].GetComponent<Food>();
        else
            mergeFood = food1.nextFood[1].GetComponent<Food>();

        if (food1.myLevel != 1)
            Destroy(food1.gameObject); //food 삭제

        isMerging = true;

        return mergeFood.gameObject;
    }

    private GameObject Merge(Food food1, Food food2) //병합 Lv4 >= Object
    {
        Debug.Log("food1의 이름은: " + food1.foodName + " food2의 이름은: " + food2.foodName);
        Food mergeFood = null;
        Transform trans = food1.gameObject.transform;

        if(food2.myLevel == 1) //재료가 앞에 있을 경우 병합하려는 대상을 서로 바꿔준 후 계산
        {
            Food changeFood = null;
            changeFood = food1;
            food1 = food2;
            food2 = changeFood;
        }

        Debug.Log("food1의 이름은: " + food1.foodName + " food2의 이름은: " + food2.foodName);

        switch (food2.myLevel)
        {
            case 2: //lv2
                switch (food1.foodName)
                {
                    case "차슈":
                        mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                        isMerging = true;
                        break;
                    case "계란":
                        mergeFood = mergeFood = food2.nextFood[1].GetComponent<Food>();
                        isMerging = true;
                        break;
                    case "숙주":
                        mergeFood = mergeFood = food2.nextFood[2].GetComponent<Food>();
                        isMerging = true;
                        break;
                    default:
                        mergeFood = food2;
                        isMerging = false;
                        break;
                }
                break;
            case 3: //lv3
                if(food2.foodName == "차슈라멘")
                {
                    switch (food1.foodName)
                    {
                        case "계란":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        case "숙주":
                            mergeFood = mergeFood = food2.nextFood[1].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                else if (food2.foodName == "숙주라멘")
                {
                    switch (food1.foodName)
                    {
                        case "차슈":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        case "계란":
                            mergeFood = mergeFood = food2.nextFood[1].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                else if (food2.foodName == "계란라멘")
                {
                    switch (food1.foodName)
                    {
                        case "차슈":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        case "숙주":
                            mergeFood = mergeFood = food2.nextFood[1].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                break;
            case 4:
                if (food2.foodName == "차계라멘")
                {
                    switch (food1.foodName)
                    {
                        case "숙주":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                else if (food2.foodName == "숙차라멘")
                {
                    switch (food1.foodName)
                    {
                        case "계란":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                else if (food2.foodName == "숙계라멘")
                {
                    switch (food1.foodName)
                    {
                        case "차슈":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                break;
            case 5:
                if (food2.foodName == "돈코츠라멘")
                {
                    switch (food1.foodName)
                    {
                        case "와사비":
                            mergeFood = mergeFood = food2.nextFood[0].GetComponent<Food>();
                            isMerging = true;
                            break;
                        case "고추기름":
                            mergeFood = mergeFood = food2.nextFood[1].GetComponent<Food>();
                            isMerging = true;
                            break;
                        default:
                            mergeFood = food2;
                            isMerging = false;
                            break;
                    }
                }
                break;
            default:
                mergeFood = food2;
                isMerging = false;
                break;
        }

        if (mergeFood.isRamen)      //마지막 음식일때 체크
        {
            CheckReceip(mergeFood);
        }

        if(isMerging)
            Destroy(food2.gameObject);

        //Destroy(food2.gameObject);

        return mergeFood.gameObject;

    }

    private GameObject CheckReceip(Food food) //레시피 체크(성공적으로 병합되었나? or 실패하였나?)
    {
        
        if (food.myFood == receip) //&& food.myLevel >= 4)
        {
            //성공
            flowManager.MenuSuccess();
            Debug.Log("fddfdgdg");
            return food.gameObject;
        }
        else
        {
            //실패 => 쓰레기
            return trashPrefab;
        }
        
    }

}
