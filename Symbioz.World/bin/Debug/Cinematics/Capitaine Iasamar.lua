


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674562
npcId = 125
doneObjectives = {}
notDoneObjectives = {} 
criteria = "Pw=2&PO=1575,1"
--------

function TalkToNpc()

env:createGuild();
env:removeItem(1575,1);

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Rapportez moi 1x <b>[Guildalogemme]</b> et vous rendrez un service.");

end
