


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 54172969
npcId = 182
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>49"
--------

function TalkToNpc()

env:teleport(54161738);

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Tu dois être niveau 50 pour te rendre au berceau d'Alma!");

end
