using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;

namespace PersonalScan
{
    [HarmonyPatch(typeof(Player))]
    [HarmonyPatch("Awake")]
    class Patch_Player
    {
        static void Postfix(Player __instance)
        {
            __instance.gameObject.AddComponent<PersonalScanBehaviour>();
        }
    }
}
