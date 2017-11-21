


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 123213824
npcId = 129
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Tes talents sont remarquable jeune Dofusien, laisse moi te faire une offrande.");
env:addItem(694,1);
env:wait(3000);
env:teleport(88083210);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
