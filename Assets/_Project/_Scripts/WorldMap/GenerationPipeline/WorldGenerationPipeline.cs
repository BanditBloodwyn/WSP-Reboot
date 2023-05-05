using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline
{
    public class WorldGenerationPipeline
    {
        private readonly List<IWorldGenerationStep> _steps = new();

        public void AddStep(IWorldGenerationStep step)
        {
            _steps.Add(step);
        }

        public void RemoveStep(IWorldGenerationStep step)
        {
            _steps.Remove(step);
        }

        public IEnumerator Execute(WorldContext context, WorldCreationParameters settings)
        {
            HashSet<Type> executedSteps = new();

            foreach (IWorldGenerationStep step in _steps)
            {
                List<Type> dependencies = step.RequiredDependencies;

                if (dependencies == null || dependencies.All(dep => executedSteps.Contains(dep)))
                {
                    Debug.Log($"Process <b><i>{step.GetType().Name}</i></b>");

                    yield return step.Process(context, settings);
                    executedSteps.Add(step.GetType());
                }
                else
                    ThrowMissingDependencyException(step);
            }
        }

        private static void ThrowMissingDependencyException(IWorldGenerationStep step)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\nCannot execute {step.GetType().Name} as its dependencies have not been executed.");
            sb.AppendLine("This step depends on:");

            foreach (Type dependency in step.RequiredDependencies)
                sb.AppendLine($"- <b><i>{dependency.Name}</i></b>");

            throw new InvalidOperationException(sb.ToString());
        }
    }
}