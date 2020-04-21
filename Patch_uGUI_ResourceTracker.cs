using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Straitjacket.Harmony;

namespace PersonalScan
{
    [HarmonyPatch(typeof(uGUI_ResourceTracker))]
    [HarmonyPatch("GatherScanned")]
    class Patch_uGUI_ResourceTracker
    {
        static void Postfix(uGUI_ResourceTracker __instance)
        {
            Debugger.Log("Hello");

            var v = Player.main.GetComponent<PersonalScanBehaviour>();

            if (v)
            {
                var a = Traverse.Create(__instance).Field("nodes").GetValue() as HashSet<ResourceTracker.ResourceInfo>;

                v.GetDiscoveredNodes(a);

                Traverse.Create(__instance).Field("nodes").SetValue(a);
            }
        }
    }
}
