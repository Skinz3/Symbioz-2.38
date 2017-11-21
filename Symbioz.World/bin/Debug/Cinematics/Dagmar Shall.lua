


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 122685953	
npcId = 154
doneObjectives = {} 
notDoneObjectives = {} 
criteria = "DO!12"
--------


function TalkToNpc()


env:sayNpc("Ne te fatigue pas c'est un faux. Le Dark Vlad a laisser ici une réplique du véritable <b>[Dofus Emeraude]</b> pour faire baver les jeunes explorateur comme toi ou moi ^^. On raconte qu'il se trouve dans la forêt maléfique , a Amakna et qu'il le protège tant bien que mal...Tiens, voici une histoire le concernant.");
env:addItem(7361,1);
env:reach(12);

	
end

function Execute()

  

end

function CriteriaWrong()

env:sayNpc("Que c'est bow...");

end