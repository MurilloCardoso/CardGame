using DeckHandCard;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot: MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    public Transform originalSlot; // Armazena o slot original
    public Transform destinationSlot; // Armazena o slot de destino
    public GameObject EventGame; // O objeto onde o item pode ser inserido como filho
    private RectTransform rectTransform;
    public ItemSlot originalItemSlot; // Armazena a referência do slot original do ItemSlot
                        public ControllerMatch ControllerMatch;              // Evento para notificar quando um filho é adicionado

    public GameObject PainelInfoAlert;
    public delegate void ChildReceivedEvent(ItemSlot itemSlot, GameObject child);
    public event ChildReceivedEvent OnChildReceived;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        originalItemSlot = transform.parent.GetComponent<ItemSlot>(); // Obtém a referência do ItemSlot
        originalSlot = transform.parent; // Armazena o slot original
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Arrasta o objeto enquanto o mouse se move
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ItemSlot destinationItemSlot = null;

        if (eventData.pointerEnter != null)
        {
            destinationItemSlot = eventData.pointerEnter.GetComponent<ItemSlot>();
        }

        if (destinationItemSlot != null)
        {
            // Move o item para o novo slot
            transform.SetParent(destinationItemSlot.transform);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            // Se não soltar em nenhum slot válido, volta para o original
            transform.SetParent(originalSlot);
            rectTransform.anchoredPosition = Vector2.zero; 
            // Dispara o evento notificando que o slot recebeu um filho
            destinationItemSlot.OnChildReceived?.Invoke(destinationItemSlot, gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
 
        if (gameObject.transform.childCount == 0)
        {

            // Se o GameObject atual não tem filhos, verifica se o EventGame tem algum filho
            if (EventGame.transform.childCount > 0 && ControllerMatch.Player.Energy >= EventGame.transform.GetChild(0).GetComponent<CardManager>().card.Cost)
            {
                // Pega o primeiro filho do EventGame e move para o GameObject atual
                Transform eventChild = EventGame.transform.GetChild(0);
                eventChild.GetComponent<CanvasGroup>().alpha = 1f;
                eventChild.SetParent(gameObject.transform);
                eventChild.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                // Dispara o evento quando um filho é recebido
                OnChildReceived?.Invoke(this, eventChild.gameObject);
            }
            else
            {
                // Exibe a mensagem de alerta e ativa o painel
                PainelInfoAlert.GetComponentInChildren<TextMeshProUGUI>().SetText("Voce nao tem Energia suficiente");
                PainelInfoAlert.SetActive(true);

                // Inicia a corrotina para esconder o painel após 3 segundos
                StartCoroutine(HideAlertAfterDelay(3f));
            }
        }
    }
    // Corrotina para esperar o tempo especificado e desativar o painel
    private IEnumerator HideAlertAfterDelay(float delay)
    {
        // Aguarda o tempo definido (em segundos)
        yield return new WaitForSeconds(delay);

        // Desativa o painel de alerta
        PainelInfoAlert.SetActive(false);
    }

}
