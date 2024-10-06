/*
    RANDOM ENCOUNTER GENERATOR
    by Alister Brumley 2024
*/
using System.Security.Principal;
using System.Transactions;
using static Globals;

/*
    VARIABLE SETTING
*/
// TERM SETTINGS
Console.CursorVisible = false;
Color.GetDefaults();
// GAME SETINGS
bool winConditon = false;

/*
    MAIN
*/
// CREATING ACTORS AND SORTING SPEED
Actor player = Game.CreatePlayer(args);
Actor[] enemies = Game.CreateEnemies();
Actor[] turnOrder = Game.TurnSort(enemies, player);

Game.Init();

// TURN LOOP
do
{
    Game.PromptReset();
    player.actionState = Game.PlayerChoice();
    if (player.actionState == playerActions.escape)
    {
        Game.RunCheck();
    }
    // else
    // {
    //     Writer.MessageWrite(player.actionState);
    // }

    // TODO - Player Target Choice
    // CLEAR PROMPT BOX
    // FOREACH ENEMY
    // DISPLAY ON PROMPTBOX - WILL HAVE TO RETHINK GLOBAL.PROMPTPOS TO LOCAL LIST ONE
    // GO TO FIRST ENEMY AND HIGHLIGHT
    // player.target == Game.TargetChoice();


    // player.Melee(enemies[0]);
    // Actor tActor = new Slime();
    // Actor tActor = new Ogre();
    // tActor.Melee(player);


    // TODO - Enemy roll for actions
    // TODO - foreach turnorder, do actor[n].action on actor[n].target
    // TODO - if actor[n].hp <=0 - remove from turnorder
    // TODO - if player.hp <= 0 - break
    // TODO - if enemy.hp all <= 0 winCondition = True

    // winConditon = player.Health <= 0 ? true : false;
} while (!winConditon);

Writer.DebugWrite("Game Over!");
Console.Read();

Game.SafeExit();