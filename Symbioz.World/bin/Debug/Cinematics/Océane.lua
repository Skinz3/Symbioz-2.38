


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 153878787
npcId = 165
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PO=312,50"
--------

function TalkToNpc()

env:removeItem(312,50);
env:addItem(684,1);
env:sayNpc("Voila pour toi! Un magnifique <b>[Parchemin de Sort]</b>!");
env:smiley(4);
env:spellAnim(49);


end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Rapporte moi 50x <b>[Fer]</b> et je saurai te récompenser!");

end
