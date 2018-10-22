using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityModManagerNet;
using Harmony12;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSearchAutoSelectNS
{
    public class ShopSearchAutoSelect
    {
        public static bool enabled = true;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            HarmonyInstance harmony = HarmonyInstance.Create(modEntry.Info.Id);
            harmony.PatchAll();
            modEntry.OnToggle = OnToggle;
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;
            return true;
        }
    }

    [HarmonyPatch(typeof(UIManager), "OpenShop")]
    class OpenShop_Patch
    {
        static void Postfix(Transform ___Shop, string ___currentPage)
        {
            if (ShopSearchAutoSelect.enabled)
            {
                ___Shop.parent.Find(___currentPage + "/InputField").GetComponent<InputField>().ActivateInputField();
            }
        }
    }
}
