


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 105649152
npcId = 179
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Merci de m'avoir libéré de ce monstre ignoble jeune aventurier, voici une <b>[Guildalogemme] </b>!");
env:addItem(1575,1);
env:wait(3000);
env:teleport(84679430);
env:canInteract(true);

end

function Execute()

end

function CriteriaWrong()



end
