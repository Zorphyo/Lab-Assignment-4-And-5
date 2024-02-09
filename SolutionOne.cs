using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SolutionOne : MonoBehaviour
{
    //public variables for user to change info about their character
    public string charName;
    public int level;
    public int charClass;
    public int conModifier;
    public bool hillDwarf;
    public bool tough;
    public bool rolled;

    //dictionary to hold the hit die values for each class
    Dictionary<int, int> hitDie = new Dictionary<int, int>();

    //to generate random numbers for the random rolls
    Random rand = new Random();

    // Start is called before the first frame update
    void Start()
    {
        //ask user to enter their class and add all of the hit die values to the dictionary
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

        hitDie.Add(1, 8);
        hitDie.Add(2, 12);
        hitDie.Add(3, 8);
        hitDie.Add(4, 8);
        hitDie.Add(5, 8);
        hitDie.Add(6, 10);
        hitDie.Add(7, 8);
        hitDie.Add(8, 10);
        hitDie.Add(9, 8);
        hitDie.Add(10, 10);
        hitDie.Add(11, 6);
        hitDie.Add(12, 8);
        hitDie.Add(13, 6);

        //calculate health function
        calculateHealth(this.charClass, this.level, this.conModifier, this.hillDwarf, this.tough);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function calculates the health of the character based on user input. It can find the average health based on the stats and hit die, or do random rolls to determine health
    void calculateHealth(int classNumber, int level, int conModifier, bool hillDwarf, bool tough)
    {
        //checking to see if the number the user entered is valid
        if (classNumber > 13 || classNumber < 1){

            Debug.Log("Invalid Class Number.");

            return;

        }

        int HP;

        //initial level 1 starting health
        HP = hitDie[classNumber] + conModifier;

        //add health if the character is a hill dwarf
        if (hillDwarf)
        {
            HP = HP + level;
        }

        //add health if the character has the tough feat
        if (tough)
        {
            HP = HP + (2 * level);
        }

        //do random dice rolls for the rest of the character's levels past one. Add con modifier to each roll if necessary
        if (this.rolled)
        {
            int randomNum;

            for(int i = 0; i < this.level - 1; i++)
            {
                randomNum = rand.Next(1, hitDie[classNumber] + 1);
                randomNum = randomNum + conModifier;

                HP = HP + randomNum;

            }
        }

        //calculate what the average dice roll would be for the specific hit die and multiply that by remaining levels past one
        else
        {
            double averageNum = Math.Ceiling(( (double) hitDie[classNumber] + 1) / 2);
            averageNum = averageNum * (level - 1);

            HP = HP + (int) averageNum;
        }

        Debug.Log("Your character's health is " + HP + ".");
    }
}
