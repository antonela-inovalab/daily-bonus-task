using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DailyBonus : MonoBehaviour
{
    private Button button;
    private GameObject[] allDays;
    private Animator animator;
    public GameObject textToMove;
    public GameObject[] lockObj;
    public GameObject[] tick;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("http://18.184.208.163:8080/api/test/dailyBonus"));
        allDays = GameObject.FindGameObjectsWithTag("Days");
        animator = textToMove.GetComponent<Animator>();

        lockObj=GameObject.FindGameObjectsWithTag("Locks");
        tick = GameObject.FindGameObjectsWithTag("Ticks");

    }

    IEnumerator GetRequest(string url)
    {

        Days daysData = new Days();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else {

                daysData = JsonUtility.FromJson<Days>("{\"days\": "+request.downloadHandler.text+" }");

                for (int i=0; i<6; i++)
                {
                    button = allDays[i].GetComponentInChildren<Button>();
                    TMPro.TextMeshProUGUI day = button.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0];
                    TMPro.TextMeshProUGUI coins = button.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];
                    string status = daysData.days[i].status;
                    Image lockOb = lockObj[i].GetComponent<Image>();
                    Image tickOb = tick[i].GetComponent<Image>();

                    //setting the values of the coins and day number on each day
                    day.text += " " + daysData.days[i].day;
                    coins.text ="+"+daysData.days[i].coins.ToString();


                    if (daysData.days[i].status.Equals("locked"))
                    {
                        lockOb.enabled = true;
                    }

                    if (daysData.days[i].status.Equals("open"))
                    {
                        tickOb.enabled=true;
                    }


                    if (!daysData.days[i].status.Equals("open"))
                    {

                        button.onClick.AddListener(() =>
                    {
                        PlayAnimation(coins, status, lockOb, tickOb);
                    });

                    }
                     
                   
                }

            }

        }

    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(1);

        animator.enabled = false;
        textToMove.SetActive(false);
        animator.SetBool("buttonClicked", false);
    }

    public void PlayAnimation(TMPro.TextMeshProUGUI coins, string status, Image lockObj, Image tick) {


        if (status.Equals("locked"))
        {
            lockObj.enabled = false;
            tick.enabled = true;
        }

        textToMove.SetActive(true);
            animator.enabled = true;
            animator.SetBool("buttonClicked", true);
            textToMove.GetComponent<TMPro.TextMeshProUGUI>().text = coins.text;
            StartCoroutine(DelayAction());


    }

}
