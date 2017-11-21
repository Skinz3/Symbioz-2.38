


 -- Script De cinématique. 140510209
---------
author = "Skinz"
mapId = 54172969
npcId = 178
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>49"
--------

function TalkToNpc() 


env:sayNpc("Cette zone est temporairement inaccessible");



end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Hop hop, tu dois être niveau <b>50</b> pour accéder au <b>Roc des Salbatroce</b>!"); 

end
