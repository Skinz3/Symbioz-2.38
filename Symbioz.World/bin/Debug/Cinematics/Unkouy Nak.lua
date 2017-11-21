


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674563
npcId = 40
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PO=14503,100"
--------

function TalkToNpc()

env:canInteract(false);
env:sayNpc("Merci beaucoup! Voici ta récompense");
env:addItem(972,1);
env:removeItem(14503,100);
env:canInteract(true);



end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Il faut absolument que je donne a manger a mes wabbits... pourrais-tu me rendre un service? pourrais tu me ramener 100 Cawottes Fraiches, on en trouve sur <b>l'Ile de la Cawotte</b> je donne un Dofus au premier qui me les rapporte :p ");

end