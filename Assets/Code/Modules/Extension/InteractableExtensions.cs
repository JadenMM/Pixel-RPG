using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
    public static class InteractableExtensions
    {
        public static List<ChestComponent> Chests(this Dictionary<GameObject, Interactable> interactables)
        {
            var chests = new List<ChestComponent>();

            foreach (var interactable in interactables.Values)
            {
                if (interactable is ChestComponent)
                    chests.Add((ChestComponent)interactable);
            }

            return chests;
        }

        public static List<InteractableCharacterComponent> Characters(this Dictionary<GameObject, Interactable> interactables)
        {
            var characters = new List<InteractableCharacterComponent>();

            foreach (var interactable in interactables.Values)
            {
                if (interactable is InteractableCharacterComponent)
                    characters.Add((InteractableCharacterComponent)interactable);
            }

            return characters;
        }

    }
}

