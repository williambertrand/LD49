using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript
{
    private Dictionary<string, Dialogue> script;

    public Character player;
    public Character blobber;


    public GameScript()
    {
        script = new Dictionary<string, Dialogue>();

        player = new Character("You", true, "Portraits/playerPortrait");
        blobber = new Character("Floating Blob", false, "Portraits/blobPlaceholder");

        // Scripts needed for tutorial
        if(SceneManager.GetActiveScene().buildIndex == GameScenes.OPENING)
        {
            AddRoomOneDialogue();
            addSoulsDialogue();
        }

        // Scripts needed for gameplay scene
        if (SceneManager.GetActiveScene().buildIndex == GameScenes.GAMEPLAY)
        {
            addFirstPortalScript();
            addBreakRoomDialogue();
        }

        if (SceneManager.GetActiveScene().buildIndex == GameScenes.STORE)
        {
            addBreakRoomDialogue();
        }

        // Scripts needed for gameplay scene
        if (SceneManager.GetActiveScene().buildIndex == GameScenes.END)
        {
            addEndingScripts();
        }
    }

    public Dialogue GetDialogue(string id)
    {
        Dialogue d;
        script.TryGetValue(id, out d);
        return d;
    }

    private void AddRoomOneDialogue()
    {
        Dialogue firstInteractionWithBlobber = new Dialogue();
        DialogueItem line1 = new DialogueItem(
            player,
            "What is this... this place? Where are we?"
        );
        firstInteractionWithBlobber.lines.Add(line1);

        DialogueItem line2 = new DialogueItem(
            blobber,
            "Ugh not this again... you lost souls really are helpless. You ask me the same question every day.... you really don't remember?"
        );
        firstInteractionWithBlobber.lines.Add(line2);

        DialogueItem line3 = new DialogueItem(
            player,
            "No, I don't. I'm sorry."
        );
        firstInteractionWithBlobber.lines.Add(line3);

        DialogueItem line4 = new DialogueItem(
            blobber,
            "It's not your fault. Sorry for being short with you, it's been a long day..."
        );
        firstInteractionWithBlobber.lines.Add(line4);

        DialogueItem line5 = new DialogueItem(
            blobber,
            "You're a lost soul, between worlds right now, and highly unstable. Which is why you can't remember anything."
        );
        firstInteractionWithBlobber.lines.Add(line5);

        DialogueItem line6 = new DialogueItem(
            player,
            "Unstable?"
        );
        firstInteractionWithBlobber.lines.Add(line6);

        DialogueItem line7 = new DialogueItem(
            blobber,
            "Yes. You're caught between worlds, phasing in and out of existince here in this place."
        );
        firstInteractionWithBlobber.lines.Add(line7);

        DialogueItem line8 = new DialogueItem(
            blobber,
            "Those white flashing lights you've been picking up are called soul shards. Those will help keep you in this phase of existince."
        );
        firstInteractionWithBlobber.lines.Add(line8);

        DialogueItem line81 = new DialogueItem(
            blobber,
            "If you don't pick more of those up, you'll go completely unstable."
        );
        firstInteractionWithBlobber.lines.Add(line81);

        

        DialogueItem line9 = new DialogueItem(
            player,
            "What happens then?"
        );
        firstInteractionWithBlobber.lines.Add(line9);

        DialogueItem line10 = new DialogueItem(
            blobber,
            "Uhhh....well, it's a very painful experience for your kind. But that's all I really know. Your memory loss may be a blessing in that sense."
        );
        firstInteractionWithBlobber.lines.Add(line10);

        DialogueItem line11 = new DialogueItem(
            blobber,
            "That's enouch chatting. Quickly now, go grab some shards over to the right."
        );
        firstInteractionWithBlobber.lines.Add(line11);

        script.Add("firstInteractionWithBlobber", firstInteractionWithBlobber);

        Dialogue secondTimeTalkingWithBlobber = new Dialogue();
        DialogueItem secondTimeTalkingToBlobber = new DialogueItem(
            blobber,
            "Go On! Head over to your right to grab some soul shards."
        );
        secondTimeTalkingWithBlobber.lines.Add(secondTimeTalkingToBlobber);

        script.Add("firstInteractionWithBlobber2", secondTimeTalkingWithBlobber);
    }

    public void addFirstPortalScript()
    {
        Dialogue blobberPortal= new Dialogue();
        DialogueItem line1 = new DialogueItem(
            blobber,
            "Hey buddy, you see that white, round, circle with the markings. Thats a portal."
        );
        blobberPortal.lines.Add(line1);

        DialogueItem line2 = new DialogueItem(
            player,
            "A portal?"
        );
        blobberPortal.lines.Add(line2);

        DialogueItem line3 = new DialogueItem(
            blobber,
            "Yep. It'll take you to the next room. Where you'll find more lost souls to get soul shards from."
        );
        blobberPortal.lines.Add(line3);

        DialogueItem line4 = new DialogueItem(
            player,
            "I don't feel too great taking soul shards from other lost souls."
        );
        blobberPortal.lines.Add(line4);

        DialogueItem line5 = new DialogueItem(
            blobber,
            "Well, you don't want to go completely unstable, do you? Sorry, but that's just the world we live in."
        );
        blobberPortal.lines.Add(line5);

        DialogueItem line5b = new DialogueItem(
            blobber,
            "Well I guess \"live\" isn't really the right word for you anymore, but you get what I mean."
        );
        blobberPortal.lines.Add(line5b);

        script.Add("aboutPortalsDialogue", blobberPortal);


        Dialogue secondBlobberPortals = new Dialogue();
        DialogueItem line11 = new DialogueItem(
            blobber,
            "Go on, hop into that portal and go get yourself some more soul shards."
        );
        secondBlobberPortals.lines.Add(line11);

        script.Add("secondAboutPortalsDialogue", secondBlobberPortals);
    }

    private void addSoulsDialogue()
    {

        Dialogue aboutSouls1 = new Dialogue();
        DialogueItem line1 = new DialogueItem(
            player,
            "What are these things? And Why are they chasing me?."
        );
        aboutSouls1.lines.Add(line1);

        DialogueItem line2 = new DialogueItem(
            blobber,
            "Those are also lost souls, like you. They're looking for soul shards too."
        );
        aboutSouls1.lines.Add(line2);

        DialogueItem line3 = new DialogueItem(
            player,
            "Why do they look so different from me?"
        );
        aboutSouls1.lines.Add(line3);


        DialogueItem line4 = new DialogueItem(
            blobber,
            "They went unstable a long time ago. Fading in and out of this place ever since..."
        );
        aboutSouls1.lines.Add(line4);

        DialogueItem line5 = new DialogueItem(
            blobber,
            "And so their existence has been reduced to only one thing: looking for soul shards."
        );
        aboutSouls1.lines.Add(line5);

        DialogueItem line6 = new DialogueItem(
            blobber,
            "As a younger lost soul, your existence is still relatively stable. They are drawn to your soul stability, and all the soul shards you carry with you."
        );
        aboutSouls1.lines.Add(line6);

        Dialogue aboutSouls2 = new Dialogue();
        DialogueItem line21 = new DialogueItem(
            blobber,
            "You should really keep moving."
        );
        aboutSouls2.lines.Add(line21);

        script.Add("aboutSouls1", aboutSouls1);
        script.Add("aboutSouls2", aboutSouls2);
    }



    // TODO: Room 5!!!

    private void addBreakRoomDialogue()
    {
        Dialogue breakRoom = new Dialogue();
        DialogueItem line1 = new DialogueItem(
            blobber,
            "How are you doing out there? Looks like you are collecting a lot of soul shards."
        );
        breakRoom.lines.Add(line1);

        DialogueItem line2 = new DialogueItem(
            player,
            "Yes, but its hard work."
        );
        breakRoom.lines.Add(line2);

        DialogueItem line3 = new DialogueItem(
            blobber,
            "That it is, taking pieces of someone else, all for yourself, shouldn't really be too easy, should it?"
        );
        breakRoom.lines.Add(line3);

        DialogueItem line4 = new DialogueItem(
            player,
            "Yes, but like you said, I don't really have a choice."
        );
        breakRoom.lines.Add(line4);

        DialogueItem line5 = new DialogueItem(
            blobber,
            "Hmmm. I said this is the world we live in, but I think you always have a choice...."
        );
        breakRoom.lines.Add(line5);


        script.Add("room5Talk", breakRoom);
    }


    // TODO: Ending dialogue

    private void addEndingScripts()
    {
        Dialogue ending = new Dialogue();
        DialogueItem line1 = new DialogueItem(
            blobber,
            "I can't believe it. You've become the most stable lost soul I've ever seen."
        );
        ending.lines.Add(line1);

        DialogueItem line2 = new DialogueItem(
            blobber,
            "The number of shards you've taken from those other souls... is astounding."
        );
        ending.lines.Add(line2);

        DialogueItem line3 = new DialogueItem(
            player,
            "Those other souls, did I set them free?"
        );
        ending.lines.Add(line3);

        DialogueItem line4 = new DialogueItem(
            blobber,
            "Perhaps..., yes, maybe you did."
        );
        ending.lines.Add(line4);

        DialogueItem line5 = new DialogueItem(
            blobber,
            "... But more likely, you just blasted them into oblivion."
        );
        ending.lines.Add(line5);

        DialogueItem line6 = new DialogueItem(
            player,
            "I was just trying to survive here. I don't want to lose my stability and forget."
        );
        ending.lines.Add(line6);

        DialogueItem line7 = new DialogueItem(
            blobber,
            "Well with all the soul shards you've collected, you'll remain stable in this state of existence for quite some time."
        );
        ending.lines.Add(line7);

        DialogueItem line8 = new DialogueItem(
            player,
            "I feel stragely powerful..."
        );
        ending.lines.Add(line8);

        DialogueItem line9 = new DialogueItem(
            blobber,
            "With each soul shard you took in, you also absorbed some of that soul's energy."
        );
        ending.lines.Add(line9);

        DialogueItem line10 = new DialogueItem(
            blobber,
            "And that energy is giving you power."
        );
        ending.lines.Add(line10);

        DialogueItem line11 = new DialogueItem(
            player,
            "I want to get even stronger!"
        );
        ending.lines.Add(line11);

        DialogueItem line12 = new DialogueItem(
            blobber,
            "Uhhh, hey now, what's with that look in your eye?"
        );
        ending.lines.Add(line12);


        DialogueItem line13 = new DialogueItem(
            blobber,
            "You're getting awfully close to me right now, buddy."
        );
        ending.lines.Add(line13);

        DialogueItem line14 = new DialogueItem(
            blobber,
            "Please, don't do this, I've helped you out so much....."
        );
        ending.lines.Add(line14);


        script.Add("ending", ending);
    }



}
