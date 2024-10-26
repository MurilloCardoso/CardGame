using DeckHandCard;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMatch : MonoBehaviour
{
    #region Variables
    public int Turn = 1;
    //UI
    public TextMeshProUGUI TxtStatusPlayer;
    public TextMeshProUGUI TxtStatusOpponent;
    public TextMeshProUGUI TxtTurn;
    public Hand HandPlayer;
    public Button FinishedTurn;

    //Player Inicializacao
    public PlayerStatusInMatch Player;
    #endregion
    public List<ItemSlot> itemSlots; // Refer�ncia a todos os ItemSlots na cena

    
    #region Config de Inicializacao
    public void Awake()
    {
        //Player Inicializacao 
        List<Card> cardsDeckPlayer = HandPlayer.GetComponent<Hand>().GenerateHand();
        Player = new PlayerStatusInMatch(100, 6, cardsDeckPlayer);
        //Player Enemy
    

        // Inscreve o ControllerMatch para ouvir quando um ItemSlot recebe um filho
        foreach (ItemSlot itemSlot in itemSlots)
        {
            itemSlot.OnChildReceived += OnItemSlotReceivedChild;
        }
        TxtTurn.SetText($"Turno: {Turn}");
    }

    public void UpdateUI_status(TextMeshProUGUI TextUI, PlayerStatusInMatch player)
    {
        TextUI.SetText($"Vida: {player.Life} - Energia: {player.Energy}");
    }

    // M�todo que ser� chamado quando um ItemSlot receber um filho
    private void OnItemSlotReceivedChild(ItemSlot itemSlot, GameObject child)
    {
        Card card=child.GetComponentInChildren<CardManager>().card;
        // Verifica se o player tem energia suficiente para jogar o card
        if (Player.Energy >= card.Cost)
        {
            // Reduz a energia do player conforme o custo do card
            Player.Energy -= card.Cost;

            // Atualiza o texto na UI com a nova energia do player
            TxtStatusPlayer.SetText($"Vida: {Player.Life} - Energia: {Player.Energy}");

            Debug.Log($"ItemSlot {itemSlot.gameObject.name} recebeu o objeto {card.Name}. Energia atual: {Player.Energy}");
        }
        else
        {
            // A��o se o player n�o tiver energia suficiente
            Debug.Log($"Energia insuficiente para jogar o card {card.Name}. Energia necess�ria: {card.Cost}, Energia atual: {Player.Energy}");

            // Aqui voc� pode adicionar uma notifica��o visual ao jogador, por exemplo:
            // Exibir mensagem na tela informando que n�o h� energia suficiente
            TxtStatusPlayer.SetText($"Energia insuficiente para jogar {card.Name}. Energia atual: {Player.Energy}");
        }
    }
    #endregion



    public void NextTurn( )
    {
        Turn++;
        TxtTurn.SetText($"Turno: {Turn}");
        // Itera sobre todos os bot�es nos filhos de HandPlayer e desativa a intera��o
        
        foreach (Button button in HandPlayer.GetComponentsInChildren<Button>())
        {
            button.interactable = !button.interactable;
        }
        FinishedTurn.interactable= !FinishedTurn.interactable;
    }

}
