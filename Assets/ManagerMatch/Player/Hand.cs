using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
namespace DeckHandCard
{
    public class Hand : MonoBehaviour
    {
        public GameObject itemPrefab;
      
        public List<Card> cards = new List<Card>();
        
        public List<Card> GenerateHand()
        {
            List<Card> DeckPlayer = generationDeck();
          
            int index = 0;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform slot = this.gameObject.transform.GetChild(i);
           
                //// Verifica se o item tem um componente Image e se o índice é válido
                if (slot.childCount == 0 && index < DeckPlayer.Count)
                {
                    // Instancia o itemPrefab dentro do slot
                    GameObject spawnedCard = Instantiate(itemPrefab, slot.position, Quaternion.identity, slot);
                    if (spawnedCard.GetComponentInChildren<CardManager>().card == null)
                    {
                        spawnedCard.GetComponentInChildren<CardManager>().card = DeckPlayer[index];
                    }
                    // Acessa o componente de imagem do itemPrefab para setar o sprite da carta
                    Image cardImage = spawnedCard.GetComponentInChildren<Image>();
                    if (cardImage != null)
                    {
                        cardImage.sprite = DeckPlayer[index].Sprite;
                    }
                    // Define os textos de ataque, vida e custo
                    TextMeshProUGUI textAtk = spawnedCard.transform.Find("Text Atk")?.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI textHealth = spawnedCard.transform.Find("Text Health")?.GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI textCost = spawnedCard.transform.Find("Text Cost")?.GetComponent<TextMeshProUGUI>();

                    if (textAtk != null) textAtk.text = DeckPlayer[index].Attack.ToString();
                    if (textHealth != null) textHealth.text = DeckPlayer[index].Health.ToString();
                    if (textCost != null) textCost.text = DeckPlayer[index].Cost.ToString();

                    index++; // Incrementa o índice para pegar a próxima carta do baralho
                }
            }
            return DeckPlayer;
        }
        public List<Card> generationDeck()
        {
            List<Card> result = new List<Card>();
            while (result.Count < cards.Count) // Continue até que o baralho tenha 4 cartas
            {
                Card card = cards[Random.Range(0, cards.Count)];
                if (!result.Contains(card))
                {
                    result.Add(card); // Adiciona a carta somente se ela não está na lista
                }
            }
            return result;
        }
    }
   
}