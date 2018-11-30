using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogue : MonoBehaviour {


	//Cynthia
    public static Node[] Cynthia = new Node[100];
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
-4 means changing party slots
-5 means already in party
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
        //Response to index 0 option 1
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
        //response to index 0 option 2
        Cynthia[2].response = "She says, \"You can find a small village just up the road."
        +"Though they may not be much help to you.\"";
        Cynthia[2].option1 = "Why is that?";
        Cynthia[2].option2 = "As long as they have a doctor then I don't care!";
        Cynthia[2].indexForOption1 = 5;
        Cynthia[2].indexForOption2 = 6;
        //response to index 1 option 1
        Cynthia[3].response = "Your story seems to have struck a chord with her."
        +" Her guarded dimeanor seems to soften. "
        +"She says, \"I think I can help. Hold on.\"\n\nShe raises her hands in your direction. "
        +"A bright light emits from her palms. You feel a warmth in your chest and start feeling a little better.";
        Cynthia[3].option1 = "That was amazing! Thank you so much for helping me. "
        +"I don't know a lot of strangers that would be willing to help. ";
        Cynthia[3].option2 = "Where did you learn that?";
        Cynthia[3].indexForOption1 = 7;
        Cynthia[3].indexForOption2 = 8;
        //response to index 1 option 2
        Cynthia[4].response = "Your impatient demeanor has caused her to become angry."
        +" She stands tall facing you. "
        +"\nShe says, \"Then move along traveller, strange things have been happening in town.\"\n";
        Cynthia[4].option1 = "I'll be going then.";
        Cynthia[4].option2 = "Restart?";
        Cynthia[4].indexForOption1 = -2;
        Cynthia[4].indexForOption2 = -1;
    /*-------------------------------index 4 does not continue index 9 and 10 are empty------------------------------------------------------ */
        //response to index 2 option 1
        Cynthia[5].response = "With confidence, she says, \"Those townspeople are always getting into trouble, but recently it's been worse."
        +" I've heard over the last few days people have gone missing. So I'd be weary of going to them. Especially since they don't know you."
        +" They could assume maybe you have something to do with it. Since you know, you're new here.\"";
        Cynthia[5].option1 = "I'm harmless, unless you get in my way.";
        Cynthia[5].option2 = "I could help them. If I wasn't hurt...";
        Cynthia[5].indexForOption1 = 11;
        Cynthia[5].indexForOption2 = 12;
        
        //response to index 2 option 2
        Cynthia[6].response = "She says, \"There is a doctor in town. Please leave.\"";
        Cynthia[6].option1 = "I'm gone.";
        Cynthia[6].option2 = "Restart?";
        Cynthia[6].indexForOption1 = -2;
        Cynthia[6].indexForOption2 = -1;
    /*-------------------------------index 6 does not continue index 13 and 14 are empty------------------------------------------------------ */

        //response to index 3 option 1
        Cynthia[7].response = "She proudly says, \"Well my parents taught me to help those in need. I've learned you can't trust everyone."
        +" I try to balance the two. What brings you to town?\"";
        Cynthia[7].option1 = "I don't remember.";
        Cynthia[7].option2 = "Just passing through.";
        Cynthia[7].indexForOption1 = 15;
        Cynthia[7].indexForOption2 = 16;

        //response to index 3 option 2
        Cynthia[8].response = "She says, \"It was when I was younger. I used to practice a lot, but now I've seem to have hit a wall in my learning."
        +" My parents were the only other people in the world who could heal the way I can. \n\nFor the last few years I've just been "
        +"working on medicine.\"";
        Cynthia[8].option1 = "Could you teach me how to do that?";
        Cynthia[8].option2 = "But you're not a doctor? I'm sure people could really use your help.";
        Cynthia[8].indexForOption1 = 17;
        Cynthia[8].indexForOption2 = 18;

        //response to index 5 option 1
        Cynthia[11].response = "Impatiently she says, \"Well, I'm not here to fight you. If that's all you need. I have a lot of work to do here.\"";
        Cynthia[11].option1 = "Leave";
        Cynthia[11].option2 = "Restart?";
        Cynthia[11].indexForOption1 = -2;
        Cynthia[11].indexForOption2 = -1;
/*-------------------------------index 11 does not continue index 23 and 24 are empty------------------------------------------------------ */

        //response to index 5 option 2 
        Cynthia[12].response = "She says, \"Are you some kind of Knight or hero or something?\" You know she meant it to mock you. "
        +"\n\nShe gives you a look over implying that maybe you couldn't even help yourself."
        +" She then says, \"You look like you could use some help. Let me try this.\" \nAs she says this she waves her hands and a white light blinds you."
        + "You feel much better.";
        Cynthia[12].option1 = "Thank you for that, but I'm not the only one that needs help.";
        Cynthia[12].option2 = "Thanks, I'll be on my way. It seems like people need me.";
        Cynthia[12].indexForOption1 = 25;
        Cynthia[12].indexForOption2 = -2;
    /*-------------------------------index 12 option 2 does not continue index 26 is empty------------------------------------------------------ */

        //response to index 7 option 1
        Cynthia[15].response = "She gives you a weird look. \n\nShe says, \"You must've hit your head pretty hard. "
        +"What will you do now? Are you going to be okay travelling alone?\"";
        Cynthia[15].option1 = "I'll be fine. Thank you for everything.";
        Cynthia[15].option2 = "Would you like to come with me?";
        Cynthia[15].indexForOption1 = -2;
        Cynthia[15].indexForOption2 = -3;
/*-------------------------------index 15 does not continue index 31 and 32 are empty------------------------------------------------------ */

        //response to index 7 option 2
        Cynthia[16].response = "She says, \"You need to be more careful. Do you think you're going to be okay moving on?"
        +" I wonder if the inn is still closed in town. Not many people would be willing to let you stay with them. Not since that family disappeared.\"";
        Cynthia[16].option1 = "What happened to them?";
        Cynthia[16].option2 = "I think I can make it. Thank you so much for your help.";
        Cynthia[16].indexForOption1 = 33;
        Cynthia[16].indexForOption2 = -2;
/*-------------------------------index 16 option 2 ends here index 34 is empty------------------------------------------------------ */

        //response to index 8 option 1
        Cynthia[17].response = "She says, \"I don't really understand how I do it myself. Maybe one day I could let you look at some of"
        +" my parents old spell books. I don't know if people other than my family can learn them though."
        +"\nThey have this book, you see, that's full of all the techniques they've ever used, but it was written by them, for them."
        +" So, it's a real mystery for me to wrap my head around.\"";
        Cynthia[17].option1 = "What happened to your parents?";
        Cynthia[17].option2 = "You could be helping people, why are you here?";
        Cynthia[17].indexForOption1 = 35;
        Cynthia[17].indexForOption2 = 36;
        
        //response to index 8 option 2
        Cynthia[18].response = "Cynthia stands there for a moment and looks over toward the back of her house. She says, "
            +"\"This is my home.  I take care of myself. If for some reason people need my help and make it to me, I will never hesitate to help.\""
            +"\n\nShe seems annoyed that you asked.";
        Cynthia[18].option1 = "Well I'm glad I found you when I did.";
        Cynthia[18].option2 = "Do you want to come with me and help people?";
        Cynthia[18].indexForOption1 = 37;
        Cynthia[18].indexForOption2 = 38;

        //repsonse to index 12 option 1
        Cynthia[25].response = "She says, \"The people in town don't like me very much, not since that doctor came. They think I'm a witch."
        +" The only one who is remotely kind to me is Emrik but that's because we trade food every now and then. No one can grow peaches like me."
        +" So, like I said, I don't owe them anything. If you are fine I'd like it if you left.\""
        +"\n\n She turned her attention back to her garden.";
        Cynthia[25].option1 = "You could be a hero. Instead you turn your back on people who need you.";
        Cynthia[25].option2 = "Fine be selfish. I'll be going.";
        Cynthia[25].indexForOption1 = 51;
        Cynthia[25].indexForOption2 = -2;
/*-------------------------------index 25 option 2 does not continue index 52 is empty------------------------------------------------------ */

        //response to index 16 option 1
        Cynthia[33].response = "She says, \"Honestly? No one knows, they just up and disappeared last week. Even more have gone missing since then."
        +" The doctor's boy and Emrik's wife. I feel really bad for Emrik. She was everything to him. So just be careful out there, okay?\"";
        Cynthia[33].option1 = "I'll take care of myself, if you take care of yourself.";
        Cynthia[33].option2 = "Thank you for everything.";
        Cynthia[33].indexForOption1 = -2;
        Cynthia[33].indexForOption2 = -2;
/*-------------------------------index 33 option 2 does not continue index 68 is empty------------------------------------------------------ */
       
        //response to index 17 option 1
        Cynthia[35].response = "She goes quiet for a moment. You can tell the question is hard for her." 
        +"\n\nShe says, \"They died a long time ago, suddenly. I don't really want to talk about it. It happened when I was young, but they"
        +" were everything to me.\""
        +"\n\nYou both stand there in silence for a moment not sure what to say.";
        Cynthia[35].option1 = "I bet if you came with me and helped all these people they would be so proud of you.";
        Cynthia[35].option2 = "I didn't mean to disturb you ma'am. I'll leave you be.";
        Cynthia[35].indexForOption1 = 71;
        Cynthia[35].indexForOption2 = -2;
/*-------------------------------index 35 option 2 does not continue index 72 is empty------------------------------------------------------ */
        
        //response to index 17 option 2
        Cynthia[36].response = "She gets defensive and says, \"What I do is really none of your business. I've made my choices.\"";
        Cynthia[36].option1 = "Fine then! I'll leave you here alone.";
        Cynthia[36].option2 = "Restart?";
        Cynthia[36].indexForOption1 = -2;
        Cynthia[36].indexForOption2 = -1;
/*-------------------------------index 36 does not continue index 73 and 74 are empty------------------------------------------------------ */
        //response to index 18 option 1
        Cynthia[37].response = "She says, \"It's just dumb luck. Be more careful next time.\"";
        Cynthia[37].option1 = "Thank you for everything I'll be going.";
        Cynthia[37].option2 = "Restart?";
        Cynthia[37].indexForOption1 = -2;
        Cynthia[37].indexForOption2 = -1;
/*-------------------------------index 37 does not continue index 75 and 76 are empty------------------------------------------------------ */
        //response to index 18 option 2
        Cynthia[38].response = "She seems hesitant to answer. Then she says, \"I'll help you, but only because those children that are missing"
        +" could need me. NOT because I think it's the right thing to do.\"";
        Cynthia[38].option1 = "We'll find them.";
        Cynthia[38].option2 = "Restart?";
        Cynthia[38].indexForOption1 = -3;
        Cynthia[38].indexForOption2 = -1;
/*-------------------------------index 37 does not continue index 75 and 76 are empty------------------------------------------------------ */
        //response to index 25 option 1
        Cynthia[51].response = "Angrily she says, \"What I do is none of your business. Leave.\"";
        Cynthia[51].option1 = "Leave";
        Cynthia[51].option2 = "Restart?";
        Cynthia[51].indexForOption1 = -2;
        Cynthia[51].indexForOption2 = -1;
/*-------------------------------index 51 does not continue ------------------------------------------------------ */
        //response to index 35 option 1
        Cynthia[71].response = "She stands there still for a moment. She then looks at you with watery eyes and says, "
        +"\"You don't know what they would want. They're dead. Leave me alone.\"";
        Cynthia[71].option1 = "Leave";
        Cynthia[71].option2 = "Restart?";
        Cynthia[71].indexForOption1 = -2;
        Cynthia[71].indexForOption2 = -1;
        GameInfo.DialogueTrees[0] = Cynthia;
        
/*
template
        Cynthia[0].response = "";
        Cynthia[0].option1 = "Talk";
        Cynthia[0].option2 = "Fight";
        Cynthia[0].indexForOption1 = 0;
        Cynthia[0].indexForOption2 = 0;
 */
    }
    
    public static void loadAnkerDialogue(){
        //Anker

        //currentIndex will lead to (currentIndex*2)+1 as option1 and (currentIndex*2)+2 as option2
        //firstEncounter after pressing talk
        Anker[0].response = "The old man glares at you through his unique glasses waiting for you to say something."
        +" \nHe waits nearly a minute before saying, \"I don't really feel like talking. But if you'd like to buy something."
        +" I think I could find something you may like.\"";
        Anker[0].option1 = "What's the news around town?";
        Anker[0].option2 = "Is everything okay?";
        Anker[0].indexForOption1 = 1;
        Anker[0].indexForOption2 = 2;
        //response to index 0 option 1
        Anker[1].response = "The old man glares at you through his unique glasses waiting for you to say something."
        +" \nHe waits nearly a minute before saying, \"I don't really feel like talking. But if you'd like to buy something."
        +" I think I could find something you may like.\"";
        Anker[1].option1 = "What's the news around town?";
        Anker[1].option2 = "Is everything okay?";
        Anker[1].indexForOption1 = 1;
        Anker[1].indexForOption2 = 2;
        
        GameInfo.DialogueTrees[1] = Anker;
    }
    
    public static void loadEdwardDialogue(){

        
        GameInfo.DialogueTrees[2] = Edward;
    }
    
    public static void loadEmrikDialogue(){

        
        GameInfo.DialogueTrees[3] = Emrik;
    }
    
    public static void loadBerndyDialogue(){

        Berndy[0].response = "The creature makes a sound you think you've heard in a dream once, but other than that"
        +" didn't seem to understand you.";
        Berndy[0].option1 = "Talk";
        Berndy[0].option2 = "Fight";
        Berndy[0].indexForOption1 = 0;
        Berndy[0].indexForOption2 = 0;

        GameInfo.DialogueTrees[4] = Berndy;
    }
    
    public static void loadModirDialogue(){
        Modir[0].response = "You begin to speak but for some reason the way it's looking at you, you choose not to."
        +"\n\nYou feel like you've seen this creature before. You can remember the warm embrace of your mother."
        +" This creature staring at you feels much colder.";
        Modir[0].option1 = "Talk";
        Modir[0].option2 = "Fight";
        Modir[0].indexForOption1 = 0;
        Modir[0].indexForOption2 = 0;
        
        GameInfo.DialogueTrees[5] = Modir;
    }
    
    public static void loadFarenvirDialogue(){
        Farenvir[0].response = "You cannot speak.";
        Farenvir[0].option1 = "Talk";
        Farenvir[0].option2 = "Fight";
        Farenvir[0].indexForOption1 = 0;
        Farenvir[0].indexForOption2 = 0;

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
        RockCreature[0].response = "What kind of noise would this make? You try to imitate the sounds of a kickdrum. Nothing happens.";
        RockCreature[0].option1 = "Talk";
        RockCreature[0].option2 = "Fight";
        RockCreature[0].indexForOption1 = 0;
        RockCreature[0].indexForOption2 = 0;

        
        GameInfo.DialogueTrees[9] = RockCreature;
    }
    
    public static void loadRabbitDialogue(){
        Rabbit[0].response = "You say, \"Hey rabbit leave those carrots alone, they're not yours.\""
        +"\n\nThe fluff ball ignores you and continues to eat the carrots.";
        Rabbit[0].option1 = "Talk";
        Rabbit[0].option2 = "Fight";
        Rabbit[0].indexForOption1 = 0;
        Rabbit[0].indexForOption2 = 0;

        
        GameInfo.DialogueTrees[10] = Rabbit;
    }

    public static string SetBaseNPCResponses(){
        string textToScreen="";
		if(GameInfo.currentNPC==0){
			textToScreen = "You approach a small woman standing next to a garden." 
			+" Her hands are covered in dirt and her forehead in sweat. You can tell she's" 
			+" been working outside all day. She notices you walking up to her. She gives"
			+" you a kind smile. ";
		}
		if(GameInfo.currentNPC==1){
			textToScreen = "An old man stands before you, broken. Both in body and soul. The look in his eyes says he's given up a long time ago." 
			+" The room is filled with many rare items, and a few common. He's clearly an old adventurer." 
			+" \n\nYou wonder why he's selling his treasures. You also consider that it might be easier just to kill him and take all the loot for yourself.";
		
		}
		if(GameInfo.currentNPC==3){
			textToScreen = "You walk into the gates of a supposed farm. The crop here is wilted and lifeless, and the person you assume is a farmer" 
			+" is standing near the gate looking down the road. It was as if he was expecting someone soon. He was tall and strong, but very unconcerned" 
			+" with his field. If this was the town's only source of food, they were in trouble.";
		
		}
		if(GameInfo.currentNPC==2){
			textToScreen = "The hospital is empty save for a doctor standing near empty beds." 
			+" He doesn't seem too concerned with you. He stands in silence, lost in his mind." 
			+" You wonder if you should talk with him at all. Would he even respond?";
		}
		if(GameInfo.currentNPC==4){//Berndy
			textToScreen = "A small creature emerges from the shadows.";
		}
		if(GameInfo.currentNPC==5){//Modir
			textToScreen = "This creature looks tired.";
		}
		if(GameInfo.currentNPC==6){//Farenvir
			textToScreen = "You feel as though gravity has increased in the presence of this creature."+
			"\nYou felt this before, somewhere.";
		}
        if(GameInfo.currentNPC==7){//Ozul
			textToScreen = "Before you stands a man, he says, \"\"";
		}
		if(GameInfo.currentNPC == 8){
			textToScreen = "Through the thick trees you see a fox tearing through a pile of feathers.\n\n"
			+"It looks at you with regretful eyes.";
		}
		if(GameInfo.currentNPC== 9){
			textToScreen = "A strange rock creature appears before you. It must have been making all those holes around town.";
        }
		if(GameInfo.currentNPC == 10){
			textToScreen = "In the tall grass near a small hole in the ground, you see a rabbit eating a carrot.";
		}
        return textToScreen;
    }
    
    public static string setNPCResponseIfEncounteredAlot(){
        string textToScreen="";
			if(GameInfo.currentNPC==0){
				textToScreen = "She says,\"I really must be getting back to work.\"";
			}
			if(GameInfo.currentNPC==1){
				textToScreen = "He says, \"If you're not going to buy anything, please leave me alone...\"";
			}
			if(GameInfo.currentNPC==2){
				textToScreen = "He gives you an angry look and turns away from you.";
			}
			if(GameInfo.currentNPC==3){
				textToScreen = "He doesn't seem to notice you standing there. He only stares out over the empty beds.";
			}
			if(GameInfo.currentNPC == 8){
				textToScreen = "The fox looks at you as if you are the grim reaper himself.";
			}
			if(GameInfo.currentNPC==10){
				textToScreen = "This rabbit is smaller than the others.";
			}
			if(GameInfo.currentNPC==9){
				textToScreen = "How are there so many of these things?";
			}
            return textToScreen;

	}

    public static string setNPCResponseIfRecruitable(){
        string textToScreen = "";
        	if(GameInfo.currentNPC==0){
				textToScreen = "She says,\"I really must be getting back to work. \n Unless you need me to go with you?\"";
			}
			if(GameInfo.currentNPC==1){
				textToScreen = "He says, \"I live for my daughter, I'll die for her too. I'm ready to go when you are.\"";
			}
			if(GameInfo.currentNPC==3){
				textToScreen = "He says, \"Take me to the demon that took my wife!\"";
			}
			if(GameInfo.currentNPC==2){
				textToScreen = "He says, \"I've failed everyone I've tried to take care of, you can't trust me.\"";
			}
            return textToScreen;
    }

public static string setNPCResponseIfOnTeam(){
        string textToScreen = "";
        	if(GameInfo.currentNPC==0){
				textToScreen = "She says,\"This is my home, I'll be here until you need me to fight.\"";
			}
			if(GameInfo.currentNPC==1){
				textToScreen = "He says, \"I'll be here at the shop if you want to buy anything.\"";
			}
			if(GameInfo.currentNPC==3){
				textToScreen = "He says, \"Can I trust you to save my wife?\"";
			}
			if(GameInfo.currentNPC==2){
				textToScreen = "He says, \"Have you noticed that the shadows seem darker?\nI wonder if it has to do with those monsters\"";
			}
            return textToScreen;
    }
}

/*
template
        Fox[0].response = "";
        Fox[0].option1 = "Talk";
        Fox[0].option2 = "Fight";
        Fox[0].indexForOption1 = 0;
        Fox[0].indexForOption2 = 0;
 */
