using Assets._Project._Scripts.WorldMap.Generation.Settings;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps
{
    public interface IWorldGenerationStep
    {
        List<Type> RequiredDependencies { get; }

        IEnumerator Process(WorldContext context, WorldCreationParameters settings);
    }
}