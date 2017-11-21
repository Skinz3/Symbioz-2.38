


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 149428224
npcId = 128
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Je ne suis pas assez fort...je ne peux .. que ceder..");
env:addItem(739,1);
env:wait(3000);
env:teleport(147849218);
env:canInteract(true);


end

function Execute()

end

function CriteriaWrong()



end
