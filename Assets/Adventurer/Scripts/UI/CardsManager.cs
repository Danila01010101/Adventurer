using System;
using System.Collections.Generic;
using ModestTree.Util;
using RedstoneinventeGameStudio;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [HideInInspector]public List<CardManager> emptyCards = new List<CardManager>();
    [HideInInspector]public List<CardManager> fullCards = new List<CardManager>();
    [HideInInspector] public List<InventoryItemData> items = new List<InventoryItemData>();
    private CardManager[] cards;
        
    private void Awake()
    {
        cards = GetComponentsInChildren<CardManager>();
        foreach (CardManager card in cards)
        {
                if (card.itemData == null)
                    emptyCards.Add(card);
                else
                    fullCards.Add(card); items.Add(card.itemData);
        }
    }

    private void Start()
    {
        
    }

    public void UpdateInventory()
    {
        emptyCards.Clear();
        fullCards.Clear();
        items.Clear();
        
        for (int i = 0; i < cards.Length; i++)
        {
            CardManager card = cards[i];
            
            if (card.itemData == null)
            {
                emptyCards.Add(card);
                card.UnSetItem();
            }
            else
            {
                fullCards.Add(card);
                items.Add(card.itemData);
                card.SetItem(card.itemData);
            }

        }
    }
}