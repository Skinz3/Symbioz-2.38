


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674563
npcId = 190
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Veux-tu inscrire ta guilde dans le tournois des GvGs?");
env:wait(3000);
env:SendRawData("gvgrequest");
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()


end
