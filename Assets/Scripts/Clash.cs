using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clash : MonoBehaviour
{

    public Animator mawAnimator;
    public Animator shawAnimator;

    public Slider slider;
    public int shawCounter = 0;
    public int mawCounter = 0;
    public int totalCount;
    public float sliderValue = 0.5f;

    public GameObject mawPrefab;
    public StartTime startTime;
    public float time = 9f;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI timerText;

    public Image FUp;
    public Image JUp;
    public GameObject keys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = mawPrefab.GetComponent<StartTime>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = sliderValue;
        countDown.enabled = false;
        timerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime.startTime == true)
        {
            time -= Time.deltaTime;

            countDown.enabled = true;
            for (int i = 3; i+6 > time; i--) 
            {
                if (i <= -1)
                {
                    countDown.enabled = false;
                    break;
                }

                if(i <= 0) 
                {
                    countDown.text = "START";
                    continue;
                }
                countDown.text = (i).ToString();
            }
            
            if (time <= 5 && time >= 0)
            {
                Counter();
                timerText.enabled = true;
                timerText.text = time.ToString("F0");
            }
            
            if (time <= 0)
            {
                time = 0;
                timerText.enabled = false;
                keys.SetActive(false);
                Collide();
            }
        }  
    }

    public void Collide()
    {
        shawAnimator.SetBool("Punch", true);
        mawAnimator.SetBool("Punch", true);
    }

    public void Counter()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shawCounter++;
            FUp.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            FUp.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            mawCounter++;
            JUp.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            JUp.enabled = true;
        }
    

        totalCount = shawCounter + mawCounter;
        if (totalCount > 0)
        {
            sliderValue = (float)shawCounter / totalCount;
        }
        else
        {
            sliderValue = 0.5f;
        }
        slider.value = sliderValue;
    }
}
