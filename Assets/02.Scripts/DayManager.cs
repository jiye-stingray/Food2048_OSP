using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public MoneyCount moneyCount;
    public SystemManager systemManager;

    public GameObject clockBar;
    public GameObject dayUI;
    public GameObject gameOverUI;
    public GameObject resetButton;

    public Text cellsText;
    public Text materialCostText;
    public Text monthlyText;
    public Text totalCostsText;
    public Text moneyText;
    public TMP_Text income;

    public TMP_Text dayCountText;
    public Text dayCountText2;

    public int dayCount = 1;
    public float maxClock = 0;
    public float curClock = 0;
    public int cells; //ÆÈ¸²
    public int materialCost; //Àç·áºñ
    public int monthly = 20; //¿ù¼¼
    public int totalCosts; //ÃÑÇÕ

    public bool isfinish;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Delay();
        ClockCount();
        UI();
    }

    private void LateUpdate()
    {
        int a = cells;
        income.text = (moneyCount.money + cells) + "";
    }

    void Delay()
    {
        if (!isfinish)
            curClock += Time.deltaTime;
        else if (isfinish)
            curClock = 0;
    }

    void ClockCount()
    {
        if (maxClock <= curClock)
        {
            systemManager.Reset();
            systemManager.stop = true;
            dayUI.gameObject.SetActive(true);
            DayCalculate();
            isfinish = true;
        }
    }

    void UI()
    {
        clockBar.transform.localScale = new Vector3(1 - (curClock / maxClock), 1, 1);
        dayCountText.text = "Day " + dayCount;
        dayCountText2.text = "Day " + dayCount;
        cellsText.text = "+" + cells;
        materialCostText.text = "-" + materialCost;
        monthlyText.text = "-" + monthly;
        if (totalCosts >= 0) totalCostsText.text = "+" + totalCosts;
        else if (totalCosts < 0) totalCostsText.text = totalCosts.ToString();
        moneyText.text = moneyCount.money.ToString();
    }

    void DayCalculate()
    {
        materialCost = moneyCount.trashCount;
        totalCosts = cells - materialCost - monthly;
        moneyCount.money += totalCosts;
    }

    public void DayPassed()
    {
        if (moneyCount.money >= 0)
        {
            isfinish = false;
            systemManager.stop = false;
            dayCount++;
            DialogueData.instance.OrderEnd();
            dayUI.gameObject.SetActive(false);

            totalCosts = 0;
            cells = 0;
            moneyCount.trashCount = 0;
        }
        else if (moneyCount.money < 0)
        {
            dayUI.gameObject.SetActive(false);
            gameOverUI.SetActive(true);
            resetButton.SetActive(false);
        }
    }
}
