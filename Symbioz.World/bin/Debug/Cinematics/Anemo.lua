


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 153880835
npcId = 46
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>14"
--------

function TalkToNpc()

env:addItem(1699,1);
env:teleport(84674563);
env:video(6);


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Tu dois être niveau 15 au moins pour aller a Astrub!"); 

end
