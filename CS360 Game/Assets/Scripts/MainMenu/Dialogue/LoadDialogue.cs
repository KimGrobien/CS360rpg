﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogue : MonoBehaviour {


	//Cynthia
    public static Node[] Cynthia = new Node[50];
    public static Node[] Emrik = new Node[50];
    public static Node[] Anker = new Node[50];
    public static Node[] Edward = new Node[50];
    public static Node[] Fox = new Node[5];
    public static Node[] RockCreature = new Node[5];
    public static Node[] Rabbit = new Node[5];
    public static Node[] Berndy = new Node[5];
    public static Node[] Modir = new Node[5];
    public static Node[] Farenvir = new Node[5];
    public static Node[] Ozul = new Node[50];

/* 
SPECIAL INDEX LIST
-1 means restart
-2 means exit to overworld
-3 means add partyMember
*/
public static void createDialogueTrees(){
        loadCynthiaDialogue();
        loadAnkerDialogue();
        loadEdwardDialogue();
        loadEmrikDialogue();
        loadBerndyDialogue();
        loadModirDialogue();
        loadFarenvirDialogue();
        loadOzulDialogue();
        loadFoxDialogue();
        loadRockCreatureDialogue();
        loadRabbitDialogue();
    }
public static void loadCynthiaDialogue(){
        //Cynthia

        //currentIndex will lead to (currentIndex*2)+1 as option1 and (currentIndex*2)+2 as option2
        //firstEncounter
        Cynthia[0].response = "She says, \"You\'re new, aren\'t you? I haven\'t seen you around here at least. Is there something you need traveler?\"";
        Cynthia[0].option1 = "I'm hurt can you help me?";
        Cynthia[0].option2 = "Where is the nearest town?";
        Cynthia[0].indexForOption1 = 1;
        Cynthia[0].indexForOption2 = 2;
        //Response to index 0 option1
        Cynthia[1].response = "She looks concerned, but hesitant to help."
        +" You think maybe she's just being defensive, a good trait to have"
        +" in this world, but you are really hurt."
        +"\n\nShe says, \"What happened? Were you traveling alone?\"";
        Cynthia[1].option1 = "I don't remember what happened."
        +" I woke up next to a dismembered horse and destroyed carraige."
        +" I think someone robbed me. If you can\'t help me. "
        +"Do you know someone who can?";
        Cynthia[1].option2 = "If you're not going to help, I'll be on my way.";
        Cynthia[1].indexForOption1 = 3;
        Cynthia[1].indexForOption2 = 4;
        //response to index 0 option2
        Cynthia[2].response = "She says, \"You can find a small village just up the road."
        +"Though they may not be much help to you.\"";
        Cynthia[2].option1 = "Why is that?";
        Cynthia[2].option2 = "As long as they have a doctor then I don't care!";
        Cynthia[2].indexForOption1 = 5;
        Cynthia[2].indexForOption2 = 6;
        //response to index 1 option1
        Cynthia[3].response = "Your story seems to have struck a chord with her."
        +" Her guarded dimeanor seems to soften. "
        +"She says, \"I think I can help. Hold on.\"\n\nShe raises her hands in your direction. "
        +"A bright light emits from her palms. You feel a warmth in your chest and start feeling a little better.";
        Cynthia[3].option1 = "That was amazing! Thank you so much for helping me. "
        +"I don't know a lot of strangers that would be willing to help. ";
        Cynthia[3].option2 = "";
        Cynthia[3].indexForOption1 = 7;
        Cynthia[3].indexForOption2 = 8;
        //response to index 1 option2
        Cynthia[4].response = "Your impatient demeanor has caused her to become angry."
        +" She stands tall facing you. "
        +"\nShe says, \"Then move along traveller, strange things have been happening in town.\"\n";
        Cynthia[4].option1 = "I'll be going then.";
        Cynthia[4].option2 = "Restart?";
        Cynthia[4].indexForOption1 = -2;
        Cynthia[4].indexForOption2 = -1;
        /*-------------------------------index 4 does not continue------------------------------------------------------ */
        //response to index 2 option1
        Cynthia[5].response = "With confidence, she says, \"Those townspeople are always getting into trouble, but recently it's been worse."
        +" I've heard over the last few days people have gone missing. So I'd be weary of going to them. Especially since they don't know you."
        +" They could assume maybe you have something to do with it. Since you know, you're new here.\"";
        Cynthia[5].option1 = "I'm harmless, unless you get in my way.";
        Cynthia[5].option2 = "I could help them. If I wasn't hurt...";
        Cynthia[5].indexForOption1 = 11;
        Cynthia[5].indexForOption2 = 12;

        GameInfo.DialogueTrees[0] = Cynthia;
    }
    
    public static void loadAnkerDialogue(){

        
        GameInfo.DialogueTrees[1] = Anker;
    }
    
    public static void loadEdwardDialogue(){

        
        GameInfo.DialogueTrees[2] = Edward;
    }
    
    public static void loadEmrikDialogue(){

        
        GameInfo.DialogueTrees[3] = Emrik;
    }
    
    public static void loadBerndyDialogue(){

        
        GameInfo.DialogueTrees[4] = Berndy;
    }
    
    public static void loadModirDialogue(){

        
        GameInfo.DialogueTrees[5] = Modir;
    }
    
    public static void loadFarenvirDialogue(){

        
        GameInfo.DialogueTrees[6] = Farenvir;
    }
    
    public static void loadOzulDialogue(){

        
        GameInfo.DialogueTrees[7] = Ozul;
    }
    
    public static void loadFoxDialogue(){

        //firstEncounter
        Fox[0].response = "You make your best fox chatter at the animal but it doesn't seem to care.";
        Fox[0].option1 = "Talk";
        Fox[0].option2 = "Fight";
        Fox[0].indexForOption1 = 0;
        Fox[0].indexForOption2 = 0;
        
        GameInfo.DialogueTrees[8] = Fox;
        
    }
    
    public static void loadRockCreatureDialogue(){

        
        GameInfo.DialogueTrees[9] = RockCreature;
    }
    
    public static void loadRabbitDialogue(){

        
        GameInfo.DialogueTrees[10] = Rabbit;
    }

    

}