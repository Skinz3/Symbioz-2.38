


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 153880064
npcId = 109
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>15"
--------

function TalkToNpc()

env:sayNpc("How, mais tu n'es plus un jeune guerrier imprudent maintenant, tu as bien grandi!");

end

function Execute()

end

function CriteriaWrong()

env:canInteract(false);
env:sayNpc("Attend jeune guerrier imprudent!");
env:wait(3000);
env:sayNpc("Cette zone est dangeureuse, tu peux la franchir mais prend garde au monstres que tu y rencontrera!");
env:wait(5000);
env:sayNpc("Dans cette zone, le <b>Cimetière</b> l'experience est multiplié par 6, et dans le donjon , par 8");
env:wait(1000);
env:smileyNpc(32);
env:canInteract(true);

end
