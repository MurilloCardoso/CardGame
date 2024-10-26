using DeckHandCard;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Hand Hand_AI;
    private PlayerStatusInMatch Opponent;
    public TextMeshProUGUI TextMesh_Status;
    public ControllerMatch ControllerMatch;
    public GameObject Table;
    public List<GameObject> CardList_gameobject = new List<GameObject>();
    public void Start()
    {
        Hand_AI= GetComponentInChildren<Hand>();
        List<Card> cardsDeckEnemy = Hand_AI.GenerateHand();
        Opponent = new PlayerStatusInMatch(100, 6, cardsDeckEnemy);
        ControllerMatch.UpdateUI_status(TextMesh_Status,Opponent);

        foreach (Transform item in Hand_AI.GetComponentsInChildren<Transform>())
        {
            // Verifica se o item possui um GameObject que é filho direto de Hand_AI
            if (item != Hand_AI.transform && item.GetComponent<CardManager>() != null)
            {
                CardList_gameobject.Add(item.gameObject);
            }
        }
    }
    public void Decide_Play()
    {

        // Erro
        // nao esta sendo possivel adicionar um novo card na mesa
        // pois tem card que tem a energia muito elevada
        // Solucao: Criar uma nova while para verificar se a energia do card é menor que a energia do oponente
        // Se nao achar o card nao achar no deck inteiro do oponente, o oponente passa a vez
        // Erro
        GameObject cardPlay = CardList_gameobject[Random.Range(0, CardList_gameobject.Count)];
        CardList_gameobject.Remove(cardPlay);
        Card card = cardPlay.GetComponentInChildren<CardManager>().card;

      
        //if (Opponent.Energy >= card.Cost)
        //{
            // Reduz a energia do player conforme o custo do card
            Opponent.Energy -= card.Cost;
            // Itera sobre os slots da Table procurando um slot vazio
            foreach (Transform slot_table in Table.GetComponentsInChildren<Transform>())
            {
              
                if (slot_table.childCount == 0)  // Verifica se o slot está vazio
                {
                    // Define o slot vazio como pai do card
                    cardPlay.transform.SetParent(slot_table); Debug.Log("awd>  "+slot_table.name);
                    cardPlay.transform.localPosition = Vector3.zero; // Centraliza o card no slot
                    break; // Sai do loop após encontrar o primeiro slot vazio
                }
            }

            // Atualiza o texto na UI com a nova energia do player
            ControllerMatch.UpdateUI_status(TextMesh_Status, Opponent);
     //   }
        //Decide qual carta jogar
        ControllerMatch.NextTurn();
    }
    private void Update()
    {

        if (ControllerMatch.Turn % 2 == 0)
        {
            Decide_Play();
        }
    }
}
