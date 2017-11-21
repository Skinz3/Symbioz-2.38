


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674563
npcId = 113
doneObjectives = {} 
notDoneObjectives = {} 
criteria = "PL>30"
--------


function TalkToNpc()

env:dayfight();

end

function Execute()


end

function CriteriaWrong()

env:sayNpc("How doucement, le Dayfight est une lutte très éprouvante, reviens me voir quand tu seras niveau 30+.");

end