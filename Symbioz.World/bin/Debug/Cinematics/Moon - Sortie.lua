


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 157291520
npcId = 162
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Tu n'as pas l'air d'être un petit joueur toi! Félicitation, tu as grimpé en haut de mon arbre, voici plusieurs petites récompenses.");
env:addItem(797,5);
env:addItem(801,5);
env:addItem(805,5);
env:addItem(810,5);
env:addItem(814,5);
env:addItem(817,5);
env:wait(5000);
env:teleport(156499968);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
