


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 167776256
npcId = 161
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>159"
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Mes forces ont décliné. Si seulement tu m'avais vu combattre quand nous tenions Amakna entre nos griffes... À l époque, j'en ai massacré des aventuriers, et pas que les plus faibles. Leurs cris de désespoir et les pleurs de leurs compagnons résonnent encore à mes oreilles et bercent mes nuits.");
env:addItem(694,1);
env:wait(12000);
env:teleport(143372);
env:canInteract(true);


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Tu vas devoir te montrer patient, car tu n es pas encore prêt. Bientôt, tu pourras partir à la recherche du premier des Dofus Primordiaux... le <b>Dofus Pourpre</b>. Lorsque tu ne seras plus un novice, et que tu auras atteint le niveau <b>160</b> reviens me voir.");

end