using UnityEngine;

public class SuperSpecial : MonoBehaviour
{
    Vector2 origin;

    SpriteRenderer sr;

    [SerializeField]
    private bool shakes;
    [SerializeField]
    private bool rainbow;

    [SerializeField]
    private float shakeIntensity;
    [SerializeField]
    private int maxShakeDelay;
    [SerializeField]
    private float rainbowSpeed;

    private int rainbowState = 0;
    private int shakeDelay = 0;
    private int shakeTime = 0;
    private Color cc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = gameObject.transform.position;
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (rainbow) cc = Color.red;
        if (shakes) shakeDelay = Random.Range(0, maxShakeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rainbow)
        {
            Tripy();
            sr.color = cc;
        }
        if (shakes)
        {
            if (shakeTime >= shakeDelay)
            {
                Vector2 randShake = Random.insideUnitCircle * Random.Range(0, shakeIntensity);
                gameObject.transform.position = origin + randShake;
                shakeTime = 0;
                shakeDelay = Random.Range(maxShakeDelay/2, maxShakeDelay);
            } else
            {
                shakeTime++;
            }
        }
    }

    void Tripy()
    {
        float temp = 0;
        switch (rainbowState)
        {
            case 0: // green ++
                temp = cc.g + rainbowSpeed * Time.fixedDeltaTime;
                if (temp > 1)
                {
                    cc.g = 1;
                    cc.r -= (temp - 1f);
                    rainbowState++;
                } else
                {
                    cc.g = temp;
                }
                break;
            case 1: // red --
                temp = cc.r - rainbowSpeed * Time.fixedDeltaTime;
                if (temp < 0)
                {
                    cc.r = 0;
                    cc.b = (temp - 1f);
                    rainbowState++;
                }
                else
                {
                    cc.r = temp;
                }
                break;
            case 2: // blue ++
                temp = cc.b + rainbowSpeed * Time.fixedDeltaTime;
                if (temp > 1)
                {
                    cc.b = 1;
                    cc.g -= (temp - 1f);
                    rainbowState++;
                }
                else
                {
                    cc.b = temp;
                }
                break;
            case 3: // green --
                temp = cc.g - rainbowSpeed * Time.fixedDeltaTime;
                if (temp < 0)
                {
                    cc.g = 0;
                    cc.r = (temp - 1f);
                    rainbowState++;
                }
                else
                {
                    cc.g = temp;
                }
                break;
            case 4: // red ++
                temp = cc.r + rainbowSpeed * Time.fixedDeltaTime;
                if (temp > 1)
                {
                    cc.r = 1;
                    cc.b -= (temp - 1f);
                    rainbowState++;
                }
                else
                {
                    cc.r = temp;
                }
                break;
            case 5: // blue --
                temp = cc.b - rainbowSpeed * Time.fixedDeltaTime;
                if (temp < 0)
                {
                    cc.b = 0;
                    cc.g = (temp - 1f);
                    rainbowState = 0;
                }
                else
                {
                    cc.b = temp;
                }
                break;
        }
    }
}
