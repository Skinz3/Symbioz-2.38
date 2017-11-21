


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 153879809
npcId = 119
doneObjectives = {}
notDoneObjectives = {} 
criteria = "OR!67"
--------

function TalkToNpc()

env:sayNpc("Oui, j'ai décider de changer d'air en me retirant du temple iop, l'air est frais ici! d'ailleurs je suis généreux, voila pour toi!");
env:addOrnament(67);

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Incarnam est un lieu si paisible.");

end
