


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 120062979
npcId = 124
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>39"
--------

function TalkToNpc()

env:sayNpc("Mais ou est donc passé Popol le Pouple?");
env:addTitle(62);


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Revient me voir niveau 40, j'aurais une petite surprise pour toi!"); 

end
