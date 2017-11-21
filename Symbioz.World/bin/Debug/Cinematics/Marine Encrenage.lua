


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674562
npcId = 104
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>99"
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Nous boila, bai de bufokia!!");

env:spellAnim(29);
env:wait(1500);
env:teleport(169083904);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("<i>Tu es beaucoup trop jeune pour te rendre dans la baie de sufokia, reviens me voir quand tu seras niveau 100</i>");

end
