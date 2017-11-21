using Symbioz.Core;
using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Modules
{
    public class NotificationModule : Singleton<NotificationModule>
    {
        static Logger logger = new Logger();
        /// <summary>
        /// En minutes
        /// </summary>
        const int Interval = 3;

        static string[] Messages = new string[]
        {
           "Vous n\'avez pas voté depuis un moment ? merci de vous rendre sur <u><a href=\"http://beta-leaf.16mb.com/vote.php\">la page de vote</a></u>. vous pourrez ainsi acheter vos items boutique via le systeme de points! (Ticket Doré, Kamas, Orbe reconstituant etc..)",
           "N'hésitez pas a tester le DayFight en cliquant sur le havre sac, le groupe de monstre change tous les jours et de belles récompenses vous attendent :p",
           "Les ateliers de Bonta peuvent être utile pour fabriquer des objets, et les hotels de vente pour les entreposer",
           "Vous n\'avez pas voté depuis un moment ? merci de vous rendre sur <u><a href=\"http://beta-leaf.16mb.com/vote.php\">la page de vote</a></u>. vous pourrez ainsi acheter vos items boutique via le systeme de points!",
           "Tapez .donjon dans le chat pour accéder aux différents donjons! Combatez le DarkVlad et droppez le Dofus Emeraude! Combatez le Commandant Imagiro et tentez le Dofus Turquoise! Combatez Merkator et droppez le Dofus des Glaces!",

        };

        static int Index = 0;

      //  [StartupInvoke("Notification Module", StartupInvokePriority.Modules)]
        public static void Initialize()
        {
            ActionTimer timer = new ActionTimer(Interval * 1000 * 60, DisplayNotif, true);
            timer.Start();
        }
        static void DisplayNotif()
        {
            try
            {
                WorldServer.Instance.OnClients(x => x.Character.Notification(Messages[Index]));
                Index++;
                Index = Index >= Messages.Length ? 0 : Index;
                logger.White("Notification sended to clients!");
            }
            catch (Exception ex)
            {
                logger.Error("Cannot display notification to clients: " + ex);
            }
        }
    }
}
