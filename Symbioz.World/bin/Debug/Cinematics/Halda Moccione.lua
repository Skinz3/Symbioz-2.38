


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 11544576
npcId = 158
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Alors franchement, je suis impressionné, tu as réussi à vaincre cette terrible créature ! Mais quelle chance !");
env:addOrnament(78);
env:wait(5000);
env:teleport(143361);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
