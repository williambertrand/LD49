using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInteraction : MonoBehaviour
{

    public static PlayerInteraction Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] TMP_Text interactionText;
    [SerializeField] GameObject interactionHolder;

    public bool isInDialouge;

    private Interactable currentFocus;

    // Start is called before the first frame update
    void Start() {
        interactionHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("TRIGGER ENTER: " + Time.time);
            Interactable inter = collision.gameObject.GetComponent<Interactable>();
            if (inter == null)
            {
                Debug.LogError("Interactable item does not have interactable component");
                return;
            }
            interactionHolder.SetActive(true);
            interactionText.text = inter.interactionString;
            interactionText.gameObject.SetActive(true);
            currentFocus = inter;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            interactionText.text = "";
            interactionHolder.gameObject.SetActive(false);
            currentFocus = null;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            /* The key/button was pressed */
            case InputActionPhase.Started:
                if(isInDialouge)
                {
                    DialogueManager.Instance.DisplayNextSentence();
                }
                else
                {
                    if (currentFocus == null) return;
                    currentFocus.OnInteract();
                    interactionHolder.SetActive(false);
                }
                break;
        }
    }

    public void Dismiss()
    {
        interactionText.text = "";
        interactionHolder.gameObject.SetActive(false);
        currentFocus = null;
    }


    // Post - Jam Polish potentially?
    private void OnControlSchemeChanged()
    {

    }
}