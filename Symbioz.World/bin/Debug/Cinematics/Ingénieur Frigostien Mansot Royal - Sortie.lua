


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 56103936
npcId = 136
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Incroyable, tu as reussi à le vaincre, j'ai pu observer tout le combat et j'ai pris des notes! merci de m'avoir aider");
env:reach(11);
env:wait(5000);
env:teleport(54165815);
env:canInteract(true);


end

function Execute()

end

function CriteriaWrong()



end