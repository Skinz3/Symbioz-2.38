


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 153880323
npcId = 164
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PO=421,50"
--------

function TalkToNpc()



env:removeItem(421,50);
env:sayNpc("Tiens, voici un <b>[Petit Parchemin de Vitalité] </b> et 50x <b>[Ticket Doré]</b>... je ne sais pas a quoi ça peut bien servir...");
env:addItem(17745,50);
env:addItem(806,1);


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Rapporte moi 50x <b>[Ortie]</b> et je ferais de toi mon homme!");

end
