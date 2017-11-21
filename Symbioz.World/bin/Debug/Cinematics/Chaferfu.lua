


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 87034370	
npcId = 157
doneObjectives = {} 
notDoneObjectives = {} 
criteria = ""
--------


function TalkToNpc()

env:canInteract(false);
env:sayNpc("Mon secret t'appartient!");
env:addItem(9200,1);
env:wait(5000);
env:teleport(82314497);
env:canInteract(true);

	
end

function Execute()

  

end

function CriteriaWrong()



end