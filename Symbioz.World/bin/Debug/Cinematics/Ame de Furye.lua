


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84675075
npcId = 123
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PO=10563"
--------

function TalkToNpc()

env:restat();
env:removeItem(10563,1);
env:sayNpc("Et voila jeune joueur! Tes caractéristiques on été reinitialisés!"); 

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Je peux reinitialiser tes caractéristiques si tu m'apportes 1x <b>[Orbe Reconstituant]</b>! On raconte que la Reine Nyée est la seule capable de savoir les fabriquer!"); 

end
