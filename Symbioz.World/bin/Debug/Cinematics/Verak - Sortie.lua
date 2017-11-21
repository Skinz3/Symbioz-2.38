


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 105387008
npcId = 98
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Félicitation jeune joueur, laisse moi te récompenser pour ta bravoure!");
env:addItem(9482,1);
env:wait(3000);
env:teleport(84676355);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
