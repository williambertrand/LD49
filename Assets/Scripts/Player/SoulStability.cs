using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulStability : MonoBehaviour
{
    [SerializeField] private float max;
    private float current;
    [SerializeField] private float degenRate;
    [SerializeField] private float threshold;

    [SerializeField] private Slider stabilityBar;

    public bool isUnstable;
    [SerializeField] private CameraShake virtualCamera;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        current = max;
        stabilityBar.maxValue = max;
        stabilityBar.value = max;
        isUnstable = false;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(current < threshold)
        {
            isUnstable = true;
            virtualCamera.SetShake(true);

        } else
        {
            virtualCamera.SetShake(false);
            isUnstable = false;
        }
        UpdateUI();

        if (!isActive) return;
        current -= degenRate * Time.deltaTime;
    }

    public void Increase(float amount)
    {
        current += amount;
        if (current > max)
        {
            current = max;
        }
    }

    void UpdateUI()
    {
        stabilityBar.value = current;
    }

    public void TakeDamage(float damage)
    {
        current -= damage;
        DropSouls((int)damage);
        UpdateUI();
    }

    void DropSouls(int damage)
    {
        for (int i = 0; i < damage; i++)
        {
            GameObject drop = Instantiate(GamePrefabs.Instance.PlayerSoulDrop);
            drop.transform.position = transform.position;
            drop.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

            Rigidbody2D dropRB = drop.GetComponent<Rigidbody2D>();
            CircleCollider2D collider = drop.GetComponent<CircleCollider2D>();
            collider.enabled = false;
            int neg = Random.Range(0.0f, 1.0f) < 0.5f ? -1 : 1;
            Vector2 randPopVel = new Vector2(
                Random.Range(1.5f, 3.5f) * neg,
                Random.Range(1.5f, 3.5f) * neg
            );
            dropRB.velocity = randPopVel;
        }
    }

    public void Pause()
    {
        isActive = false;
    }

    public void Resume()
    {
        isActive = true;
    }

    public void Begin()
    {
        isActive = true;
    }
}
