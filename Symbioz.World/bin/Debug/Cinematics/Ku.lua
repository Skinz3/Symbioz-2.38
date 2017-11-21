


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 101452295	
npcId = 166
doneObjectives = {} 
notDoneObjectives = {} 
criteria = ""
--------


function TalkToNpc()

local canAlmanach = env:canAlmanach();

if canAlmanach == false then

env:sayNpc("<i>Tu as déja honoré les mérydes aujourd'hui.");
return;

end

local hasDoneAlmanach = env:honorAlmanach();

if hasDoneAlmanach == false then

env:canInteract(false);
env:orientation(1);
env:teleportSameMap(398);
env:sayNpc("<i>*Vous regarde fixement*");
env:wait(2000);
env:orientation(7);
env:wait(2000);
env:say("<i>*Vous soutenez le regard du Meryde*");
env:spellAnim(7355);
env:wait(4000);
env:sayNpc("<i>*Ku a peur et vous propose une offre*");
env:wait(4000);
env:sayNpc(env:getAlmanachRequestString());

env:canInteract(true);
else

env:sayNpc("<i>Te voila récompensé, création.");

end


	
end

function Execute()

  

end

function CriteriaWrong()



end