using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Settings.Scriptables;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps
{
    public interface IWorldGenerationStep
    {
        List<Type> RequiredDependencies { get; }

        IEnumerator Process(WorldContext context, WorldCreationParameters settings);
    }
}