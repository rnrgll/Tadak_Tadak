using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [HideInInspector] public float defaultSpeed = 5f; // 기본 이동 속도
    [HideInInspector] public float minSpeed = 0.5f; // 최저 속도
    [HideInInspector] public float breakRate = 2f; // 감속 계수 (클수록 감속이 더 빨라짐)
    [HideInInspector] public float accelerationRate = 2f; // 가속 계수 (클수록 빠르게 원속도로 복귀)

    private float currentSpeed;
    private float holdTime = 0f; // Shift 누른 시간
    private float releaseTime = 0f; // Shift 뗀 후 시간
    private bool isSlowing = false; // 감속 중인지 체크
    private bool isStop = false;
    public bool isChecked = false;
    
    public int waterAmount = 0;
    private Rigidbody2D _rb;
    
    public Canvas canvas;
    public CheckScore checkScore;

    [SerializeField] private GameObject _dropCount;
    public float dropCountOffsetX;
    public float dropCountOffsetY;
    private GameObject _countText;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _countText = Instantiate(_dropCount, canvas.transform);
    }

    void Start()
    {
        currentSpeed = defaultSpeed;
    }

    void Update()
    {
        if (isStop)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            isSlowing = true;
            holdTime += Time.deltaTime;
            releaseTime = 0f; // 감속 중이면 가속 타이머 초기화
            float speedDecrease = Mathf.Exp(holdTime* breakRate);
            currentSpeed = Mathf.Max(defaultSpeed - speedDecrease, minSpeed); // minSpeed 이하로 안 내려감
        }
        else
        {
            if (isSlowing) // 감속 후 가속 시작
            {
                isSlowing = false;
                holdTime = 0f;
            }
            releaseTime += Time.deltaTime;
            float speedIncrease = (1 - Mathf.Exp(-releaseTime * accelerationRate)) * (defaultSpeed - minSpeed);
            currentSpeed = minSpeed + speedIncrease; // 지수 함수로 점진적 증가
        }

        transform.position += Vector3.right * currentSpeed * Time.deltaTime; // 이동
        _countText.transform.position = Camera.main.WorldToScreenPoint(
            new Vector3(transform.position.x + dropCountOffsetX, transform.position.y + dropCountOffsetY,0));
        _countText.GetComponent<TextMeshProUGUI>().text = waterAmount.ToString();
        
    }

    public void WaterReceived()
    {
        waterAmount++;

        // 물을 너무 많이 줘도 시듦
        if (waterAmount > checkScore.maxRange)
        {
            OverWater();
            return;
        }

        // 일정량의 물을 받으면 성장
        if (waterAmount >= checkScore.minRange) 
        {
            Grow();
        }
    }

    private void Grow()
    {
        Debug.Log("화분이 성장했습니다!");
        // 성장에 따른 이미지, 애니메이션 변경 여기서
    }

    private void OverWater()
    {
        Debug.Log("물을 너무 많이 줘버렸다해!");
        // 과수분에 따른 이미지 등 처리
    }

    public void Stop()
    {
        isStop = true;
        // Debug.Log("Stop");
        _rb.velocity = Vector2.zero;
        Destroy(_countText, 2f);
    }
}