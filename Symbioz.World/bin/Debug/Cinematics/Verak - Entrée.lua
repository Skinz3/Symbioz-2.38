


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84676355
npcId = 115
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("On souhaite se mesurer a la grande pretresse des champs? hum tu es bien courageux en avant!");
env:wait(3000);
env:teleport(105381888);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
