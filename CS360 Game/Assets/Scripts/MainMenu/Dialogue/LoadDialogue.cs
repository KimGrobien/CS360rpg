using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogue : MonoBehaviour {


	//Cynthia
    public static Node[] Cynthia = new Node[100];
    public static Node[] Emrik = new Node[50];
    public static Node[] Anker = new Node[100];
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
        +" Her guarded demeanor seems to soften. "
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
        Anker[1].response = "The old man says, \"What I've heard is people are disappearing lately. Most people've been staying home."
        +" Not much going on though. I've heard some people been havin' trouble with some pests just north of here. I wonder how much longer"
        +" this town will last.\"";
        Anker[1].option1 = "What do you think could be making people disappear?";
        Anker[1].option2 = "I'm going to go look into those pests.";
        Anker[1].indexForOption1 = 3;
        Anker[1].indexForOption2 = -2;
/*-------------------------------index 1 option 2 does not continue index 4 is empty----------------------------------------------------- */
        //response to index 0 option 2
        Anker[2].response = "He says, \"Even if everything wasn't okay. I wouldn't want to talk to a stranger about it.\"";
        Anker[2].option1 = "Why are the prices so high?";
        Anker[2].option2 = "I'm sorry I asked.(Restart?)";
        Anker[2].indexForOption1 = 5;
        Anker[2].indexForOption2 = -1;
/*-------------------------------index 6 option 2 does not continue index 6 is empty------------------------------------------------------ */
        //response to index 1 option 1
        Anker[3].response = "He says, \"Could be because we're out here in the middle of nowhere and no one wants to live here."+
        " Who would blame them for up and leaving? People don't usually make their way out here."
        +" They get lost, or get abandoned and end up here. Yeah that could be it or whatever is eating Sal's chickens finally got real hungry. \""
        + "\n\nHe kind of laughed at this. \n\"There's always that castle that sprung up a few years ago. No one goes there.\"";
        Anker[3].option1 = "How did you end up here?";
        Anker[3].option2 = "What's the story with the castle?";
        Anker[3].indexForOption1 = 7;
        Anker[3].indexForOption2 = 8;
        //response to index 2 option 1
        Anker[5].response = "The old man looks you directly in the eyes and says, \"That is a very rude question."
        +" Do you go to other shops and treat them like this too? Where are you from? What are you doing here? If you're just passing through"
        +" please keep going.";
        Anker[5].option1 = "I'm here to save those missing people.";
        Anker[5].option2 = "Fine I will be going.";
        Anker[5].indexForOption1 = 11;
        Anker[5].indexForOption2 = -2;
        //response to index 3 option 1
        Anker[7].response = "He says, \"I came here a long time ago, back when my daughter was born. Things were different back then, easier. "
        +" That was before the war ended... Once I sell all of my stock I plan to take me and my daughter far away from here.\""
        +"\n\nThe old man looked back at his items for sell. \"I don't have a use for those anymore.\"";
        Anker[7].option1 = "What war?";
        Anker[7].option2 = "I hope that you can find a better life outside of here.";
        Anker[7].indexForOption1 = 15;
        Anker[7].indexForOption2 = 16;
        //response to index 3 option 2
        Anker[8].response = "He says, \"That kind of just showed up overnight a few years ago. There are monsters guarding the place most"
        +" people stay away from it. I've never seen anyone coming or going from it though. If you're thinking that maybe the"
        +" missing people are there, I wouldn't think about it. You don't seem strong enough to go take on the place. If they went there"
        +" they're already dead.\"";
        Anker[8].option1 = "Maybe if you came with me I could take them on.";
        Anker[8].option2 = "I'm stronger than I look.";
        Anker[8].indexForOption1 = 17;
        Anker[8].indexForOption2 = 18;
        //response to index 5 option 1
        Anker[11].response = "He laughs and says, \"Good luck!\"\n\nHe doesn't seem like talking anymore.";
        Anker[11].option1 = "Leave";
        Anker[11].option2 = "Restart?";
        Anker[11].indexForOption1 = -2;
        Anker[11].indexForOption2 = -1;
        //response to index 7 option 1
        Anker[15].response = "The old man grips his can tighter then says, \"You don't know about the war? Where are you from?\"";
        Anker[15].option1 = "From far away.";
        Anker[15].option2 = "I don't know. I was attacked. I can't remember anything.";
        Anker[15].indexForOption1 = 31;
        Anker[15].indexForOption2 = 32;
        //response to index 7 option 2
        Anker[16].response = "He says, \"This place has been slowly dying anyways. Everyone wants to act like everything is okay"
        +" but if people just opened their eyes. This are starting to feel like they used to back when those things roamed freely."
        +"\n\nSince those of us who were able to fight got rid of them, it's easy for people to forget how things used to be."
        +" So, anywhere would be better than staying here and watch everyone else I know die. I just want to keep my daughter safe."
        +"\nShe looks so much like her mother.\"";
        Anker[16].option1 = "Where is her mother now?";
        Anker[16].option2 = "You'd rather leave everyone behind and not know their fates, when you could stay and protect them?";
        Anker[16].indexForOption1 = 33;
        Anker[16].indexForOption2 = 34;
        //response to index 8 option 1
        Anker[17].response = "He asks, \"Why would I do that? I told you it's hopeless.\"";
        Anker[17].option1 = "You seem strong, and protective. You could save these people.";
        Anker[17].option2 = "You could save your daughter from a similar fate.";
        Anker[17].indexForOption1 = 35;
        Anker[17].indexForOption2 = 36;
        //response to index 8 option 2
        Anker[18].response = "He says, \"Then go be strong somewhere else.\"";
        Anker[18].option1 = "Leave";
        Anker[18].option2 = "Restart?";
        Anker[18].indexForOption1 = -2;
        Anker[18].indexForOption2 = -1;
        //response to index 15 option 1
        Anker[31].response = "He says, \"If you're not going to buy anything then please go back to where you came from. I don't know you"
        +" and you don't seem right. I don't trust you.\"";
        Anker[31].option1 = "Leave";
        Anker[31].option2 = "Restart?";
        Anker[31].indexForOption1 = -2;
        Anker[31].indexForOption2 = -1;
        //response to index 15 option 2
        Anker[32].response = "At first it seemed the old man was going to get angry with you, but then he says, "
        +"\"I'm sorry you've been through that. This world must seem strange then. I must seem very unkind. Life has not been kind to me. "
        +"It's been very unforgiving actually. Forgive me. I hope you understand.\"";
        Anker[32].option1 = "Will you help me find the missing people?";
        Anker[32].option2 = "Acting like that won't get people to buy things from you.";
        Anker[32].indexForOption1 = 63;
        Anker[32].indexForOption2 = -2;
        //response to index 16 option 1
        Anker[33].response = "He stands there silent staring at you.";
        Anker[33].option1 = "Sir?";
        Anker[33].option2 = "Leave";
        Anker[33].indexForOption1 = 33;
        Anker[33].indexForOption2 = -2;
        //response to index 16 option 2
        Anker[34].response = "He says, \"At least then I won't feel the loss. Look if you're not going to buy anything please leave.\"";
        Anker[34].option1 = "Leave";
        Anker[34].option2 = "Restart?";
        Anker[34].indexForOption1 = -2;
        Anker[34].indexForOption2 = -1;
        //response to index 17 option 1
        Anker[35].response = "He says, \"They are not my problem. Buy something or get out.\"";
        Anker[35].option1 = "Leave";
        Anker[35].option2 = "Restart?";
        Anker[35].indexForOption1 = -2;
        Anker[35].indexForOption2 = -1;
        //response to index 17 option 2
        Anker[36].response = "The shopkeeper gets angry that you mentioned his daughter. He yells at you, \"Don't ever talk about her "
        +" she is not your business. I will take care of her how I need. Fight me.\"";
        Anker[36].option1 = "Leave";
        Anker[36].option2 = "Fight";
        Anker[36].indexForOption1 = -2;
        Anker[36].indexForOption2 = 0;
        //response to index 32 option 1
        Anker[63].response = "He says, \"I'll help you, but only to protect my daughter.\"";
        Anker[63].option1 = "Add to Party";
        Anker[63].option2 = "Leave";
        Anker[63].indexForOption1 = -3;
        Anker[63].indexForOption2 = 0;

        GameInfo.DialogueTrees[1] = Anker;
    }
    
    public static void loadEdwardDialogue(){
        Edward[0].response = "The doctor seems to look passed you as he talks and says, \"Did you need something stranger?\"";
        Edward[0].option1 = "I'm not feeling so good, could you heal me?";
        Edward[0].option2 = "Are you okay?";
        Edward[0].indexForOption1 = 1;
        Edward[0].indexForOption2 = 2;
        //response to index 0 option 1
        Edward[1].response = "He says,\"Of course I can heal you.\"\n\nHe gives you a look over then pulls out some medicine and gives it to you."
        +" You take them and feel a bit better.";
        Edward[1].option1 = "Thank you sir, are you doing okay?";
        Edward[1].option2 = "Now I can go";
        Edward[1].indexForOption1 = 3;
        Edward[1].indexForOption2 = -2;
        //response to index 0 option 2
        Edward[2].response = "He says, \"My son... my son is missing.\" He continues to stare at nothing.";
        Edward[2].option1 = "If you heal me I can go find him and bring him back to you.";
        Edward[2].option2 = "I'm going to bring him back for you!";
        Edward[2].indexForOption1 = 5;
        Edward[2].indexForOption2 = -2;  
        //response to index 1 option 1
        Edward[3].response = "He looks to the beds again. You can tell something is bothering him. He says,\"I've been trying to do nothing."
        +" It's so hard. I fear that if I do something then I will go back to the way I used to be... so, for your sake and everyone elses, "
        +"please leave me here to do nothing.\"";
        Edward[3].option1 = "Okay, I'll leave you alone.";
        Edward[3].option2 = "What do you think will happen if you do something?";
        Edward[3].indexForOption1 = -2;
        Edward[3].indexForOption2 = 8;
        //response to index 1 option 2
        Edward[5].response = "He says, \"I would have done that anyways. Doing good is all I feel like doing...\"";
        Edward[5].option1 = "Without you I wouldn't be able to do anything.";
        Edward[5].option2 = "I'm going to bring him back for you!";
        Edward[5].indexForOption1 = 11;
        Edward[5].indexForOption2 = -2;
        //response to index 3 option 2
        Edward[8].response = "He says, \"I will hurt people again... So I must do nothing. Please leave.\"";
        Edward[8].option1 = "I understand.";
        Edward[8].option2 = "Restart?";
        Edward[8].indexForOption1 = -2;
        Edward[8].indexForOption2 = -1;
        //response to index 11 option 1
        Edward[11].response = "He says, \"If I was stronger I would go save him myself.\"";
        Edward[11].option1 = "Well why don't you come with me? We can save him together.";
        Edward[11].option2 = "I'll be strong for you.";
        Edward[11].indexForOption1 = -3;
        Edward[11].indexForOption2 = -2;
        
        GameInfo.DialogueTrees[2] = Edward;
    }
    
    public static void loadEmrikDialogue(){
         Emrik[0].response = "The farmer looks at you and says, \"I have nothing for sale today boy. Come back later.\"";
        Emrik[0].option1 = "I wouldn't want to buy anything from you anyways your fields aren't lookin too good.";
        Emrik[0].option2 = "Are you waiting on someone?";
        Emrik[0].indexForOption1 = 1;
        Emrik[0].indexForOption2 = 2; 
        //response to index 0 option 1
        Emrik[1].response = "The farmer gets angry and yells, \"If you keep talking like that I'll kill you!\"";
        Emrik[1].option1 = "Don't get so mad. I'm leaving.";
        Emrik[1].option2 = "Do it.";
        Emrik[1].indexForOption1 = -2;
        Emrik[1].indexForOption2 = 0;
        //response to index 0 option 1
        Emrik[2].response = "The farmer says, \"My wife disappeared not too long ago, along with that doctor's son and that family."
        +" I think they just went somewhere and any day they'll return. I feel like it's going to be today.\"";
        Emrik[2].option1 = "Why would they leave in the first place?";
        Emrik[2].option2 = "Why not go look for her?";
        Emrik[2].indexForOption1 = 5;
        Emrik[2].indexForOption2 = 6;
        //response to index 2 option 1
        Emrik[5].response = "The farmer says, \"Maybe they seen a heard of wild cows and went to go catch one, or maybe"
        +" they watched that young boy run off into the forest or something and are still looking for him." 
        +" I don't know. But they better get back soon.\"";
        Emrik[5].option1 = "How long will you wait?";
        Emrik[5].option2 = "Is there anything I can do?";
        Emrik[5].indexForOption1 = 11;
        Emrik[5].indexForOption2 = 12;
        //response to index 2 option 2
        Emrik[6].response = "The farmer says, \"Did you see that woman who lives just outside of town? She's like a daughter to me."
        +" If I left and something happened to her, and I lost both my wife and daughter... I don't know what I would do.\"";
        Emrik[6].option1 = "I understand, but wouldn't it make more sense to go get rid of the threat? You seem capable.";
        Emrik[6].option2 = "She seems to be able to take care of herself.";
        Emrik[6].indexForOption1 = 13;
        Emrik[6].indexForOption2 = 14;
        //response to index 2 option 1
        Emrik[11].response = "The farmer says, \"As long as I have to. If you could leave me alone. I don't need another person to worry about.\"";
        Emrik[11].option1 = "Restart?";
        Emrik[11].option2 = "Leave";
        Emrik[11].indexForOption1 = -1;
        Emrik[11].indexForOption2 = -2;
        //response to index 2 option 1
        Emrik[12].response = "The farmer says, \"You can leave me alone.\"";
        Emrik[12].option1 = "Restart?";
        Emrik[12].option2 = "Leave";
        Emrik[12].indexForOption1 = -1;
        Emrik[12].indexForOption2 = -2;
        //response to index 2 option 1
        Emrik[13].response = "The farmer says, \"If I knew what the threat was I would get rid of it right now. Do you know what it is or where she's gone?\"";
        Emrik[13].option1 = "Yes, I do and if you come with me we can save her.";
        Emrik[13].option2 = "No, but I think we can figure it out together.";
        Emrik[13].indexForOption1 = -3;
        Emrik[13].indexForOption2 = 28;
        //response to index 2 option 1
        Emrik[14].response = "The farmer says, \"Some people say she's witch. I just think she has a gift, but she cannot defend herself for long."
        +" So you met her\"";
        Emrik[14].option1 = "Yes, I do and if you come with me we can save her.";
        Emrik[14].option2 = "No, but I think we can figure it out together.";
        Emrik[14].indexForOption1 = -3;
        Emrik[14].indexForOption2 = 28;

        
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
        Ozul[0].response = "He asks, \"What are you?\"";
        Ozul[0].option1 = "???";
        Ozul[0].option2 = "???";
        Ozul[0].indexForOption1 = 1;
        Ozul[0].indexForOption2 = 2;
        //response to index 0 option 1
        Ozul[1].response = "He asks, \"No response? Do you even know? My name is Ozul I was born into this world let down from god into"
        +" the loving arms of my mother and father. Do you remember your mother and father?\"";
        Ozul[1].option1 = "Yes";
        Ozul[1].option2 = "No";
        Ozul[1].indexForOption1 = 3;
        Ozul[1].indexForOption2 = 4;
        //response to index 0 option 2
        Ozul[2].response = "He says, \"You walk into my home and are willing to hurt the ones I protect? And for what? I see you've gained nothing."
        +" For entertainment? Does our suffering amuse you?\"";
        Ozul[2].option1 = "Yes";
        Ozul[2].option2 = "No";
        Ozul[2].indexForOption1 = 5;
        Ozul[2].indexForOption2 = 6;
        //response to index 1 option 1
        Ozul[3].response = "He says, \"You are lying.\"";
        Ozul[3].option1 = "How would you know?";
        Ozul[3].option2 = "Attack!";
        Ozul[3].indexForOption1 = 7;
        Ozul[3].indexForOption2 = 0;
        //response to index 1 option 2
        Ozul[4].response = "He says, \"Do you feel anything at all?\"";
        Ozul[4].option1 = "Yes";
        Ozul[4].option2 = "No";
        Ozul[4].indexForOption1 = 9;
        Ozul[4].indexForOption2 = 10;
        //response to index 2 option 1
        Ozul[5].response = "He says, \"Then allow me to join in on the fun!\"";
        Ozul[5].option1 = "Leave";
        Ozul[5].option2 = "Fight";
        Ozul[5].indexForOption1 = -2;
        Ozul[5].indexForOption2 = 0;
        //response to index 2 option 2
        Ozul[6].response = "He says, \"For some reason, I believe you. But, that does not change what you have done. Allow me to show you what suffering"
        +" feels like.\"";
        Ozul[6].option1 = "Leave";
        Ozul[6].option2 = "Fight";
        Ozul[6].indexForOption1 = -2;
        Ozul[6].indexForOption2 = 0;
        //response to index 3 option 1
        Ozul[7].response = "He says, \"I can't feel your shadows. I'm done talking. My shadows will destroy you!\"";
        Ozul[7].option1 = "Leave";
        Ozul[7].option2 = "Fight";
        Ozul[7].indexForOption1 = -2;
        Ozul[7].indexForOption2 = 0;
        //response to index 4 option 1
        Ozul[9].response = "He says, \"You're lying. I've seen what kind of a monster you are. You'll pay for what you've done!\"";
        Ozul[9].option1 = "Leave";
        Ozul[9].option2 = "Fight";
        Ozul[9].indexForOption1 = -2;
        Ozul[9].indexForOption2 = 0;
        //response to index 4 option 2
        Ozul[10].response = "He says, \"Then leave. Leave this place and never return.\"";
        Ozul[10].option1 = "Leave";
        Ozul[10].option2 = "Fight";
        Ozul[10].indexForOption1 = -2;
        Ozul[10].indexForOption2 = 0;

        
        GameInfo.DialogueTrees[7] = Ozul;
    }
    
    public static void loadFoxDialogue(){

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
			textToScreen = "The flapping of his wings causes air to rush past you. Before you stands a man he says, "
            +"\n\"You must be it.\"";
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
			if(GameInfo.currentNPC==3){
				textToScreen = "He gives you an angry look and turns away from you.";
			}
			if(GameInfo.currentNPC==2){
				textToScreen = "He doesn't seem to notice you standing there. He only stares out over the empty beds.";
			}
            if(GameInfo.currentNPC == 7){
				textToScreen = "He says, \"You are trapped. It is worse than you could have imagined.\"";
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
				textToScreen = "He says, \"I've failed everyone I've tried to take care of, you can't trust me. But, I'll go with you.\"";
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
				textToScreen = "He says, \"Have you noticed that the shadows seem darker? I wonder if it has to do with those monsters\"";
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