


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674051
npcId = 118
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

local position = env:getPortalPositionString("Portail vers Enutrosor");

if position == "?" then

env:sayNpc("Je n'ai aucune idée d'ou peut bien se trouver le portail pour enutrosor...");


else

env:sayNpc(string.format("La position du portail enutrosor? %s",position));

end

end

function Execute()

end

function CriteriaWrong()



end
