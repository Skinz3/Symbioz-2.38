


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 84674562
npcId = 131
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PO=737"
--------

function TalkToNpc()


env:sayNpc("Un émeraude! Un emeraude!");


end

function Execute()

end

function CriteriaWrong()

env:canInteract(false);
env:sayNpc("<i>Seul les êtres maléfiques sont autorisés a pénétrer dans la clairière du Dark Vlad, les larves de ton espèce n'y rentrerons jamais!</i>");
env:wait(5000);
env:sayNpc("<i>A moins que tu arrives a t'introduire dans la <b>Forêt maléfique..</b> mais l'entrée en <b>[-2,14]</b> est extremement surveillée!.</i>");
env:canInteract(true);


end