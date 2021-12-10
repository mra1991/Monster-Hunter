//Revision history:
//Mohammadreza Abolhassani      2021-12-06      Created the Hunter object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter
{
    public class Hunter : Character
    {
        private string gsValidationError = "";

        private string gsName = "Player1"; //global string for name of the hunter
        const int MAX_NAME_LENGTH = 20; //maximum acceptable length of gsName
        public string Name //public get and set with validation for gsName
        {
            get
            {
                try
                {
                    return gsName;
                }
                catch (Exception e)
                {
                    throw new Exception("An error occured", e);
                }
            }
            set
            {
                try
                {
                    gsValidationError = ""; //clear the error message
                    //remove blank spaces
                    value = value.Trim();
                    if (value is null || value == "")
                    {
                        gsValidationError = "The name cannot be empty.";
                    }
                    else
                    {
                        if (value.Length > MAX_NAME_LENGTH)
                        {
                            gsValidationError = "The name cannot contain more than " + MAX_NAME_LENGTH + " characters.";
                        }
                        else //the input is valid
                        {
                            gsName = value;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("An error occured", e);
                }
            }


        }

        protected int giScore = 0; //global integer for score of the player
        const int MAX_SCORE = 100000; //max amount for giScore is one hundred thousand
        public int Score  //property with public get and set with validation for giScore
        {
            get
            {
                try
                {
                    return giScore;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            set
            {
                try
                {
                    //clear the error message
                    gsValidationError = "";

                    if (value < 0)
                    {
                        gsValidationError = "The value for score cannot be negative.";
                    }                   
                    else if (value > MAX_SCORE)
                    {
                        gsValidationError = "The value for score cannot be more than " + MAX_SCORE.ToString() + " .";
                    }
                    else
                    {
                        giScore = value;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        }

        private IState goState; //to store the current state of the hunter
        private NormalState goLastNormalSate; //keep a normalstate object from before hunter drinks a potion, to revert back to later.
        private bool gbIsInvisible = false; //when true, hunter can pass monsters without engaging in a fight.
        public IState State //property will call ChangeAttribute function of the new state automatically once set
        {
            get
            {
                try
                {
                    return goState;
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
            set
            {
                try
                {
                    if(goState is NormalState) //if we are changing from normal state
                    {
                        //save the information in a new NormalState object. pass the variables by value to be stored in the object
                        goLastNormalSate = new NormalState(HP, Attack, Armor, MilliseconsBetweenMoves); 
                    }
                    goState = value;
                    //passing variables by reference for the ChangeAttribute function to be able to modify them
                    goState.ChangeAttributes(MAX_HP, ref this.giHP, ref this.giAttack, ref this.giArmor, ref this.giMilliseconsBetweenMoves, ref gbIsInvisible);
                }
                catch (Exception e)
                {
                    throw new Exception("<ERROR> ", e);
                }
            }
        }
        private IGadget goGadget = null; //a gadget can be a sword, shield or pickaxe

        private bool gbGoalFound = false; //global boolean for winning the game
        public bool GoalFound { get => gbGoalFound; } //read only access to gbGoalFound

        public string ValidationError { get => gsValidationError; } //read only access to error message
        public Hunter(int piPosY, int piPosX, int piHeight = 0, int piWidth = 0, Map poMap = null) : base(piPosY, piPosX, piHeight, piWidth, poMap)
        {
            //set the delay between moves to one second
            this.giMilliseconsBetweenMoves = 1000;

            //set the global object for state to normal state
            goState = new NormalState(HP, Attack, Armor, MilliseconsBetweenMoves);
        }

        private String AttackMonster(Monster poMonster)
        {
            string sLog = this.Name + " attacks a monster ";
            //battle
            //damage inflicted is player's attack minus monster's armor (negative values won't be applyed)
            poMonster.TakeDamage(this.Attack - poMonster.Armor);
            if (poMonster.IsDead)
            {
                sLog += "and kills it.";
                return sLog;
            }
            if(goGadget is Sword)
            {
                poMonster.TakeDamage((goGadget as Sword).OffenceStrength);
            }
            if (goGadget.Break()) //see if the sword breaks 
            {
                goGadget = null; //throw it out
            }
            if (poMonster.IsDead)
            {
                sLog += "with a sword and kills it.";
                return sLog;
            }
            return sLog;
        }

        //moves the hunter to the new position if possible
        public override bool Move(int piX, int piY)
        {
            // if new coordinates are out of bounds, the move won't be possible
            if (piX < 0 || piY < 0 || piX >  - 1 || piY > giHeight - 1)
            {
                return false;
            }

            //go through the list of monsters and see if there is any at this position
            List<Monster> monstersList = GoMap.GoMonsters.FindMonsters(piX, piY);
            foreach(Monster oM in monstersList)
            {
                string sFightLog = AttackMonster(oM);
                //save the battle log in a file
                //...
            }

            //if there's no monster there, there can be:
            switch (GoMap.gcMap[piX, piY])
            {
                case '#': //Wall
                    //check if the hunter has a pickaxe, he can break the wall
                    if(goGadget is Pickaxe)
                    {
                        GoMap.gcMap[piX, piY] = ' '; //break the wall
                        this.giPosX = piX; //update hunter's position along x axis
                        this.giPosY = piY; //update hunter's position along y axis
                        if (goGadget.Break()) //check if the pickaxe breaks
                        {
                            goGadget = null; //throw it away!
                        }
                        return true; //successfully moved
                    }
                    //the hunter can't pass a wall
                    return false;
                case 'G': //Goal
                    gbGoalFound = true;
                    break;
                case 'w': //Sword
                    goGadget = new Sword();
                    break;
                case 'h': //Shield
                    goGadget = new Shield();
                    break;
                case 'x': //Pickaxe
                    goGadget = new Pickaxe();
                    break;
                case 'p': //Potion
                    break;
                case ' ': //Empty
                    break;
                default:
                    break;
            }
            this.giPosX = piX; //update hunter's position along x axis
            this.giPosY = piY; //update hunter's position along y axis
            return true; //successfully moved
        }

        //gets a potion and applys its effect on hunter
        private void DrinkPotion(Potion poPotion)
        {
            switch (poPotion.Type)
            {
                case POTION_TYPE.INVISIBILITY_POTION:
                    this.State = new InvisibleState();
                    break;
                case POTION_TYPE.POISON_POTION:
                    this.State = new PoisonedState();
                    break;
                case POTION_TYPE.SPEED_POTION:
                    this.State = new FastState();
                    break;
                case POTION_TYPE.STRENGTH_POTION:
                    this.State = new StrongState();
                    break;

                default:
                    break;
            }
            //after a time (10 seconds) the effects of the potion face and hunter will go back to normal state
            Task.Delay(Potion.POTION_EFFECT_MILLISEC).ContinueWith(t => BackToNormal());
        }

        //should be called when the effects of a potion fades to return the state to normal
        private void BackToNormal()
        {
            this.State = goLastNormalSate;
        }
    }
}
