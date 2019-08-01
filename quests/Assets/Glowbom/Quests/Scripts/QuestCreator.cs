﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Created on Sun Jul 30 2019
 *
 * Copyright (c) 2019 Glowbom.
 */
public class QuestCreator : MonoBehaviour
{
    public GridStatusScript game;
    private const int MAIN_ELEMENTS_COUNT = 4;
    private const int ITEM_BUTTONS_COUNT = 3;

    public Text projectName;
    // All
    public Button allPreviousButton;
    public Button allNextButton;
    public Button[] allTitleButtons;
    public Button[] allInsertButtons;
    public Button[] allRemoveButtons;
    public GameObject allValues;
    public InputField allValuesName;
    public InputField allValuesValue;
    public Button allValuesPreviousButton;
    public Button allValuesNextButton;

    // Item
    public InputField title;
    public InputField description;
    public GameObject values;
    public InputField valuesName;
    public InputField valuesValue;
    public Button valuesPreviousButton;
    public Button valuesNextButton;
    // Go To Buttons
    public Button gotoPreviousButton;
    public Button gotoNextButton;
    public Text[] buttonTexts;
    public InputField[] buttonTitles;
    public InputField[] buttonValues;
    public Button[] buttonGoButtons;
    public Button[] buttonInsertButtons;
    public Button[] buttonRemoveButtons;
    public GameObject[] buttonContainers;

    QuestLoader questLoader = new QuestLoader();

    int mainItemsPosition = 0;
    int itemButtonsPosition = 0;
    int currentAllValuePosition = 0;
    int currentValuePosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        questLoader.load();

        initMainQuest();
    }

    private void initMainQuest()
    {
        if (questLoader.logic != null)
        {
            for (int i = 0 + mainItemsPosition; i < mainItemsPosition + MAIN_ELEMENTS_COUNT; i++)
            {
                bool hasItem = i - mainItemsPosition < allTitleButtons.Length;

                allTitleButtons[i - mainItemsPosition].gameObject.SetActive(hasItem);
                allInsertButtons[i - mainItemsPosition].gameObject.SetActive(hasItem);
                allRemoveButtons[i - mainItemsPosition].gameObject.SetActive(hasItem);

                if (hasItem)
                {
                    allTitleButtons[i - mainItemsPosition].GetComponentInChildren<Text>().text = i == questLoader.logic.currentItemIndex ?
                    "* " + i + " : " + questLoader.logic.items[i].title :
                     i + " : " + questLoader.logic.items[i].title;
                }
            }

            allPreviousButton.gameObject.SetActive(mainItemsPosition != 0);
            allNextButton.gameObject.SetActive(mainItemsPosition != questLoader.logic.items.Length - MAIN_ELEMENTS_COUNT);
            initCurrentItem();

            allValuesPreviousButton.gameObject.SetActive(currentAllValuePosition > 0);
            allValuesNextButton.gameObject.SetActive(currentAllValuePosition < questLoader.logic.heroValues.Length - 1);
            if (questLoader.logic.heroValues.Length > 0)
            {
                allValuesName.text = questLoader.logic.heroElements[currentAllValuePosition];
                allValuesValue.text = questLoader.logic.heroValues[currentAllValuePosition].ToString();
            }

        }
    }

    private void initCurrentItem()
    {
        Logic.Item item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        title.text = item.title;
        description.text = item.description;

        int buttonsCount = currentItemButtonsCount();

        for (int i = 0 + itemButtonsPosition; i < itemButtonsPosition + ITEM_BUTTONS_COUNT; i++)
        {
            bool hasItem = i - itemButtonsPosition < buttonsCount;

            buttonContainers[i - itemButtonsPosition].gameObject.SetActive(hasItem);

            if (hasItem)
            {
                buttonTexts[i - itemButtonsPosition].text = "Button " + (i + 1);
                buttonTitles[i - itemButtonsPosition].text = item.buttonsTexts[i];
                buttonValues[i - itemButtonsPosition].text = item.goIndexes[i].ToString();
            }
        }

        gotoPreviousButton.gameObject.SetActive(itemButtonsPosition != 0);
        gotoNextButton.gameObject.SetActive(itemButtonsPosition < buttonsCount - ITEM_BUTTONS_COUNT);

        if (item.heroValues == null || item.heroValues.Length != questLoader.logic.heroValues.Length)
        {
            item.heroValues = new int[questLoader.logic.heroValues.Length];
        }

        valuesPreviousButton.gameObject.SetActive(currentValuePosition > 0);
        valuesNextButton.gameObject.SetActive(currentValuePosition < item.heroValues.Length - 1);
        if (item.heroValues.Length > 0)
        {
            valuesName.text = questLoader.logic.heroElements[currentValuePosition];
            valuesValue.text = item.heroValues[currentValuePosition].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void save() {
        updateQuest();
        questLoader.save();
        initMainQuest();
    }

    public void load() {
        questLoader.load();
        initMainQuest();
    }

    public void allNextPressed() {
        if (mainItemsPosition < questLoader.logic.items.Length - MAIN_ELEMENTS_COUNT)
        {
            ++mainItemsPosition;
            initMainQuest();
        }
    }

    public void allPreviousPressed() {
        if (mainItemsPosition > 0)
        {
            --mainItemsPosition;
            initMainQuest();
        }
    }

    public void allValuesNextPressed() {
        if (currentAllValuePosition < questLoader.logic.heroValues.Length - 1)
        {
            updateQuest();
            ++currentAllValuePosition;
            initMainQuest();
        }
    }

    public void allValuesPreviousPressed() {
        if (currentAllValuePosition > 0)
        {
            updateQuest();
            --currentAllValuePosition;
            initMainQuest();
        }
    }

    public void valuesNextPressed() {
        Logic.Item item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        if (currentValuePosition < item.heroValues.Length - 1)
        {
            updateQuest();
            ++currentValuePosition;
            initCurrentItem();
        }
    }

    public void valuesPreviousPressed() {
        if (currentValuePosition > 0)
        {
            updateQuest();
            --currentValuePosition;
            initCurrentItem();
        }
    }

    private int currentItemButtonsCount()
    {
        Logic.Item item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        int buttonsCount = 0;
        foreach (var goIndex in item.goIndexes)
        {
            if (goIndex == -1)
            {
                break;
            }
            ++buttonsCount;
        }
        return buttonsCount;
    }

    public void buttonsNextPressed() {
        Logic.Item item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        int buttonsCount = currentItemButtonsCount();

        if (itemButtonsPosition <= buttonsCount - MAIN_ELEMENTS_COUNT)
        {
            updateQuest();
            ++itemButtonsPosition;
            initCurrentItem();
        }
    }

    public void buttonsPreviousPressed() {
        if (itemButtonsPosition > 0)
        {
            updateQuest();
            --itemButtonsPosition;
            initCurrentItem();
        }
    }

    // All

    public void allTitlePressed(int i)
    {
        updateQuest();

        questLoader.logic.currentItemIndex = mainItemsPosition + i;
        itemButtonsPosition = 0;
        initMainQuest();
    }

    private void updateQuest()
    {
        Logic.Item item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        item.title = title.text;
        item.description = description.text;

        int buttonsCount = currentItemButtonsCount();
        for (int i = 0 + itemButtonsPosition; i < itemButtonsPosition + ITEM_BUTTONS_COUNT; i++)
        {
            bool hasItem = i - itemButtonsPosition < buttonsCount;

            if (hasItem)
            {
                item.buttonsTexts[i] = buttonTitles[i - itemButtonsPosition].text;
                try
                {
                    item.goIndexes[i] = int.Parse(buttonValues[i - itemButtonsPosition].text);
                    if (item.goIndexes[i] >= questLoader.logic.items.Length)
                    {
                        item.goIndexes[i] = 0;
                    }
                } 
                catch(Exception e)
                {
                    item.goIndexes[i] = 0;
                }

            }
        }

        if (questLoader.logic.heroValues.Length > 0)
        {
            questLoader.logic.heroElements[currentAllValuePosition] = allValuesName.text;
            try
            {
                questLoader.logic.heroValues[currentAllValuePosition] = int.Parse(allValuesValue.text);
            }
            catch (Exception e)
            {
                questLoader.logic.heroValues[currentAllValuePosition] = 0;
            }
        }

        if (item.heroValues == null)
        {
            item.heroValues = new int[questLoader.logic.heroValues.Length];
        }

        if (item.heroValues.Length > 0)
        {
            try
            {
                item.heroValues[currentValuePosition] = int.Parse(valuesValue.text);
            }
            catch (Exception e)
            {
                item.heroValues[currentValuePosition] = 0;
            }
        }
    }

    private void insert(int i)
    {
        Logic.Item item = new Logic.Item();
        item.title = "New Title";
        item.description = "Hello!";
        item.buttonsTexts = new string[1];
        item.buttonsTexts[0] = "Go Button";
        item.goIndexes = new int[1];
        item.goIndexes[0] = 0;

        List<Logic.Item> items = new List<Logic.Item>(questLoader.logic.items);
        items.Insert(i + 1, item);

        questLoader.logic.items = items.ToArray();

        foreach (var logicItem in questLoader.logic.items)
        {
            for (int j = 0; j < logicItem.goIndexes.Length; j++)
            {
                if (logicItem.goIndexes[j] >= i + 1)
                {
                    logicItem.goIndexes[j] = logicItem.goIndexes[j] + 1;
                }
            }
        }

        initMainQuest();
    }

    public void allInsertPressed(int i) {
        insert(mainItemsPosition + i);
    }

    private void remove(int i)
    {
        List<Logic.Item> items = new List<Logic.Item>(questLoader.logic.items);
        items.RemoveAt(i);

        questLoader.logic.items = items.ToArray();

        foreach (var logicItem in questLoader.logic.items)
        {
            for (int j = 0; j < logicItem.goIndexes.Length; j++)
            {
                if (logicItem.goIndexes[j] >= i)
                {
                    logicItem.goIndexes[j] = logicItem.goIndexes[j] - 1;
                    if (logicItem.goIndexes[j] < 0)
                    {
                        logicItem.goIndexes[j] = 0;
                    }
                }
            }
        }

        initMainQuest();
    }

    public void allRemovePressed(int i) {
        remove(mainItemsPosition + i);
    }

    private void insertInitialHeroValue(int i)
    {
        List<string> heroElementsList = new List<string>(questLoader.logic.heroElements);
        List<int> heroValuesList = new List<int>(questLoader.logic.heroValues);

        heroValuesList.Insert(i + 1, 0);
        heroElementsList.Insert(i + 1, "Points " + i);

        questLoader.logic.heroElements = heroElementsList.ToArray();
        questLoader.logic.heroValues = heroValuesList.ToArray();

        ++currentAllValuePosition;
        initMainQuest();
    }

    private void removeInitialHeroValue(int i)
    {
        List<string> heroElementsList = new List<string>(questLoader.logic.heroElements);
        List<int> heroValuesList = new List<int>(questLoader.logic.heroValues);

        heroElementsList.RemoveAt(i);
        heroValuesList.RemoveAt(i);

        questLoader.logic.heroElements = heroElementsList.ToArray();
        questLoader.logic.heroValues = heroValuesList.ToArray();

        initMainQuest();
    }

    public void allValueInsertPressed() {
        insertInitialHeroValue(currentAllValuePosition);
    }

    public void allValueRemovePressed() {
        removeInitialHeroValue(currentAllValuePosition);
    }

    // Buttons
    private void insertButton(int i)
    {
        var item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        List<string> buttonsTextsList = new List<string>(item.buttonsTexts);
        List<int> goIndexesList = new List<int>(item.goIndexes);
        List<int> goConditionsList = item.goConditions == null ? new List<int>() : new List<int>(item.goConditions);

        if (goConditionsList.Count > 0)
        {
            goConditionsList.Insert(i + 1, 0);
        }

        goIndexesList.Insert(i + 1, 0);
        buttonsTextsList.Insert(i + 1, "Go Button");

        item.buttonsTexts = buttonsTextsList.ToArray();
        item.goIndexes = goIndexesList.ToArray();

        if (goConditionsList.Count > 0)
        {
            item.goConditions = goConditionsList.ToArray();
        }

        initCurrentItem();
    }

    public void buttonInsertPressed(int i) {
        insertButton(i);
    }

    public void buttonGoPressed(int i) {
        updateQuest();
        Logic.Item item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        int index = item.goIndexes[itemButtonsPosition + i];

        questLoader.logic.currentItemIndex = index;
        itemButtonsPosition = 0;
        mainItemsPosition = index;
        initMainQuest();
    }

    private void removeButton(int i)
    {
        var item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        List<string> buttonsTextsList = new List<string>(item.buttonsTexts);
        List<int> goIndexesList = new List<int>(item.goIndexes);
        List<int> goConditionsList = item.goConditions != null ? new List<int>(item.goConditions) : new List<int>();

        if (item.goConditions != null && goConditionsList.Count > 0)
        {
            goConditionsList.RemoveAt(i);
        }

        goIndexesList.RemoveAt(i);
        buttonsTextsList.RemoveAt(i);

        item.buttonsTexts = buttonsTextsList.ToArray();
        item.goIndexes = goIndexesList.ToArray();

        if (item.goConditions != null && goConditionsList.Count > 0)
        {
            item.goConditions = goConditionsList.ToArray();
        }

        initCurrentItem();
    }

    public void buttonRemovePressed(int i) {
        removeButton(i);
    }

    private void insertHeroValue(int i)
    {
        var item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        List<string> heroElementsList = new List<string>(questLoader.logic.heroElements);
        List<int> heroValuesList = new List<int>(questLoader.logic.heroValues);
        List<int> heroValuesItemList = new List<int>(item.heroValues);

        heroValuesList.Insert(i + 1, 0);
        heroValuesItemList.Insert(i + 1, 0);
        heroElementsList.Insert(i + 1, "Points " + i);

        questLoader.logic.heroElements = heroElementsList.ToArray();
        questLoader.logic.heroValues = heroValuesList.ToArray();
        item.heroValues = heroValuesItemList.ToArray();

        ++currentValuePosition;
        initMainQuest();
    }

    private void removeHeroValue(int i)
    {
        var item = questLoader.logic.items[questLoader.logic.currentItemIndex];
        List<string> heroElementsList = new List<string>(questLoader.logic.heroElements);
        List<int> heroValuesList = new List<int>(questLoader.logic.heroValues);
        List<int> heroValuesItemList = new List<int>(item.heroValues);

        heroElementsList.RemoveAt(i);
        heroValuesList.RemoveAt(i);
        heroValuesItemList.RemoveAt(i);

        questLoader.logic.heroElements = heroElementsList.ToArray();
        questLoader.logic.heroValues = heroValuesList.ToArray();
        item.heroValues = heroValuesItemList.ToArray();

        initMainQuest();
    }

    public void valueInsertPressed() {
        insertHeroValue(currentValuePosition);
    }

    public void valueRemovePressed() {
        removeHeroValue(currentValuePosition);
    }

    public void backPressed() {
        updateQuest();
        save();
        game.logic = questLoader.logic;
        game.procced();
        gameObject.SetActive(false);
    }
}