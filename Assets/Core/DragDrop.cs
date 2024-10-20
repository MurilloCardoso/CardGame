using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour, IPointerDownHandler
{

    public Transform originalSlot; // Armazena o slot original

    public GameObject EventGame; // O objeto onde o item pode ser inserido como filho
    public CanvasGroup groups;

    private void Start()
    {
        // Verifica se o objeto tem filhos
        if (gameObject.transform.childCount != 0)
        {
            // Verifica se o primeiro filho tem um CanvasGroup
            CanvasGroup canvasGroup = gameObject.transform.GetChild(0).GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                groups = canvasGroup;
            }
            else
            {
                Debug.LogWarning("O primeiro filho do objeto não possui um componente CanvasGroup.");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Verifica se o GameObject atual tem um filho
        if (gameObject.transform.childCount > 0 && EventGame.transform.childCount == 0)
        {

            groups.alpha = 0.6f;
            // Obtém o primeiro filho do GameObject atual

            Transform childToMove = gameObject.transform.GetChild(0);

            EventGame.GetComponent<DragDrop>().originalSlot = gameObject.transform;
            childToMove.SetParent(EventGame.transform);

        }
        else
        {
        
            if (gameObject.transform.childCount > 0 &&  gameObject.name != "Event") {
                EventGame.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
                EventGame.transform.GetChild(0).SetParent(EventGame.transform.GetComponent<DragDrop>().originalSlot);
                Transform childToMove = gameObject.transform.GetChild(0);
                childToMove.GetComponent<CanvasGroup>().alpha = 0.6f;
                EventGame.GetComponent<DragDrop>().originalSlot = gameObject.transform;
                childToMove.SetParent(EventGame.transform);

                // eventChild.SetParent(gameObject.transform);
            }
            else if(gameObject.transform.childCount > 0)
            {
                EventGame.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
                EventGame.transform.GetChild(0).SetParent(EventGame.transform.GetComponent<DragDrop>().originalSlot);
        
            }
        }
    }
}
