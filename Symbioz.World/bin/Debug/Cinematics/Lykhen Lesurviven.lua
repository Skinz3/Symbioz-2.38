


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 154010883;
npcId = 59;
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:sayNpc(string.format("La quantité d'expérience qu'acquiert un joueur en fin de combat dépend des zones sur ce serveur...Ici, sur la <b>Route des âmes</b> l'expérience est multipliée par %s!",env:getZoneExperienceRate()));

end

function Execute()

end

function CriteriaWrong()

end