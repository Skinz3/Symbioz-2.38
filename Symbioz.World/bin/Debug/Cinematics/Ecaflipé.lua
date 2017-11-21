


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674051
npcId = 117
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

local position = env:getPortalPositionString("Portail vers Ecaflipus");

if position == "?" then

env:sayNpc("La position du portail ecaflipus? je n'en sais rien :p");


else

env:sayNpc(string.format("La position du portail ecaflipus? %s",position));

end

end

function Execute()

end

function CriteriaWrong()



end
