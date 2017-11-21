


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 169083904
npcId = 112
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Nous boila, Astrub!!");
env:spellAnim(29);
env:wait(1500);
env:teleport(84674562);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
