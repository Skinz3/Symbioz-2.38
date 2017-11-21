


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 88213257
npcId = 132
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PO=382,3"
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Bon courage jeune guerrier");
env:addOrnament(36);
env:removeItem(382,3);
env:wait(3000);
env:teleport(53085710);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Hola, on ne rentre pas comme ça dans la forêt maléfique , rapportes moi 3x <b>[Ongle de Chevaucheur de Karne]</b> et je te ferais entrer!"); 

end
