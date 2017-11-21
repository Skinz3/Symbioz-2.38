


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 51642368
npcId = 130
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Niaf ... Niaf.. Niaf");
env:addItem(737,1);
env:wait(3000);
env:teleport(88213257);
env:canInteract(true);


end

function Execute()

end

function CriteriaWrong()

end