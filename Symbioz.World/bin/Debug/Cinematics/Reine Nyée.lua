


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 149166080
npcId = 127
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Je n'ai rien a redire, tu es un puissant guerrier...Voici une orbe que j'ai confectionnée d'une valeur inestimable, prend en soin et tu sera remercié.");
env:addItem(10563,1);
env:wait(5000);
env:teleport(147853312);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
