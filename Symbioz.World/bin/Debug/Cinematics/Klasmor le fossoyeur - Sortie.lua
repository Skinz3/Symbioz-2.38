


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 152835072
npcId = 110
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Petit, tout ceci est prometteur, fécilitation, tu as vaincu <b>Kardorim</b>! Laisse moi te récompenser!");
env:addItem(14048,1);
env:wait(3000);
env:teleport(153881600);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
