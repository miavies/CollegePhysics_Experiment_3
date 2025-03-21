using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



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
    public GameObject mawStats, shawStats;

    public Image FUp;
    public Image JUp;
    public GameObject keys;

    public Image shawWin, mawWin, draw;
    public Button restart;

    public float sliderSpeed = 5f;
    public float influenceAmount = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = mawPrefab.GetComponent<StartTime>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = sliderValue;
        countDown.enabled = false;
        timerText.enabled = false;
        shawWin.enabled = false;
        mawWin.enabled = false;
        draw.enabled = false;
        restart.gameObject.SetActive(false);
        restart.interactable = false;
        shawStats.gameObject.SetActive(false);
        mawStats.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (startTime.startTime == true)
        {
            time -= Time.deltaTime;

            countDown.enabled = true;
            for (int i = 3; i + 6 > time; i--)
            {
                if (i <= -1)
                {
                    countDown.enabled = false;
                    break;
                }

                if (i <= 0)
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
                Winner();
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
        bool shawPressed = false;
        bool mawPressed = false;

        if (Input.GetKeyDown(KeyCode.F))
        {
            shawCounter++;
            FUp.enabled = false;
            shawPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            FUp.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            mawCounter++;
            JUp.enabled = false;
            mawPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            JUp.enabled = true;
        }

        if (shawPressed && !mawPressed)
        {
            sliderValue += influenceAmount;
        }
        else if (mawPressed && !shawPressed)
        {
            sliderValue -= influenceAmount;
        }
        sliderValue = Mathf.Clamp(sliderValue, 0f, 1f);

        slider.value = Mathf.Lerp(slider.value, sliderValue, Time.deltaTime * sliderSpeed);
    }

    public void Winner()
    {
        if (mawCounter > shawCounter)
        {
            mawAnimator.SetBool("Win", true);
            shawAnimator.SetBool("Loss", true);
            Invoke("MawVictory", 3f);
        }

        if (shawCounter > mawCounter)
        {
            mawAnimator.SetBool("Loss", true);
            shawAnimator.SetBool("Win", true);
            Invoke("ShawVictory", 3f);
        }

        if(shawCounter == mawCounter)
        {
            Invoke("Draw", 2f);
        }

    }

    private void MawVictory()
    {
        mawAnimator.SetBool("Victory", true);
        mawWin.enabled = true;
        StartCoroutine(RotateSmoothly(mawAnimator.transform, Quaternion.Euler(0, 0, 0), 0.5f));

        restart.gameObject.SetActive(true);
        restart.interactable = true;
        mawStats.gameObject.SetActive(true);
    }

    private void ShawVictory()
    {
        shawAnimator.SetBool("Victory", true);
        shawWin.enabled = true;
        StartCoroutine(RotateSmoothly(shawAnimator.transform, Quaternion.Euler(0, 0, 0), 0.5f));

        restart.gameObject.SetActive(true);
        restart.interactable = true;
        shawStats.gameObject.SetActive(true);
    }

    private void Draw()
    {
        draw.enabled = true;
        restart.gameObject.SetActive(true);
        restart.interactable = true;
    }


    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator RotateSmoothly(Transform character, Quaternion targetRotation, float duration)
    {
        float time = 0;
        Quaternion startRotation = character.rotation;

        while (time < duration)
        {
            character.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        character.rotation = targetRotation; // Ensure exact final rotation
    }

}
