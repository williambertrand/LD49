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

        player = new Character("You", true, "Portraits/portraitPlaceholder");
        blobber = new Character("Floating Blob", false, "Portraits/blobPlaceholder");

        // Scripts needed for tutorial
        if(SceneManager.GetActiveScene().buildIndex == GameScenes.OPENING)
        {
            AddRoomOneDialogue();
            // Todo: talk on enemies
        }

        // Scripts needed for gameplay scene
        if (SceneManager.GetActiveScene().buildIndex == GameScenes.GAMEPLAY)
        {
            addFirstPortalScript();
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
            "What is this... place? Where are we?"
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
            "That's enouch chatting. Quickly now, go grab some shards over there."
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
            "Well, you don't want to go completely unstable, do you? Sorry, but that's just the world we live in. Well I guess \"live\" isn't really the right word for you anymore, but you get what I mean."
        );
        blobberPortal.lines.Add(line5);

        script.Add("aboutPortalsDialogue", blobberPortal);


        Dialogue secondBlobberPortals = new Dialogue();
        DialogueItem line11 = new DialogueItem(
            blobber,
            "Go on, hop into that portal and go get yourself some more soul shards."
        );
        secondBlobberPortals.lines.Add(line11);

        script.Add("secondAboutPortalsDialogue", secondBlobberPortals);
    }





}
