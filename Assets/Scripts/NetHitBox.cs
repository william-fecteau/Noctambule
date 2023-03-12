using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetHitBox : MonoBehaviour
{
    [SerializeField] private float netCooldownSec = 1.0f;
    [SerializeField] private float netCatchPeriodSec = 0.2f;
    [SerializeField] private LayerMask mothlayerMasks;
    [SerializeField] private float volumeSFX = 1;
    [SerializeField] private GameObject playerNet;

    //0 is catch
    //1-4 is swing
    [SerializeField] private AudioClip[] clips;

    private AudioClip clip;
    private AudioSource catchSound;
    private AudioSource swingSound;
    private float netCooldownTimer = 0.0f;
    private float netCatchPeriodTimer = 0.0f;
    private bool isNetActivated = false;
    private bool isNetInCooldown = false;
    private PlayerController playerController;

    private const int MAX_MOTH_CATCH = 69;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        catchSound = new AudioSource();
        swingSound = new AudioSource();

        swingSound = gameObject.AddComponent<AudioSource>();
        catchSound = gameObject.AddComponent<AudioSource>();
    }

    private void Update() {
        if (isNetActivated)
        {
            CatchMothsInReach();

            netCatchPeriodTimer += Time.deltaTime;
            if (netCatchPeriodTimer > netCatchPeriodSec)
            {
                isNetActivated = false;
                playerNet.SetActive(false);
                isNetInCooldown = true;
                netCooldownTimer = 0.0f;
            }
        }
        else if (isNetInCooldown)
        {
            netCooldownTimer += Time.deltaTime;
            if (netCooldownTimer > netCooldownSec)
            {
                isNetInCooldown = false;
                isNetActivated = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //Sorry need audio
            
            swingSound.clip = clips[Random.Range(1, clips.Length - 1)];
            swingSound.loop = false;
            swingSound.volume = volumeSFX;
            swingSound.Play();

            isNetActivated = true;
            playerNet.SetActive(true);
            netCatchPeriodTimer = 0.0f;
        }

        

    }

    private void CatchMothsInReach()
    {

        Vector2 size = new Vector2(2, 2);
        float distance = 2.0f;
        float angle = 0.0f;
        Vector2 direction = playerController.GetNormalizedDirection();
        RaycastHit2D[] results = new RaycastHit2D[MAX_MOTH_CATCH];
        if (Physics2D.BoxCastNonAlloc(transform.position, size, angle, direction, results, distance, mothlayerMasks) > 0)
        {
            if (results == null || results.Length == 0) return;

            foreach (RaycastHit2D raycastHit in results) {
                if (raycastHit.transform == null) continue;

                if (raycastHit.transform.TryGetComponent<BaseMoth>(out BaseMoth baseMoth))
                {
                    if (baseMoth.itemNeededToCatch != string.Empty && !playerController.items.Contains(baseMoth.itemNeededToCatch)) 
                        continue;

                    GetComponent<PlayerWallet>().AddMoney(baseMoth.money);

                    catchSound.clip = clips[0];
                    catchSound.loop = false;
                    catchSound.volume = volumeSFX;
                    catchSound.Play();
                    Destroy(raycastHit.collider.gameObject);
                }
            }
        }
    }
}
