


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674051
npcId = 114
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

local position = env:getPortalPositionString("Portail vers Srambad");

if position == "?" then

env:sayNpc("Je n'ai aucune idée d'ou peut bien se trouver le portail de srambad... repasse me voir plus tard");


else

env:sayNpc(string.format("La position du portail srambad? %s",position));

end

end

function Execute()

end

function CriteriaWrong()



end
