


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 54172969
npcId = 92
doneObjectives = {} 
notDoneObjectives = {} 
criteria = "PL>149"
--------


function TalkToNpc()

env:teleport(60035079);

end

function Execute()



end

function CriteriaWrong()

env:sayNpc("How doucement,la foret pétrifiée est reservée au joueurs de niveau 150+.");

end