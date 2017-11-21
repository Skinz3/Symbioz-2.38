


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 57157377
npcId = 134
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Mais!! Tu m'as écouté n'est-ce pas? j'ai bien trouvé son point faible, mes recherches portent donc leur fruits! Merci de m'avoir aider!");
env:reach(10);
env:wait(5000);
env:teleport(54169427);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end