


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 54172969
npcId = 137
doneObjectives = {}
notDoneObjectives = {} 
criteria = "DO=10&DO=11&DO!19"
--------

function TalkToNpc()


env:sayNpc("Merci énormément pour ton aide dans ces donjons, j'ai une récompense pour toi! J'ai trouvé ce <b>[Dofus Des Glaces]</b> au cours d'une exploration frigostienne, j'espère qu'il te sera utile!");
env:reach(19);
env:addItem(7043,1);


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Je fais des recherches sur Frigost, j'ai comme projet de me rendre dans les différents donjons de la région pour parfaire mes connaissances, si tu t'y rends, on se rencontrera peut être!");

end