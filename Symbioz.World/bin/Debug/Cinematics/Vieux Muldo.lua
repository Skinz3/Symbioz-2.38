


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674562
npcId = 189
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PT=196"
--------

function TalkToNpc()

env:removeFirstItemByType(196);
env:addItem(12124,3000);
env:sayNpc("C'est fait!");


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Je peux  échanger un certificat de muldo contre 3000 x <b>[Piece de kama géante]</b>! Mais... tu n'en a pas");

end
