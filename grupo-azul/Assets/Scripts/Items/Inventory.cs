using System;
using UnityEngine;

namespace Items
{
    public class Inventory : MonoBehaviour
    {
        public bool HudNavigator { get; private set; }
        public bool FluxCondensor { get; private set; }
        public bool IaNeuralInterface { get; private set; }
        
        public void Awake()
        {
            HudNavigator = false;
            FluxCondensor = false;
            IaNeuralInterface = false;
        }

        public void AddSpaceshipPart(SpaceshipPart.Id part)
        {
            switch (part)
            {
                case SpaceshipPart.Id.HudNavigator:
                    HudNavigator = true;
                    break;
                case SpaceshipPart.Id.FluxCondensor:
                    FluxCondensor = true;
                    break;
                case SpaceshipPart.Id.IaNeuralInterface:
                    IaNeuralInterface = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary> Comprobar si una pieza de la nave fue recogida </summary>
        /// <param name="part"> Id de la pieza de la nave </param>
        public bool WasCollected(SpaceshipPart.Id part)
        {
            return part switch
            {
                SpaceshipPart.Id.HudNavigator => HudNavigator,
                SpaceshipPart.Id.FluxCondensor => FluxCondensor,
                SpaceshipPart.Id.IaNeuralInterface => IaNeuralInterface,
                _ => false
            };
        }
    }
}