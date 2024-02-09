using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using Random = System.Random;

public class SolutionTwo : MonoBehaviour
{
    //public variables for user to change info about their character
    public string charName;
    public int charLevel;
    public int charClass;
    public int charConModifier;
    public bool charHillDwarf;
    public bool charToughFeat;
    public bool rolled;

    //to generate random numbers for the random rolls
    Random rand = new Random();

    //creating a class for the user's character to hold their stats
    public class DNDCharacter{
        
        private string characterName;
        private int characterLevel;
        private int characterClass;
        private int characterConModifier;
        private bool characterHillDwarf;
        private bool characterToughFeat;

        public string GetCharacterName(){

            return this.characterName;

        }

        public void SetCharacterName(string charName){

            this.characterName = charName;

        }

        public int GetCharacterLevel(){

            return this.characterLevel;

        }

        public void SetCharacterLevel(int charLevel){

            this.characterLevel = charLevel;

        }

        public int GetCharacterClass(){

            return this.characterClass;

        }

        public void SetCharacterClass(int charClass){

            this.characterClass = charClass;

        }

        public int GetCharacterConModifier(){

            return this.characterConModifier;

        }

        public void SetCharacterConModifier(int charConModifier){

            this.characterConModifier = charConModifier;

        }

        public bool GetHillDwarf(){

            return this.characterHillDwarf;

        }

        public void SetHillDwarf(bool charHillDwarf){

            this.characterHillDwarf = charHillDwarf;

        }

        public bool GetToughFeat(){

            return this.characterToughFeat;

        }

        public void SetToughFeat(bool charToughFeat){

            this.characterToughFeat = charToughFeat;

        }

        public DNDCharacter(string characterName, int characterLevel, int characterClass, int characterConModifier, bool hillDwarf, bool toughFeat){

            SetCharacterName(characterName);
            SetCharacterLevel(characterLevel);
            SetCharacterClass(characterClass);
            SetCharacterConModifier(characterConModifier);
            SetHillDwarf(hillDwarf);
            SetToughFeat(toughFeat);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ask user to enter their class
        Debug.Log("Please enter the corresponding number for your class:");
        Debug.Log("1: Artificer");
        Debug.Log("2: Barbarian");
        Debug.Log("3: Bard");
        Debug.Log("4: Cleric");
        Debug.Log("5: Druid");
        Debug.Log("6: Fighter");
        Debug.Log("7: Monk");
        Debug.Log("8: Paladin");
        Debug.Log("9: Ranger");
        Debug.Log("10: Rogue");
        Debug.Log("11: Sorcerer");
        Debug.Log("12: Warlock");
        Debug.Log("13: Wizard");

        //create character
        DNDCharacter character = new DNDCharacter(charName, charLevel, charClass, charConModifier, charHillDwarf, charToughFeat);

        //calculate health function
        calculateHealth(character.GetCharacterClass(), character.GetCharacterLevel(), character.GetCharacterConModifier(), character.GetHillDwarf(), character.GetToughFeat());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function calculates the health of the character based on user input. It can find the average health based on the stats and hit die, or do random rolls to determine health
    void calculateHealth(int charClass, int charLevel, int charConModifier, bool charHillDwarf, bool charToughFeat)
    {

        //checking to see if the number the user entered is valid
        if (charClass > 13 || charClass < 1){

            Debug.Log("Invalid Class Number.");

            return;

        }

        int HP;

        int hitDie;

        //determining our hit die value based on the character's class number
        if(charClass == 1 || charClass == 3 || charClass == 4 || charClass == 5 || charClass == 7 || charClass == 9 || charClass == 12){

            hitDie = 8;

        }

        else if(charClass == 6 || charClass == 8 || charClass == 10){

            hitDie = 10;

        }

        else if(charClass == 2){

            hitDie = 12;

        }

        else{

            hitDie = 6;

        }

        //initial level 1 starting health
        HP = hitDie + charConModifier;

        //add health if the character is a hill dwarf
        if (charHillDwarf)
        {
            HP = HP + charLevel;
        }

        //add health if the character has the tough feat
        if (charToughFeat)
        {
            HP = HP + (2 * charLevel);
        }

        //do random dice rolls for the rest of the character's levels past one. Add con modifier to each roll if necessary
        if (this.rolled)
        {
            int randomNum;

            for(int i = 0; i < charLevel - 1; i++)
            {
                randomNum = rand.Next(1, hitDie + 1);
                randomNum = randomNum + charConModifier;

                HP = HP + randomNum;

            }
        }

        //calculate what the average dice roll would be for the specific hit die and multiply that by remaining levels past one
        else
        {
            double averageNum = Math.Ceiling(( (double) hitDie + 1) / 2);
            averageNum = averageNum * (charLevel - 1);

            HP = HP + (int) averageNum;
        }

        Debug.Log("Your character's health is " + HP + ".");
    }
}
